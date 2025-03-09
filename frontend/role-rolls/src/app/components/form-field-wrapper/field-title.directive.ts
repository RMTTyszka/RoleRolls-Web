import { Directive, Input, TemplateRef } from '@angular/core';

@Directive({
  selector: '[fieldTitle]'
})
export class FieldTitleDirective {
  @Input('fieldTitle') title: string; // Título do campo
  constructor(public templateRef: TemplateRef<any>) {} // Referência ao template do campo
}
