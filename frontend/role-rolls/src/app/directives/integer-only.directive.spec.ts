import { ElementRef } from '@angular/core';
import { IntegerOnlyDirective } from './integer-only.directive';

describe('IntegerOnlyDirective', () => {
  it('should create an instance', () => {
    const directive = new IntegerOnlyDirective(new ElementRef(document.createElement('div')));
    expect(directive).toBeTruthy();
  });
});
