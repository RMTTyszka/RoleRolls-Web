import {Component, computed, forwardRef, input} from '@angular/core';
import {
  ControlValueAccessor,
  FormGroupDirective,
  FormsModule,
  NG_VALUE_ACCESSOR,
  ReactiveFormsModule
} from '@angular/forms';
import { SelectModule } from 'primeng/select';
import { RROption } from '@app/models/RROption';
import { PropertyType } from '@app/campaigns/models/propertyType';
import { Campaign } from '@app/campaigns/models/campaign';
import {
  AttributeTemplate, DefenseTemplate, VitalityTemplate,
  SpecificSkillsTemplate,
  SkillTemplate
} from '@app/campaigns/models/campaign.template';
import { Property } from '@app/models/bonuses/bonus';

@Component({
  selector: 'rr-property-selector',
  standalone: true,
  imports: [
    SelectModule, // Certifique-se de que SelectModule é compatível com formulários reativos
    ReactiveFormsModule, // Adicione ReactiveFormsModule para suporte a formulários reativos
    FormsModule // Adicione FormsModule para suporte a ngModel
  ],
  templateUrl: './property-selector.component.html',
  styleUrl: './property-selector.component.scss',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => PropertySelectorComponent),
      multi: true
    }
  ],
  host: {
    class: 'flex'
  }
})
export class PropertySelectorComponent implements ControlValueAccessor {
  public _value: Property | null = null;
  public campaign = input.required<Campaign>();
  public propertyType = input.required<PropertyType[]>();
  public placeholder = input<string>();
  public disabled: boolean = false;

  public properties = computed<RROption<Property>[]>(() => {
    const propertiesTypes = this.propertyType();
    let options: RROption<Property>[] = [];
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

  public attributes = computed<RROption<Property>[]>(() => {
    return this.campaign().campaignTemplate.attributes.map((a: AttributeTemplate) => ({
      label: a.name,
      value: { propertyId: a.id, type: PropertyType.Attribute }
    }));
  });

  public skills = computed<RROption<Property>[]>(() => {
    return this.campaign().campaignTemplate.skills.map((a: SkillTemplate) => ({
      label: a.name,
      value: { propertyId: a.id, type: PropertyType.Skill }
    }));
  });

  public specificSkills = computed<RROption<Property>[]>(() => {
    return this.campaign().campaignTemplate.skills.flatMap((s: SkillTemplate) =>
      s.specificSkills.map((a: SpecificSkillsTemplate) => ({
        label: a.name,
        value: { propertyId: a.id, type: PropertyType.SpecificSkill }
      }))
    ).concat(
      this.campaign().campaignTemplate.attributelessSkills.flatMap((s: SkillTemplate) =>
        s.specificSkills.map((a: SpecificSkillsTemplate) => ({
          label: a.name,
          value: { propertyId: a.id, type: PropertyType.SpecificSkill }
        }))
      )
    );
  });

  public defenses = computed<RROption<Property>[]>(() => {
    return this.campaign().campaignTemplate.defenses.map((a: DefenseTemplate) => ({
      label: a.name,
      value: { propertyId: a.id, type: PropertyType.Defense }
    }));
  });

  public vitalities = computed<RROption<Property>[]>(() => {
    return this.campaign().campaignTemplate.vitalities.map((a: VitalityTemplate) => ({
      label: a.name,
      value: { propertyId: a.id, type: PropertyType.Vitality }
    }));
  });

  constructor() {}

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
}
