import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {GloveInstance} from '../../../shared/models/GloveInstance.model';
import {FormGroup} from '@angular/forms';
import {Inventory} from '../../../shared/models/Inventory.model';
import {isGlove} from '../../../shared/utils/isItem';

@Component({
  selector: 'loh-inventory-gloves-select',
  templateUrl: './inventory-gloves-select.component.html',
  styleUrls: ['./inventory-gloves-select.component.css']
})
export class InventoryGlovesSelectComponent implements OnInit {
  formName = 'glove';
  placeholder = 'Glove';
  @Output() gloveSelected = new EventEmitter<GloveInstance>();
  constructor() {}

  ngOnInit() {
  }
  filter = (inventoryForm: FormGroup) => (inventoryForm.value as Inventory).items
    .filter(item => isGlove(item)) as GloveInstance[]

  search = (filter: string, items: Array<GloveInstance>) => items
    .filter((item: GloveInstance) => item.name.includes(filter)
      || item.gloveModel.name.includes(filter))
    .map(item => item.name)

  itemSelected(selectedGlove: GloveInstance) {
    this.gloveSelected.next(selectedGlove);
  }
}
