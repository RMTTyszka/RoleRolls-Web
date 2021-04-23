import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {isWeapon} from 'src/app/shared/utils/isItem';
import {WeaponInstance} from 'src/app/shared/models/WeaponInstance.model';
import {FormGroup} from '@angular/forms';
import {Inventory} from 'src/app/shared/models/Inventory.model';
import {ItemInstantiatorPath} from '../../../masters/master-tools/master-item-instantiator/master-item-instantiator.component';
import {ItemInstance} from '../../../shared/models/ItemInstance.model';

@Component({
  selector: 'rr-inventory-weapon-select',
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

  itemSelected(selectedWeapon: ItemInstance) {
    this.weaponSelected.next(selectedWeapon as WeaponInstance);
  }
}
