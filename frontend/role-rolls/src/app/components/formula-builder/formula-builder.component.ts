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

const LEGACY_NUMBER_TYPE = 1 as FormulaTokenType;
const LEGACY_OPERATOR_TYPE = 2 as FormulaTokenType;

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
    { label: 'Valor customizado', value: FormulaTokenType.CustomValue },
    { label: 'Valor manual', value: FormulaTokenType.Manual }
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
      .map(token => this.normalizeToken({ ...token }));
    this.emitPreview();
  }

  private normalizeToken(token: FormulaToken): FormulaToken {
    if (token.type === LEGACY_NUMBER_TYPE) {
      return {
        ...token,
        type: FormulaTokenType.Manual,
        manualValue: token.value?.toString() ?? '',
        value: null
      };
    }
    if (token.type === LEGACY_OPERATOR_TYPE) {
      return {
        ...token,
        type: FormulaTokenType.Manual,
        manualValue: token.operator ?? '',
        operator: null
      };
    }
    if (token.type === FormulaTokenType.Manual) {
      return {
        ...token,
        manualValue: token.manualValue ?? ''
      };
    }
    if (token.type === FormulaTokenType.Property || token.type === FormulaTokenType.CustomValue) {
      return {
        ...token,
        manualValue: token.manualValue ?? ''
      };
    }
    return token;
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

  public addToken(): void {
    if (this.disabled) {
      return;
    }
    const baseToken: FormulaToken = {
      order: this.tokens.length,
      type: FormulaTokenType.Property,
      property: null,
      manualValue: ''
    };
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
    if (!current) {
      return;
    }
    current.type = token;
    current.property = null;
    current.operator = null;
    current.customValue = null;
    current.value = null;
    current.manualValue = null;
    switch (token) {
      case FormulaTokenType.CustomValue:
        current.customValue = FormulaCustomValue.Level;
        current.manualValue = '';
        break;
      case FormulaTokenType.Manual:
        current.manualValue = '';
        break;
      case FormulaTokenType.Property:
      default:
        current.property = null;
        current.manualValue = '';
        break;
    }
    this.emitChange();
  }

  public onTokenEdited(): void {
    this.emitChange();
  }

  public onManualValueBlur(): void {
    if (this.disabled) {
      return;
    }
    this.emitChange();
  }

  public trackByIndex(index: number): number {
    return index;
  }

  public get descriptionTokens(): { label: string; value: string }[] {
    return this.tokens.map(token => ({
      label: this.getTokenTypeLabel(token.type),
      value: this.describeToken(token)
    }));
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
    this.tokens = this.tokens.map((token, index) =>
      this.normalizeToken({
        ...token,
        order: index
      })
    );
    this.onChange(this.tokens.map(token => ({ ...token })));
    this.onTouched();
    this.emitPreview();
  }

  private describeToken(token: FormulaToken): string {
    const appendManual = (text: string, manual?: string | null): string => {
      const manualText = manual ?? '';
      if (!manualText.trim()) {
        return text;
      }
      return `${text} ${manualText}`;
    };
    switch (token.type) {
      case FormulaTokenType.CustomValue:
        return appendManual(this.getCustomValueLabel(token.customValue), token.manualValue);
      case FormulaTokenType.Property:
        return appendManual(this.getPropertyLabel(token.property), token.manualValue);
      case FormulaTokenType.Manual:
        return token.manualValue ?? '';
      default:
        return this.resolveLegacyTokenDescription(token);
    }
  }

  private resolveTokenSymbol(token: FormulaToken): string {
    const appendManual = (text: string, manual?: string | null): string => {
      const manualText = manual ?? '';
      if (!manualText.trim()) {
        return text;
      }
      return `${text}${manualText}`;
    };
    switch (token.type) {
      case FormulaTokenType.CustomValue:
        return appendManual(this.getCustomValueLabel(token.customValue), token.manualValue);
      case FormulaTokenType.Property:
        return appendManual(this.getPropertyLabel(token.property), token.manualValue);
      case FormulaTokenType.Manual:
        return token.manualValue ?? '';
      default:
        return this.resolveLegacyTokenSymbol(token);
    }
  }

  private resolveLegacyTokenDescription(token: FormulaToken): string {
    if (token.value !== null && token.value !== undefined) {
      return token.value.toString();
    }
    if (token.operator) {
      return ` ${token.operator} `;
    }
    return token.manualValue ?? '';
  }

  private resolveLegacyTokenSymbol(token: FormulaToken): string {
    if (token.value !== null && token.value !== undefined) {
      return token.value.toString();
    }
    if (token.operator) {
      return ` ${token.operator} `;
    }
    return token.manualValue ?? '';
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

  private getTokenTypeLabel(type: FormulaTokenType): string {
    switch (type) {
      case FormulaTokenType.Property:
        return 'Propriedade';
      case FormulaTokenType.CustomValue:
        return 'Valor customizado';
      case FormulaTokenType.Manual:
        return 'Valor manual';
      default:
        return 'Token';
    }
  }

  private emitPreview(): void {
    this.expressionChange.emit(this.expressionPreview);
    this.descriptionChange.emit(this.descriptionPreview);
  }
}
