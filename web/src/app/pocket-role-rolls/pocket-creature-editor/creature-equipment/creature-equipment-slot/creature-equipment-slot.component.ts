import {Component, Input, OnInit} from '@angular/core';
import {EquipableSlotOld} from '../../../../shared/models/items/EquipableSlotOld';
import {EquipableSlot} from '../../../../shared/models/pocket/itens/equipable-slot';
import {FormsModule, UntypedFormGroup} from '@angular/forms';
import {ItemModel} from '../../../../shared/models/pocket/creatures/item-model';
import {InputTextModule} from 'primeng/inputtext';
import {NgIf} from '@angular/common';
import _ from 'lodash';
import {SubscriptionManager} from '../../../../shared/utils/subscription-manager';
import {CreatureDetailsService} from '../../creature-details.service';
import {debounceTime} from 'rxjs/operators';
@Component({
  selector: 'rr-creature-equipment-slot',
  standalone: true,
  imports: [
    InputTextModule,
    FormsModule,
    NgIf
  ],
  templateUrl: './creature-equipment-slot.component.html',
  styleUrl: './creature-equipment-slot.component.scss'
})
export class CreatureEquipmentSlotComponent implements OnInit {

  public item: ItemModel;
  @Input() public slot: string;
  @Input() public creatureForm: UntypedFormGroup;
  private subscriptionManager = new SubscriptionManager();

  constructor(
    private creatureDetailsService: CreatureDetailsService
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
}
