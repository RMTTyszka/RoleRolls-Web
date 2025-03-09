import {
  AfterContentInit,
  Component,
  ContentChild,
  ContentChildren,
  Input, QueryList,
  TemplateRef
} from '@angular/core';
import { NgForOf, NgTemplateOutlet } from '@angular/common';
import { InputText } from 'primeng/inputtext';
import { FieldTitleDirective } from '@app/components/form-field-wrapper/field-title.directive';

@Component({
  selector: 'rr-form-field-wrapper',
  imports: [
    NgTemplateOutlet,
    NgForOf,
    InputText
  ],
  templateUrl: './form-field-wrapper.component.html',
  styleUrl: './form-field-wrapper.component.scss'
})
export class FormFieldWrapperComponent implements AfterContentInit {
  @ContentChildren(FieldTitleDirective) fieldTitles: QueryList<FieldTitleDirective>; // Coleta os títulos e campos
  items: { title: string; field: any }[] = []; // Lista de títulos e campos

  ngAfterContentInit() {
    this.items = this.fieldTitles.map(directive => ({
      title: directive.title,
      field: directive.templateRef
    }));
  }
}
