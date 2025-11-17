import { CommonModule } from '@angular/common';
import {
  Component,
  EventEmitter,
  forwardRef,
  Input,
  OnChanges,
  Output,
  SimpleChanges
} from '@angular/core';
import {
  ControlValueAccessor,
  FormsModule,
  NG_VALUE_ACCESSOR
} from '@angular/forms';
import { ButtonDirective } from 'primeng/button';
import { InputText } from 'primeng/inputtext';
import { SelectModule } from 'primeng/select';
import { PropertySelectorComponent } from '@app/components/property-selector/property-selector.component';
import { Campaign } from '@app/campaigns/models/campaign';
import {
  FormulaCustomValue,
  FormulaToken,
  FormulaTokenType
} from '@app/campaigns/models/formula-token.model';
import { PropertyType } from '@app/campaigns/models/propertyType';
import { Property } from '@app/models/bonuses/bonus';

@Component({
  selector: 'rr-formula-builder',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ButtonDirective,
    InputText,
    SelectModule,
    PropertySelectorComponent
  ],
  templateUrl: './formula-builder.component.html',
  styleUrl: './formula-builder.component.scss',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => FormulaBuilderComponent),
      multi: true
    }
  ]
})
export class FormulaBuilderComponent implements ControlValueAccessor, OnChanges {
  @Input({ required: true }) campaign!: Campaign;
  @Input() disabled = false;
  @Output() expressionChange = new EventEmitter<string>();
  @Output() descriptionChange = new EventEmitter<string>();

  public tokens: FormulaToken[] = [];
  public readonly PropertyType = PropertyType;
  public readonly FormulaTokenType = FormulaTokenType;

  public tokenTypeOptions = [
    { label: 'Propriedade', value: FormulaTokenType.Property },
    { label: 'Numero', value: FormulaTokenType.Number },
    { label: 'Operador', value: FormulaTokenType.Operator },
    { label: 'Valor customizado', value: FormulaTokenType.CustomValue }
  ];

  public operatorOptions = [
    { label: '+', value: '+' },
    { label: '-', value: '-' },
    { label: 'x', value: '*' },
    { label: '/', value: '/' },
    { label: '(', value: '(' },
    { label: ')', value: ')' }
  ];

  public customValueOptions = [
    { label: 'Bonus da Armadura', value: FormulaCustomValue.ArmorDefenseBonus },
    { label: 'Nivel', value: FormulaCustomValue.Level }
  ];

  private onChange: (value: FormulaToken[]) => void = () => {};
  private onTouched: () => void = () => {};

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['disabled']) {
      this.setDisabledState(this.disabled);
    }
  }

  writeValue(value: FormulaToken[] | null): void {
    if (!value) {
      this.tokens = [];
      this.emitPreview();
      return;
    }
    this.tokens = value
      .slice()
      .sort((a, b) => a.order - b.order)
      .map(token => ({ ...token }));
    this.emitPreview();
  }

  registerOnChange(fn: (value: FormulaToken[]) => void): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }

  setDisabledState(isDisabled: boolean): void {
    this.disabled = isDisabled;
  }

  public addToken(type: FormulaTokenType = FormulaTokenType.Property): void {
    if (this.disabled) {
      return;
    }
    const baseToken: FormulaToken = {
      order: this.tokens.length,
      type
    };
    switch (type) {
      case FormulaTokenType.Number:
        baseToken.value = 0;
        break;
      case FormulaTokenType.Operator:
        baseToken.operator = '+';
        break;
      case FormulaTokenType.CustomValue:
        baseToken.customValue = FormulaCustomValue.Level;
        break;
      case FormulaTokenType.Property:
      default:
        baseToken.property = null;
        break;
    }
    this.tokens = [...this.tokens, baseToken];
    this.emitChange();
  }

  public removeToken(index: number): void {
    if (this.disabled) {
      return;
    }
    this.tokens = this.tokens.filter((_, i) => i !== index);
    this.emitChange();
  }

  public moveToken(index: number, direction: number): void {
    if (this.disabled) {
      return;
    }
    const target = index + direction;
    if (target < 0 || target >= this.tokens.length) {
      return;
    }
    const newTokens = [...this.tokens];
    const [token] = newTokens.splice(index, 1);
    newTokens.splice(target, 0, token);
    this.tokens = newTokens;
    this.emitChange();
  }

  public onTokenTypeChanged(index: number, token: FormulaTokenType): void {
    if (this.disabled) {
      return;
    }
    const current = this.tokens[index];
    current.type = token;
    current.property = null;
    current.operator = null;
    current.customValue = null;
    current.value = null;
    switch (token) {
      case FormulaTokenType.Number:
        current.value = 0;
        break;
      case FormulaTokenType.Operator:
        current.operator = '+';
        break;
      case FormulaTokenType.CustomValue:
        current.customValue = FormulaCustomValue.Level;
        break;
      case FormulaTokenType.Property:
      default:
        current.property = null;
        break;
    }
    this.emitChange();
  }

  public onTokenEdited(): void {
    this.emitChange();
  }

  public trackByIndex(index: number): number {
    return index;
  }

  public get descriptionPreview(): string {
    if (!this.tokens.length) {
      return '';
    }
    return this.tokens
      .map(token => this.describeToken(token))
      .join(' ')
      .replace(/\s+/g, ' ')
      .trim();
  }

  public get expressionPreview(): string {
    if (!this.tokens.length) {
      return '';
    }
    return this.tokens
      .map(token => this.resolveTokenSymbol(token))
      .join('')
      .replace(/\s+/g, ' ')
      .trim();
  }

  private emitChange(): void {
    this.tokens = this.tokens.map((token, index) => ({
      ...token,
      order: index
    }));
    this.onChange(this.tokens.map(token => ({ ...token })));
    this.onTouched();
    this.emitPreview();
  }

  private describeToken(token: FormulaToken): string {
    switch (token.type) {
      case FormulaTokenType.Number:
        return (token.value ?? 0).toString();
      case FormulaTokenType.Operator:
        return token.operator ?? '';
      case FormulaTokenType.CustomValue:
        return this.getCustomValueLabel(token.customValue);
      case FormulaTokenType.Property:
        return this.getPropertyLabel(token.property);
      default:
        return '';
    }
  }

  private resolveTokenSymbol(token: FormulaToken): string {
    switch (token.type) {
      case FormulaTokenType.Number:
        return (token.value ?? 0).toString();
      case FormulaTokenType.Operator:
        return ` ${token.operator ?? ''} `;
      case FormulaTokenType.CustomValue:
        return this.getCustomValueLabel(token.customValue);
      case FormulaTokenType.Property:
        return this.getPropertyLabel(token.property);
      default:
        return '';
    }
  }

  private getCustomValueLabel(customValue?: FormulaCustomValue | null): string {
    switch (customValue) {
      case FormulaCustomValue.ArmorDefenseBonus:
        return 'Bonus Armadura';
      case FormulaCustomValue.Level:
        return 'Nivel';
      default:
        return 'Custom';
    }
  }

  private getPropertyLabel(property?: Property | null): string {
    if (!property || !this.campaign) {
      return 'Propriedade';
    }
    const template = this.campaign.campaignTemplate;
    const findById = <T extends { id: string; name: string }>(items: T[]): string | undefined =>
      items.find(item => item.id === property.id)?.name;

    switch (property.type) {
      case PropertyType.Attribute:
        return findById(template.attributes) ?? 'Atributo';
      case PropertyType.Skill:
        return findById(template.skills) ?? 'Pericia';
      case PropertyType.SpecificSkill:
        return template.skills
          .flatMap(s => s.specificSkillTemplates)
          .find(ss => ss.id === property.id)?.name ?? 'Especializacao';
      case PropertyType.Defense:
        return findById(template.defenses) ?? 'Defesa';
      case PropertyType.Vitality:
        return findById(template.vitalities) ?? 'Vitalidade';
      default:
        return 'Propriedade';
    }
  }

  private emitPreview(): void {
    this.expressionChange.emit(this.expressionPreview);
    this.descriptionChange.emit(this.descriptionPreview);
  }
}
