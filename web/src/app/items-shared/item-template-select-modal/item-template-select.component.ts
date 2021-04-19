import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormGroup, FormGroupDirective} from '@angular/forms';
import {ItemTemplateApiService} from '../item-template-provider/item-template-api.service';
import {map, tap} from 'rxjs/operators';
import {createForm} from '../../shared/EditorExtension';
import {ItemTemplate} from '../../shared/models/items/ItemTemplate';

@Component({
  selector: 'loh-item-template-select',
  templateUrl: './item-template-select.component.html',
  styleUrls: ['./item-template-select.component.css']
})
export class ItemTemplateSelectComponent implements OnInit {
  @Input() controlName: string;
  @Input() required: boolean;
  form: FormGroup;
  @Output() entitySelected = new EventEmitter<ItemTemplate>();
  constructor(
    private formGroupDirective: FormGroupDirective,
    private itemTemplateApiService: ItemTemplateApiService
  ) {
  }

  ngOnInit(): void {
    this.form = this.formGroupDirective.form;
  }

}
