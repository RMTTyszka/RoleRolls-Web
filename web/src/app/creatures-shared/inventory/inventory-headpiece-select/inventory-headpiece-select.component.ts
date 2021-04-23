import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {HeadpieceInstance} from '../../../shared/models/HeadpieceInstance.model';
import {FormGroup} from '@angular/forms';
import {Inventory} from '../../../shared/models/Inventory.model';
import {isHeadpiece} from '../../../shared/utils/isItem';

@Component({
  selector: 'rr-inventory-headpiece-select',
  templateUrl: './inventory-headpiece-select.component.html',
  styleUrls: ['./inventory-headpiece-select.component.css']
})
export class InventoryHeadpieceSelectComponent implements OnInit {
  formName = 'headpiece';
  placeholder = 'Head';
  @Output() headpieceSelected = new EventEmitter<HeadpieceInstance>();
  constructor() {}

  ngOnInit() {
  }
  filter = (inventoryForm: FormGroup) => (inventoryForm.value as Inventory).items
    .filter(item => isHeadpiece(item)) as HeadpieceInstance[]

  search = (filter: string, items: Array<HeadpieceInstance>) => items
    .filter((item: HeadpieceInstance) => item.name.includes(filter)
      || item.headpieceModel.name.includes(filter))
    .map(item => item.name)

  itemSelected(selectedHeadpiece: HeadpieceInstance) {
    this.headpieceSelected.next(selectedHeadpiece);
  }
}
