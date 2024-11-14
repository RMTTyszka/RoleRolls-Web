import {Component, Input, OnInit} from '@angular/core';
import {EquipableSlotOld} from '../../../../shared/models/items/EquipableSlotOld';
import {EquipableSlot} from '../../../../shared/models/pocket/itens/equipable-slot';
import {FormsModule, UntypedFormGroup} from '@angular/forms';
import {ItemModel} from '../../../../shared/models/pocket/creatures/item-model';
import {InputTextModule} from 'primeng/inputtext';
import {NgIf} from '@angular/common';
import _ from 'lodash';
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

  ngOnInit() {
    this.item = this.creatureForm.get('equipment.' + _.camelCase(this.slot)).value ?? new ItemModel();
  }
}
