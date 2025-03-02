import { Component, Input } from '@angular/core';
import { UntypedFormGroup } from '@angular/forms';
import { ItemModel } from '@app/campaigns/models/item-model';
import { SubscriptionManager } from '@app/tokens/subscription-manager';
import {CampaignSessionService} from '@app/campaign-session/campaign-session.service';
import {CreatureDetailsService} from '@app/creatures/creature-details.service';
import { ItemInstanceService } from '@app/services/itens/instances/item-instance.service';
import {EquipableSlot} from '@app/models/itens/equipable-slot';
import {debounceTime} from 'rxjs/operators';

@Component({
  selector: 'rr-creature-equipment-slot',
  imports: [],
  templateUrl: './creature-equipment-slot.component.html',
  styleUrl: './creature-equipment-slot.component.scss'
})
export class CreatureEquipmentSlotComponent {

  public item: ItemModel;
  @Input() public slot: string;
  @Input() public creatureForm: UntypedFormGroup;
  private subscriptionManager = new SubscriptionManager();

  private get creatureId() {
    return this.creatureForm.get('id').value;
  }
  constructor(
    private itemInstanceService: ItemInstanceService,
    private campaignDetailsService: CampaignSessionService,
    private creatureDetailsService: CreatureDetailsService,
  ) {
  }

  ngOnDestroy(): void {
    this.subscriptionManager.clear();
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
