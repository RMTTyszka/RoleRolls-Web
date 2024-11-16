import {Component, Input, OnInit} from '@angular/core';
import {FormControl, FormGroup, FormsModule, UntypedFormArray, UntypedFormControl, UntypedFormGroup} from '@angular/forms';
import {ItemModel} from '../../../../shared/models/pocket/creatures/item-model';
import {InputTextModule} from 'primeng/inputtext';
import {NgIf, NgStyle} from '@angular/common';
import _ from 'lodash';
import {SubscriptionManager} from '../../../../shared/utils/subscription-manager';
import {CreatureDetailsService} from '../../creature-details.service';
import {debounceTime} from 'rxjs/operators';
import {ButtonDirective} from 'primeng/button';
import {TooltipModule} from 'primeng/tooltip';
import {ItemInstanceService} from '../../../items/item-instantiator/services/item-instance.service';
import {PocketCampaignDetailsService} from '../../../campaigns/CampaignInstance/pocket-campaign-bodyshell/pocket-campaign-details.service';
import {EquipableSlot} from '../../../../shared/models/pocket/itens/equipable-slot';
import {Entity} from '../../../../shared/models/Entity.model';
import {getAsForm} from '../../../../shared/EditorExtension';
@Component({
  selector: 'rr-creature-equipment-slot',
  standalone: true,
  imports: [
    InputTextModule,
    FormsModule,
    NgIf,
    ButtonDirective,
    TooltipModule,
    NgStyle
  ],
  templateUrl: './creature-equipment-slot.component.html',
  styleUrl: './creature-equipment-slot.component.scss'
})
export class CreatureEquipmentSlotComponent implements OnInit {

  public item: ItemModel;
  @Input() public slot: string;
  @Input() public creatureForm: UntypedFormGroup;
  private subscriptionManager = new SubscriptionManager();

  private get creatureId() {
    return this.creatureForm.get('id').value;
  }
  constructor(
    private itemInstanceService: ItemInstanceService,
    private campaignDetailsService: PocketCampaignDetailsService,
    private creatureDetailsService: CreatureDetailsService,
  ) {
  }
  ngOnInit() {
    this.getItems();
    this.subribeToValueChanges();
  }

  public getItems() {
    this.item = this.creatureForm.get('equipment.' + _.camelCase(this.slot)).value ?? new ItemModel();
  }
  public subribeToValueChanges() {
    this.subscriptionManager.add('itensArray', this.creatureForm.get('equipment').valueChanges
      .pipe(debounceTime(100))
      .subscribe(() => {
      this.getItems();
    }));
  }

  unequip(item: ItemModel) {
    this.itemInstanceService.unequip(this.campaignDetailsService.campaign.id, this.creatureId, EquipableSlot[this.slot])
      .subscribe(() => {
        this.creatureDetailsService.refreshCreature.next(this.creatureId);
      });
  }
}
