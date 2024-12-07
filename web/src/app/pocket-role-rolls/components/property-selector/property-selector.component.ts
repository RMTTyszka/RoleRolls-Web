import {Component, computed, forwardRef, input} from '@angular/core';
import {PocketCampaignModel, PropertyType} from 'src/app/shared/models/pocket/campaigns/pocket.campaign.model';
import {
  AttributeTemplateModel,
  MinorSkillsTemplateModel,
  SkillTemplateModel
} from 'src/app/shared/models/pocket/creature-templates/creature-template.model';
import {RROption} from 'src/app/pocket-role-rolls/global/models/RROption';
import {DropdownModule} from 'primeng/dropdown';
import {ControlValueAccessor, FormGroupDirective, FormsModule, NG_VALUE_ACCESSOR} from '@angular/forms';

@Component({
  selector: 'rr-property-selector',
  standalone: true,
  imports: [
    DropdownModule,
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
  public value = '';
  public campaign = input.required<PocketCampaignModel>();
  public propertyType = input.required<PropertyType>();

  public properties = computed<RROption<string>[]>(() => {
    switch (this.propertyType()) {
      case PropertyType.All:
        return [];
      case PropertyType.Attribute:
        return this.attributes();
      case PropertyType.Skill:
        return this.skills();
      case PropertyType.MinorSkill:
        return this.minorSkills();
      case PropertyType.Defense:
        return this.defenses();
      case PropertyType.Life:
        return this.lifes();
      default:
        return [];
    }
  });

  public attributes = computed<RROption<string>[]>(() => {
    return this.campaign().creatureTemplate.attributes.map(a => {
      return {
        label: a.name,
        value: a.id
      } as RROption<string>;
    });
  });

  public skills = computed<RROption<string>[]>(() => {
    return this.campaign().creatureTemplate.skills.map(a => {
      return {
        label: a.name,
        value: a.id
      } as RROption<string>;
    });
  });
  public minorSkills = computed<RROption<string>[]>(() => {
    return this.campaign().creatureTemplate.skills.flatMap(s => [...s.minorSkills.map(a => {
      return {
        label: a.name,
        value: a.id
      } as RROption<string>;
    })]);
  });
  public defenses = computed<RROption<string>[]>(() => {
    return this.campaign().creatureTemplate.defenses.map(a => {
      return {
        label: a.name,
        value: a.id
      } as RROption<string>;
    });
  });
  public lifes = computed<RROption<string>[]>(() => {
    return this.campaign().creatureTemplate.lifes.map(a => {
      return {
        label: a.name,
        value: a.id
      } as RROption<string>;
    });
  });
  constructor(public form: FormGroupDirective) {

  }
  onChange = (value: string) => {};
  onTouched = () => {};

  // Função chamada quando o valor muda
  onInput(value: string): void {
    this.value = value;
    this.onChange(value); // Notifica o Angular sobre a mudança de valor
  }

  // Métodos do ControlValueAccessor
  writeValue(value: string): void {
    this.value = value; // Atualiza o valor no componente
  }

  registerOnChange(fn: (value: string) => void): void {
    this.onChange = fn; // Registra a função para mudanças de valor
  }

  registerOnTouched(fn: () => void): void {
    this.onTouched = fn; // Registra a função para indicar que o componente foi tocado
  }
}
