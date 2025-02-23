import {Component, computed, forwardRef, input} from '@angular/core';
import {ControlValueAccessor, FormGroupDirective, FormsModule, NG_VALUE_ACCESSOR} from '@angular/forms';
import { SelectModule } from 'primeng/select';
import { RROption } from '@app/models/RROption';
import { PropertyType } from '@app/campaigns/models/propertyType';
import { Campaign } from '@app/campaigns/models/campaign';
import {
  AttributeTemplate, DefenseTemplate, LifeTemplate,
  MinorSkillsTemplate,
  SkillTemplate
} from '@app/campaigns/models/campaign.template';

@Component({
  selector: 'rr-property-selector',
  standalone: true,
  imports: [
    SelectModule,
    FormsModule
  ],
  templateUrl: './property-selector.component.html',
  styleUrl: './property-selector.component.scss',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => PropertySelectorComponent),
      multi: true
    }
  ]
})
export class PropertySelectorComponent implements ControlValueAccessor {
  public _value = '';
  public campaign = input.required<Campaign>();
  public propertyType = input.required<PropertyType[]>();
  public placeholder = input<string>();
  public disabled: boolean;

  public properties = computed<RROption<string>[]>(() => {
    const propertiesTypes = this.propertyType();
    let options: RROption<string>[] = [];
    propertiesTypes.forEach(p => {
      switch (p) {
        case PropertyType.All:
          options = options.concat(this.attributes()
            .concat(this.skills()
              .concat(this.minorSkills()
                .concat(this.defenses()
                  .concat(this.lifes())))));
          break;
        case PropertyType.Attribute:
          options = options.concat(this.attributes());
          break;
        case PropertyType.Skill:
          options = options.concat(this.skills());
          break;
        case PropertyType.MinorSkill:
          options = options.concat(this.minorSkills());
          break;
        case PropertyType.Defense:
          options = options.concat(this.defenses());
          break;
        case PropertyType.Life:
          options = options.concat(this.lifes());
          break;
        default:
          break;
      }
    });
    return options;
  });

  public attributes = computed<RROption<string>[]>(() => {
    return this.campaign().campaignTemplate.attributes.map((a: AttributeTemplate) => {
      return {
        label: a.name,
        value: a.id
      } as RROption<string>;
    });
  });

  public skills = computed<RROption<string>[]>(() => {
    return this.campaign().campaignTemplate.skills.map((a: SkillTemplate) => {
      return {
        label: a.name,
        value: a.id
      } as RROption<string>;
    });
  });
  public minorSkills = computed<RROption<string>[]>(() => {
    return this.campaign().campaignTemplate.skills.flatMap((s: SkillTemplate) =>
      s.minorSkills.map((a: MinorSkillsTemplate) => ({
        label: a.name,
        value: a.id
      } as RROption<string>))
    ).concat(
      this.campaign().campaignTemplate.attributelessSkills.flatMap((s: SkillTemplate) =>
        s.minorSkills.map((a: MinorSkillsTemplate) => ({
          label: a.name,
          value: a.id
        } as RROption<string>))
      )
    );
  });
  public defenses = computed<RROption<string>[]>(() => {
    return this.campaign().campaignTemplate.defenses.map((a: DefenseTemplate) => {
      return {
        label: a.name,
        value: a.id
      } as RROption<string>;
    });
  });
  public lifes = computed<RROption<string>[]>(() => {
    return this.campaign().campaignTemplate.lifes.map((a: LifeTemplate) => {
      return {
        label: a.name,
        value: a.id
      } as RROption<string>;
    });
  });
  constructor(public form: FormGroupDirective) {

  }
  get value(): string {
    return this._value;
  }

  set value(val: string) {
    if (val !== this._value) {
      this._value = val;
      if (this.onChange) {
        this.onChange(val);
      }
      this.onTouched();
    }
  }
  onChange = (value: string) => {
  }
  onTouched = () => {};

  onInput(value: string): void {
    this.value = value;
    this.onChange(value);
    this.onTouched();
  }

  writeValue(value: string): void {
    this.value = value;
  }

  registerOnChange(fn: (value: string) => void): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }
  setDisabledState(isDisabled: boolean): void {
    this.disabled = isDisabled;
  }

}
