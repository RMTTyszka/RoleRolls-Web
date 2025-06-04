import { Component, forwardRef, model } from '@angular/core';
import { SelectButton, SelectButtonModule } from "primeng/selectbutton";
import { Property } from '@app/models/bonuses/bonus';
import { RROption } from '@app/models/RROption';
import { ControlValueAccessor, FormsModule, NG_VALUE_ACCESSOR, ReactiveFormsModule } from '@angular/forms';
import { AdvantageSelectComponent } from '@app/rolls/advantage-select/advantage-select.component';

@Component({
  selector: 'rr-luck-select',
  imports: [SelectButtonModule, FormsModule, ReactiveFormsModule],

  templateUrl: './luck-select.component.html',
  styleUrl: './luck-select.component.scss',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => LuckSelectComponent),
      multi: true
    }
  ],
})
export class LuckSelectComponent implements ControlValueAccessor {
  public _value: Property | null = null;
  public disabled: boolean = false;
  public options = [
    {
      label: '- V',
      value: -5,
      csClass: 'danger-color'
    } as RROption<number>,
    {
      label: '- IV',
      value: -4
    } as RROption<number>,
    {
      label: '- III',
      value: -3
    } as RROption<number>,
    {
      label: '- II',
      value: -2
    } as RROption<number>,
    {
      label: '- I',
      value: -1
    } as RROption<number>,
    {
      label: 'Normal',
      value: 0
    } as RROption<number>,
    {
      label: '+ I',
      value: 1
    } as RROption<number>,
    {
      label: '+ II',
      value: 2
    } as RROption<number>,
    {
      label: '+ III',
      value: 3
    } as RROption<number>,
    {
      label: '+ IV',
      value: 4
    } as RROption<number>,
    {
      label: '+ V',
      value: 5
    } as RROption<number>
  ];

  get value(): Property | null {
    return this._value;
  }

  set value(val: Property | null) {
    if (val !== this._value) {
      this._value = val;
      if (this.onChange) {
        this.onChange(val);
      }
      this.onTouched();
    }
  }

  onChange = (value: Property | null) => {};
  onTouched = () => {};

  onInput(value: Property): void {
    this.value = value;
    this.onChange(value);
    this.onTouched();
  }

  writeValue(value: Property | null): void {
    this.value = value;
  }

  registerOnChange(fn: (value: Property | null) => void): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }

  setDisabledState(isDisabled: boolean): void {
    this.disabled = isDisabled;
  }

  protected readonly model = model;
}
