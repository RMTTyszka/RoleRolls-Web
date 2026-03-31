import {Component, computed, forwardRef, input} from '@angular/core';
import {
  ControlValueAccessor,
  FormsModule,
  NG_VALUE_ACCESSOR,
  ReactiveFormsModule
} from '@angular/forms';
import { SelectModule } from 'primeng/select';
import { RROption } from '@app/models/RROption';
import { PropertyType } from '@app/campaigns/models/propertyType';
import { Campaign } from '@app/campaigns/models/campaign';
import {
  AttributeTemplate, CreatureCondition, DefenseTemplate, VitalityTemplate,
  SpecificSkillsTemplate,
  SkillTemplate
} from '@app/campaigns/models/campaign.template';
import { Property } from '@app/models/bonuses/bonus';
import {Creature} from '@app/campaigns/models/creature';

@Component({
  selector: 'rr-property-selector',
  standalone: true,
  imports: [
    SelectModule,
    ReactiveFormsModule,
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
  ],
  host: {
    class: 'flex'
  }
})
export class PropertySelectorComponent implements ControlValueAccessor {
  public _value: Property | null = null;
  public campaign = input<Campaign>();
  public creature = input<Creature>();
  public propertyType = input.required<PropertyType[]>();
  public placeholder = input<string>();
  public cleanable = input<boolean>();
  public required = input<boolean>();
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
              .concat(this.creatureConditions())
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
        case PropertyType.CreatureCondition:
          options = options.concat(this.creatureConditions());
          break;
      }
    });
    return options;
  });

  public attributes = computed<RROption<Property>[]>(() => {
    const attributes = this.campaign()?.campaignTemplate?.attributes ?? [];
    return attributes.map((a: AttributeTemplate) => ({
      label: a.name,
      value: { id: a.id, type: PropertyType.Attribute }
    }));
  });

  public skills = computed<RROption<Property>[]>(() => {
    const skills = this.campaign()?.campaignTemplate?.skills ?? [];
    return skills.map((a: SkillTemplate) => ({
      label: a.name,
      value: { id: a.id, type: PropertyType.Skill }
    }));
  });

  public specificSkills = computed<RROption<Property>[]>(() => {
    const skills = this.campaign()?.campaignTemplate?.skills ?? [];
    return skills.flatMap((s: SkillTemplate) =>
      s.specificSkillTemplates.map((a: SpecificSkillsTemplate) => ({
        label: a.name,
        value: { id: a.id, type: PropertyType.SpecificSkill }
      }))
    );
  });

  public defenses = computed<RROption<Property>[]>(() => {
    const defenses = this.campaign()?.campaignTemplate?.defenses ?? [];
    return defenses.map((a: DefenseTemplate) => ({
      label: a.name,
      value: { id: a.id, type: PropertyType.Defense }
    }));
  });

  public vitalities = computed<RROption<Property>[]>(() => {
    const vitalities = this.campaign()?.campaignTemplate?.vitalities ?? [];
    return vitalities.map((a: VitalityTemplate) => ({
      label: a.name,
      value: { id: a.id, type: PropertyType.Vitality }
    }));
  });

  public creatureConditions = computed<RROption<Property>[]>(() => {
    const creatureConditions = this.campaign()?.campaignTemplate?.creatureConditions ?? [];
    return creatureConditions.map((a: CreatureCondition) => ({
      label: a.name,
      value: { id: a.id, type: PropertyType.CreatureCondition }
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
