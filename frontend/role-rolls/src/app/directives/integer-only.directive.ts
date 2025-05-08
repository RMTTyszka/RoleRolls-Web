import { Directive, ElementRef, HostListener } from '@angular/core';

@Directive({
  selector: '[rrIntegerOnly]'
})
export class IntegerOnlyDirective {

  private inputElement: HTMLInputElement | null = null;

  constructor(private el: ElementRef) { }

  ngAfterViewInit(): void {
    this.findInputElement();
  }

  private findInputElement(): void {
    const input = this.el.nativeElement.querySelector('input');
    if (input) {
      this.inputElement = input;
    } else {
      console.warn('IntegerOnlyDirective: Não encontrou um elemento <input> filho.');
    }
  }

  @HostListener('input', ['$event']) onInputChange(event: any) {
    if (this.inputElement) {
      const initialValue = this.inputElement.value;
      this.inputElement.value = initialValue.replace(/[^0-9]*/g, '');
      if (initialValue !== this.inputElement.value) {
        event.stopPropagation();
      }
    }
  }

}
