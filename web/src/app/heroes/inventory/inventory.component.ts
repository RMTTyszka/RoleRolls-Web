import {Component, Input, OnInit} from '@angular/core';
import {FormArray, FormBuilder, FormGroup, FormGroupDirective} from '@angular/forms';
import {Inventory} from '../../shared/models/Inventory.model';
import {HeroManagementService} from '../hero-management.service';
import {createForm} from '../../shared/EditorExtension';
import {ItemInstance} from '../../shared/models/ItemInstance.model';
import {EquipableInstance} from '../../shared/models/EquipableInstance.model';

@Component({
  selector: 'loh-hero-inventory',
  templateUrl: './inventory.component.html',
  styleUrls: ['./inventory.component.css']
})
export class InventoryComponent implements OnInit {

  @Input() formGroupName = 'inventory'
  form: FormGroup;
  constructor(
    private formGroupDirective: FormGroupDirective,
    private fb: FormBuilder,
    private heroManagementService: HeroManagementService
  ) {
    this.heroManagementService.addItemToinventory.subscribe(item => {
      const itemForm = new FormGroup({});
      createForm(itemForm, item);
      (this.form.get('items') as FormArray).push(itemForm);
    });
    this.heroManagementService.updateFunds.subscribe(value => {
      let funds = this.form.get('cash1').value;
      funds -= value;
      this.form.get('cash1').setValue(funds);
    });
  }

  ngOnInit() {
    this.form = this.formGroupDirective.form.get(this.formGroupName) as FormGroup;
  }
  get inventory(): Inventory {
    return this.form.value;
  }

  showItem(item: ItemInstance) {
    return item.equipable && (item as EquipableInstance).removable || !item.equipable;
  }
}
