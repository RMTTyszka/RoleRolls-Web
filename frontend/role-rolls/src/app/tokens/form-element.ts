import { FormManipulator } from './form-manipulator';
import { Output, EventEmitter, Input, Directive } from '@angular/core';

@Directive()
export class FormElementDirective extends FormManipulator {

  @Input() public readonly: boolean;
  @Input() public maxLength: number;
  @Input() public required: boolean;
  @Input() public errorMessage: string;

  @Output() clickEvent: EventEmitter<any> = new EventEmitter();
  @Output() keyEnterEvent: EventEmitter<any> = new EventEmitter();
  @Output() blurEvent: EventEmitter<any> = new EventEmitter();
  @Output() focusEvent: EventEmitter<any> = new EventEmitter();

  public onClick(event?: any): void {
    this.clickEvent.emit(event);
  }

  public onKeyEnter(option?: any): void {
    this.keyEnterEvent.emit(option);
  }

  public onBlur(option?: any): void {
    this.blurEvent.emit(option);
    this.onTouch();
  }

  public onFocus(option?: any): void {
    this.focusEvent.emit(option);
  }
}
