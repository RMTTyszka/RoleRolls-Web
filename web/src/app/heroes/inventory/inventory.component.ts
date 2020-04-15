import {Component, Input, OnInit} from '@angular/core';
import {FormGroup, FormGroupDirective} from '@angular/forms';
import {Inventory} from '../../shared/models/Inventory.model';

@Component({
  selector: 'loh-hero-inventory',
  templateUrl: './inventory.component.html',
  styleUrls: ['./inventory.component.css']
})
export class InventoryComponent implements OnInit {

  @Input() formGroupName = 'inventory'
  form: FormGroup;
  constructor(
    private formGroupDirective: FormGroupDirective
  ) {
  }

  ngOnInit() {
    this.form = this.formGroupDirective.form.get(this.formGroupName) as FormGroup;
  }
  get inventory(): Inventory {
    return this.form.value;
  }

}
