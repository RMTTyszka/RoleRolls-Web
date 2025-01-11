import { ControlValueAccessor } from '@angular/forms';

export abstract class FormManipulator implements ControlValueAccessor {

  private internalValue: any;

  public disabled = false;

  public get value(): any {
    return this.internalValue;
  }

  public set value(val: any) {
    this.internalValue = val;
    this.onChange(val);
  }

  /**
   * Returns normalized value for comparison or correction purposes.
   */
  protected normalize(value: any): any {
    return value;
  }

  /**
   * Evaluates if the newly assigned value is different from the previous one.
   *
   * To add additional validation (e.g. masks, dates), implement this method
   * and call `super.isNewValueDifferent(value)`.
   */
  protected isNewValueDifferent(value: any): boolean {
    return this.normalize(value) !== this.normalize(this.internalValue);
  }

  /**
   * Sync FormControl value with on-screen value
   */
  protected setValue(value: any): void {
    if (this.isNewValueDifferent(value)) {
      this.internalValue = this.normalize(value);
    }
  }

  // ControlValueAccessor

  public writeValue(value: any): void {
    this.setValue(value);
  }

  public setDisabledState(disabled: boolean): void {
    this.disabled = disabled;
  }

  protected onChange: any = () => {};
  public onTouch: any = () => {};

  // Default: Only to make angular works fine
  public registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  public registerOnTouched(fn: any): void {
    this.onTouch = fn;
  }

}
