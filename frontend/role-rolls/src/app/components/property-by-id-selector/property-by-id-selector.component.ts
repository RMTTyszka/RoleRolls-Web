import { Component, computed, forwardRef, input } from '@angular/core';
import { ControlValueAccessor, FormsModule, NG_VALUE_ACCESSOR, ReactiveFormsModule } from '@angular/forms';
import { PropertySelectorComponent } from '@app/components/property-selector/property-selector.component';
import { Property } from '@app/models/bonuses/bonus';
import { Campaign } from '@app/campaigns/models/campaign';
import { PropertyType } from '@app/campaigns/models/propertyType';
import { RROption } from '@app/models/RROption';
import {
  AttributeTemplate,
  DefenseTemplate,
  SkillTemplate,
  SpecificSkillsTemplate, VitalityTemplate
} from '@app/campaigns/models/campaign.template';
import { SelectModule } from 'primeng/select';

@Component({
  selector: 'rr-property-by-id-selector',
  imports: [
    SelectModule,
    ReactiveFormsModule,
    FormsModule
  ],
  templateUrl: './property-by-id-selector.component.html',
  styleUrl: './property-by-id-selector.component.scss',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => PropertyByIdSelectorComponent),
      multi: true
    }
  ],
  host: {
    class: 'flex'
  }
})
export class PropertyByIdSelectorComponent implements ControlValueAccessor {
  public _value: string | null = null;
  public campaign = input.required<Campaign>();
  public propertyType = input.required<PropertyType[]>();
  public placeholder = input<string>();
  public disabled: boolean = false;

  public properties = computed<RROption<string>[]>(() => {
    const propertiesTypes = this.propertyType();
    let options: RROption<string>[] = [];
    propertiesTypes.forEach(p => {
      switch (p) {
        case PropertyType.All:
          options = options.concat(
            this.attributes()
              .concat(this.skills())
              .concat(this.specificSkills())
              .concat(this.defenses())
              .concat(this.vitalities())
          );
          break;
        case PropertyType.Attribute:
          options = options.concat(this.attributes());
          break;
        case PropertyType.Skill:
          options = options.concat(this.skills());
          break;
        case PropertyType.SpecificSkill:
          options = options.concat(this.specificSkills());
          break;
        case PropertyType.Defense:
          options = options.concat(this.defenses());
          break;
        case PropertyType.Vitality:
          options = options.concat(this.vitalities());
          break;
      }
    });
    return options;
  });

  public attributes = computed<RROption<string>[]>(() => {
    return this.campaign().campaignTemplate.attributes.map((a: AttributeTemplate) => ({
      label: a.name,
      value: a.id
    }));
  });

  public skills = computed<RROption<string>[]>(() => {
    return this.campaign().campaignTemplate.skills.map((a: SkillTemplate) => ({
      label: a.name,
      value: a.id
    }));
  });

  public specificSkills = computed<RROption<string>[]>(() => {
    return this.campaign().campaignTemplate.skills.flatMap((s: SkillTemplate) =>
      s.specificSkills.map((a: SpecificSkillsTemplate) => ({
        label: a.name,
        value: a.id
      }))
    ).concat(
      this.campaign().campaignTemplate.attributelessSkills.flatMap((s: SkillTemplate) =>
        s.specificSkills.map((a: SpecificSkillsTemplate) => ({
          label: a.name,
          value: a.id
        }))
      )
    );
  });

  public defenses = computed<RROption<string>[]>(() => {
    return this.campaign().campaignTemplate.defenses.map((a: DefenseTemplate) => ({
      label: a.name,
      value: a.id
    }));
  });

  public vitalities = computed<RROption<string>[]>(() => {
    return this.campaign().campaignTemplate.vitalities.map((a: VitalityTemplate) => ({
      label: a.name,
      value: a.id
    }));
  });

  constructor() {}

  get value(): string | null {
    return this._value;
  }

  set value(val: string | null) {
    if (val !== this._value) {
      this._value = val;
      if (this.onChange) {
        this.onChange(val);
      }
      this.onTouched();
    }
  }

  onChange = (value: string | null) => {};
  onTouched = () => {};

  onInput(value: string): void {
    this.value = value;
    this.onChange(value);
    this.onTouched();
  }

  writeValue(value: string | null): void {
    this.value = value;
  }

  registerOnChange(fn: (value: string | null) => void): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }

  setDisabledState(isDisabled: boolean): void {
    this.disabled = isDisabled;
  }
}
