import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormGroup, FormGroupDirective} from '@angular/forms';
import {createForm} from '../../shared/EditorExtension';
import {Inventory} from '../../shared/models/Inventory.model';
import isArmor from 'src/app/shared/utils/isItem';
import { ArmorInstance } from 'src/app/shared/models/ArmorInstance.model';

@Component({
  selector: 'loh-inventory-armor-select',
  templateUrl: './inventory-armor-select.component.html',
  styleUrls: ['./inventory-armor-select.component.css']
})
export class InventoryArmorSelectComponent implements OnInit {
  @Output() armorSelected = new EventEmitter<ArmorInstance>();
  inventoryForm: FormGroup;
  armorForm: FormGroup;
  equipmentForm: FormGroup;
  @Input() inventoryFormName = 'inventory';
  @Input() armorFormName = 'armor';
  @Input() equipmentFormName = 'equipment';
  result: string[] = [];
  entities: ArmorInstance[] = [];
  value: string;
  constructor(
    private formGroupDirective: FormGroupDirective  ) { }

  ngOnInit() {
    this.inventoryForm = this.formGroupDirective.form.get(this.inventoryFormName) as FormGroup;
    this.equipmentForm = this.formGroupDirective.form.get(this.equipmentFormName) as FormGroup;
    this.armorForm = this.equipmentForm.get(this.armorFormName) as FormGroup;
    this.entities = (this.inventoryForm.value as Inventory).items
    .filter(item => isArmor(item)) as ArmorInstance[];
  }

  search(filter: string) {
    this.result = this.entities
      .filter((item: ArmorInstance) => item.name.includes(filter)
        || item.armorModel.name.includes(filter)
        || item.armorModel.baseArmor.name.includes(filter))
      .map(item => item.name);
  }
  selected(entityName: string) {
    const selectedEntity = this.entities.find(e => e.name === entityName);
    const form = new FormGroup({});
    createForm(form , selectedEntity);
   // this.form.setControl(this.groupName + '.armor', form);
    this.armorSelected.emit(selectedEntity);
  }

}
