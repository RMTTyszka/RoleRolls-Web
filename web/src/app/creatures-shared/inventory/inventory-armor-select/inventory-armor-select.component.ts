import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {FormGroup} from '@angular/forms';
import {ArmorInstance} from 'src/app/shared/models/ArmorInstance.model';
import {Inventory} from 'src/app/shared/models/Inventory.model';
import {isArmor} from 'src/app/shared/utils/isItem';

@Component({
  selector: 'loh-inventory-armor-select',
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

  search = (filter: string, items: Array<ArmorInstance>) => items
      .filter((item: ArmorInstance) => item.name.includes(filter)
        || item.armorModel.baseArmor.name.includes(filter))
      .map(item => item.name)

  itemSelected(selectedArmor: ArmorInstance) {
    this.armorSelected.next(selectedArmor);
  }
}
