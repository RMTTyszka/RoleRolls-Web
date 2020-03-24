import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {HeadpieceInstance} from '../../../shared/models/HeadpieceInstance.model';
import {FormGroup} from '@angular/forms';
import {Inventory} from '../../../shared/models/Inventory.model';
import {isHeadpiece} from '../../../shared/utils/isItem';
import {NeckAccessoryInstance} from '../../../shared/models/NeckAccessoryInstance.model';

@Component({
  selector: 'loh-inventory-neckaccesory-select',
  templateUrl: './inventory-neckaccesory-select.component.html',
  styleUrls: ['./inventory-neckaccesory-select.component.css']
})
export class InventoryNeckaccesorySelectComponent implements OnInit {
  formName = 'neckAccessory';
  placeholder = 'Neck Accessory';
  @Output() neckAccessorySelected = new EventEmitter<NeckAccessoryInstance>();
  constructor() {}

  ngOnInit() {
  }
  filter = (inventoryForm: FormGroup) => (inventoryForm.value as Inventory).items
    .filter(item => isHeadpiece(item)) as NeckAccessoryInstance[]

  search = (filter: string, items: Array<NeckAccessoryInstance>) => items
    .filter((item: NeckAccessoryInstance) => item.name.includes(filter)
      || item.neckAccessoryModel.name.includes(filter))
    .map(item => item.name)

  itemSelected(selectedNeckAccessoryInstance: NeckAccessoryInstance) {
    this.neckAccessorySelected.next(selectedNeckAccessoryInstance);
  }
}
