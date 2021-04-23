import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {FormGroup} from '@angular/forms';
import {ArmorInstance} from 'src/app/shared/models/items/ArmorInstance.model';
import {Inventory} from 'src/app/shared/models/Inventory.model';
import {isArmor} from 'src/app/shared/utils/isItem';
import {ItemInstance} from '../../../shared/models/ItemInstance.model';

@Component({
  selector: 'rr-inventory-armor-select',
  templateUrl: './inventory-armor-select.component.html',
  styleUrls: ['./inventory-armor-select.component.css']
})
export class InventoryArmorSelectComponent implements OnInit {
  armorFormName = 'armor';
  placeholder = 'Armor';
  @Output() armorSelected = new EventEmitter<ArmorInstance>();
  constructor() {}

  ngOnInit() {
  }
  filter = (inventoryForm: FormGroup) => (inventoryForm.value as Inventory).items
    .filter(item => isArmor(item)) as ArmorInstance[]

  itemSelected(selectedArmor: ItemInstance) {
    this.armorSelected.next(selectedArmor as ArmorInstance);
  }
}
