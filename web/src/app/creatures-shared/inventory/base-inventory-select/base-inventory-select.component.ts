import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormGroup, FormGroupDirective} from '@angular/forms';
import {createForm} from 'src/app/shared/EditorExtension';
import {ItemInstance} from 'src/app/shared/models/ItemInstance.model';

@Component({
  selector: 'rr-base-inventory-select',
  templateUrl: './base-inventory-select.component.html',
  styleUrls: ['./base-inventory-select.component.css']
})
export class BaseInventorySelectComponent implements OnInit {
  @Output() itemSelected = new EventEmitter<ItemInstance>();
  inventoryForm: FormGroup;
  itemForm: FormGroup;
  equipmentForm: FormGroup;
  @Input() inventoryFormName = 'inventory';
  @Input() equipmentFormName = 'equipment';
  @Input() placeholder: string;
  @Input() itemFormName: string;
  @Input() filter: (inventoryForm: FormGroup) => Array<ItemInstance>;
  get items(): ItemInstance[] {
    return this.filter(this.inventoryForm);
  }
  options: ItemInstance[] = [];
  constructor(
    protected formGroupDirective: FormGroupDirective  ) { }

  ngOnInit() {
    this.inventoryForm = this.formGroupDirective.form.get(this.inventoryFormName) as FormGroup;
    this.equipmentForm = this.formGroupDirective.form.get(this.equipmentFormName) as FormGroup;
    this.itemForm = this.equipmentForm.get(this.itemFormName) as FormGroup;
    this.options = this.filter(this.inventoryForm);
    this.options.push(this.itemForm.value);
  }

  selected(entityId: string) {
    const selectedEntity = this.items.find(e => e.id === entityId);
    this.itemForm.patchValue(selectedEntity);
    this.itemSelected.emit(selectedEntity);
  }
}
