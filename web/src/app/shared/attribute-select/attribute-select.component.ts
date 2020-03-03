import { Component, OnInit, Input } from '@angular/core';
import { DataService } from '../data.service';
import { SelectItem } from 'primeng/api';
import { FormGroup, FormGroupDirective } from '@angular/forms';

@Component({
  selector: 'loh-attribute-select',
  templateUrl: './attribute-select.component.html',
  styleUrls: ['./attribute-select.component.css']
})
export class AttributeSelectComponent implements OnInit {
  isLoading = true;
  form: FormGroup;
  @Input() placeholder = 'Attribute';
  @Input() controlName = 'attribute';
  options: Array<SelectItem> = [];
  constructor(
    private attributeService: DataService,
    private formGroupDirective: FormGroupDirective
  ) {
    this.attributeService.getAllAttributes().toPromise().then(attributes => {
      this.options = attributes.map(attr => <SelectItem> {label: attr, value: attr});
    });
   }

  ngOnInit() {
    this.form = this.formGroupDirective.form;
    this.isLoading = false;
  }

}
