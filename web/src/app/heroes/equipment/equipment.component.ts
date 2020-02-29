import {Component, Input, OnInit} from '@angular/core';
import {FormGroup} from '@angular/forms';
import {createForm} from '../../shared/EditorExtension';
import {Equipment} from '../../shared/models/Equipment.model';
import {map, tap} from 'rxjs/operators';
import {Inventory} from '../../shared/models/Inventory.model';
import {ArmorInstance} from '../../shared/models/ArmorInstance.model';

@Component({
  selector: 'loh-equipment',
  templateUrl: './equipment.component.html',
  styleUrls: ['./equipment.component.css']
})
export class EquipmentComponent implements OnInit {

  @Input() form: FormGroup = this.createForm();
  @Input() controlName = 'equipment';
  @Input() inventoryControlName = 'inventory';
  entity: Equipment;
  constructor() { }

  ngOnInit() {
    this.entity = this.getEntity();
  }

  createForm() {
    const form = new FormGroup({});
    const entity = new Equipment();
    createForm(form, entity);
    return form;
  }

  getEntity() {
    const entity = this.form.get(this.controlName).value;
    return entity ? entity : new Equipment();
  }
  get inventory(): Inventory {
    return this.form.get(this.inventoryControlName).value;
  }

  armorSelected(armor: ArmorInstance) {
    const selectedEntity = this.inventory.items.find(r => r.id === armor.id);
    const form = new FormGroup({});
    createForm(form , selectedEntity);
    this.form.setControl(this.controlName + '.armor', form);
  }

}
