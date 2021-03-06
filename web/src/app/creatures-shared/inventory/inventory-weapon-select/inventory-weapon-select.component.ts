import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {isWeapon} from 'src/app/shared/utils/isItem';
import {WeaponInstance} from 'src/app/shared/models/WeaponInstance.model';
import {FormGroup} from '@angular/forms';
import {Inventory} from 'src/app/shared/models/Inventory.model';

@Component({
  selector: 'loh-inventory-weapon-select',
  templateUrl: './inventory-weapon-select.component.html',
  styleUrls: ['./inventory-weapon-select.component.css']
})
export class InventoryWeaponSelectComponent implements OnInit {
  @Input() weaponFormName = 'mainWeapon';
  @Input() placeholder = 'Main Weapon';
  @Output() weaponSelected = new EventEmitter<WeaponInstance>();
  constructor() {}

  ngOnInit() {
  }
  filter = (inventoryForm: FormGroup) => (inventoryForm.value as Inventory).items
    .filter(item => isWeapon(item)) as WeaponInstance[]

  search = (filter: string, items: Array<WeaponInstance>) => items
      .filter((item: WeaponInstance) => item.name.includes(filter)
        || item.weaponModel.baseWeapon.name.includes(filter)
        || item.weaponModel.description.includes(filter)
        || item.weaponModel.specialName.includes(filter))
      .map(item => item.name)
  itemSelected(selectedWeapon: WeaponInstance) {
    this.weaponSelected.next(selectedWeapon);
  }
}
