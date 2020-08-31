import {Component, Input, OnInit} from '@angular/core';
import {FormArray, FormBuilder, FormGroup, FormGroupDirective} from '@angular/forms';
import {Inventory} from '../../shared/models/Inventory.model';
import {HeroManagementService} from '../hero-management.service';
import {createForm} from '../../shared/EditorExtension';

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
  }

  ngOnInit() {
    this.form = this.formGroupDirective.form.get(this.formGroupName) as FormGroup;
  }
  get inventory(): Inventory {
    return this.form.value;
  }

}
