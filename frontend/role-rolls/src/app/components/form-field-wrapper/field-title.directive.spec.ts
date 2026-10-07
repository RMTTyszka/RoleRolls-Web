import { TemplateRef } from '@angular/core';
import { FieldTitleDirective } from './field-title.directive';

describe('FieldTitleDirective', () => {
  it('should create an instance', () => {
    const directive = new FieldTitleDirective({} as TemplateRef<unknown>);
    expect(directive).toBeTruthy();
  });
});
