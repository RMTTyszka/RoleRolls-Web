import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {FormGroup} from '@angular/forms';
import {Inventory} from '../../../shared/models/Inventory.model';
import {isBelt} from '../../../shared/utils/isItem';
import {BeltInstance} from '../../../shared/models/BeltInstance.model';

@Component({
  selector: 'rr-inventory-belt-select',
  templateUrl: './inventory-belt-select.component.html',
  styleUrls: ['./inventory-belt-select.component.css']
})
export class InventoryBeltSelectComponent implements OnInit {
  formName = 'belt';
  placeholder = 'Belt';
  @Output() beltSelected = new EventEmitter<BeltInstance>();
  constructor() {}

  ngOnInit() {
  }
  filter = (inventoryForm: FormGroup) => (inventoryForm.value as Inventory).items
    .filter(item => isBelt(item)) as BeltInstance[]

  itemSelected(selectedBelt: BeltInstance) {
    this.beltSelected.next(selectedBelt);
  }
}
