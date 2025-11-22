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
  FormulaCreatureValue,
  FormulaEquipmentValue,
  FormulaToken,
  FormulaTokenType
} from '@app/campaigns/models/formula-token.model';
import { PropertyType } from '@app/campaigns/models/propertyType';
import { Property } from '@app/models/bonuses/bonus';
import { EquipableSlot } from '@app/models/itens/equipable-slot';

const LEGACY_NUMBER_TYPE = 1 as FormulaTokenType;
const LEGACY_OPERATOR_TYPE = 2 as FormulaTokenType;

type EquipmentValueOption = {
  key: string;
  label: string;
  slot: EquipableSlot;
  value: FormulaEquipmentValue;
};

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
    { label: 'Valor da criatura', value: FormulaTokenType.Creature },
    { label: 'Valor de equipamento', value: FormulaTokenType.Equipment },
    { label: 'Valor manual', value: FormulaTokenType.Manual }
  ];

  public creatureValueOptions = [{ label: 'Nivel', value: FormulaCreatureValue.Level }];
  public equipmentPropertyOptions: EquipmentValueOption[] = this.buildEquipmentValueOptions();

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
    if (token.type === FormulaTokenType.Property) {
      return {
        ...token,
        manualValue: token.manualValue ?? ''
      };
    }
    if (token.type === FormulaTokenType.Creature) {
      const normalized: FormulaToken = {
        ...token,
        customValue: token.customValue ?? FormulaCreatureValue.Level,
        manualValue: token.manualValue ?? '',
        equipmentSlot: null,
        equipmentValue: null
      };
      if (
        normalized.customValue !== null &&
        normalized.customValue !== undefined &&
        normalized.customValue !== FormulaCreatureValue.Level
      ) {
        return this.convertLegacyCreatureToken(normalized);
      }
      return normalized;
    }
    if (token.type === FormulaTokenType.Equipment) {
      const slot = token.equipmentSlot ?? EquipableSlot.Chest;
      return {
        ...token,
        equipmentSlot: slot,
        equipmentValue: token.equipmentValue ?? this.getDefaultEquipmentValue(slot),
        manualValue: token.manualValue ?? ''
      };
    }
    return {
      ...token,
      manualValue: token.manualValue ?? ''
    };
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
    current.manualValue = '';
    current.equipmentSlot = null;
    current.equipmentValue = null;
    switch (token) {
      case FormulaTokenType.Creature:
        current.customValue = FormulaCreatureValue.Level;
        break;
      case FormulaTokenType.Equipment:
        current.equipmentSlot = EquipableSlot.Chest;
        current.equipmentValue = this.getDefaultEquipmentValue(EquipableSlot.Chest);
        break;
      case FormulaTokenType.Property:
      default:
        current.property = null;
        break;
    }
    this.emitChange();
  }

  public onEquipmentOptionChanged(index: number, key: string | null): void {
    if (this.disabled) {
      return;
    }
    const current = this.tokens[index];
    if (!current) {
      return;
    }
    const option = this.equipmentPropertyOptions.find(opt => opt.key === key);
    if (!option) {
      return;
    }
    current.equipmentSlot = option.slot;
    current.equipmentValue = option.value;
    this.emitChange();
  }

  public getEquipmentOptionKey(token: FormulaToken): string | null {
    if (token.equipmentSlot === null || token.equipmentSlot === undefined) {
      return null;
    }
    const option = this.equipmentPropertyOptions.find(
      opt => opt.slot === token.equipmentSlot && opt.value === token.equipmentValue
    );
    return option?.key ?? null;
  }

  private getDefaultEquipmentValue(slot: EquipableSlot | null): FormulaEquipmentValue {
    if (slot === EquipableSlot.Chest) {
      return FormulaEquipmentValue.DefenseBonus1;
    }
    return FormulaEquipmentValue.LevelBonus;
  }

  private buildEquipmentValueOptions(): EquipmentValueOption[] {
    const numericSlots = Object.values(EquipableSlot).filter(value => typeof value === 'number') as EquipableSlot[];
    const options: EquipmentValueOption[] = [];
    numericSlots.forEach(slot => {
      const slotLabel = this.getEquipableSlotLabel(slot);
      options.push({
        key: `${slot}-level`,
        label: `Bonus ${slotLabel.toLowerCase()}`,
        slot,
        value: FormulaEquipmentValue.LevelBonus
      });
      if (slot === EquipableSlot.Chest) {
        options.push(
          {
            key: `${slot}-def1`,
            label: 'Bonus defesa 1 (peito)',
            slot,
            value: FormulaEquipmentValue.DefenseBonus1
          },
          {
            key: `${slot}-def2`,
            label: 'Bonus defesa 2 (peito)',
            slot,
            value: FormulaEquipmentValue.DefenseBonus2
          }
        );
      }
    });
    return options;
  }

  private getEquipableSlotLabel(slot: EquipableSlot): string {
    const raw = EquipableSlot[slot];
    return raw.replace(/([A-Z])/g, ' $1').trim();
  }

  private convertLegacyCreatureToken(token: FormulaToken): FormulaToken {
    const equipmentValue = this.mapLegacyCreatureValueToEquipment(token.customValue);
    return {
      ...token,
      type: FormulaTokenType.Equipment,
      equipmentSlot: EquipableSlot.Chest,
      equipmentValue,
      customValue: null
    };
  }

  private mapLegacyCreatureValueToEquipment(
    value: FormulaCreatureValue | null | undefined
  ): FormulaEquipmentValue {
    switch (value) {
      case FormulaCreatureValue.DefenseBonus1:
      case FormulaCreatureValue.ArmorDefenseBonus:
        return FormulaEquipmentValue.DefenseBonus1;
      case FormulaCreatureValue.DefenseBonus2:
        return FormulaEquipmentValue.DefenseBonus2;
      case FormulaCreatureValue.ArmorBonus:
        return FormulaEquipmentValue.LevelBonus;
      default:
        return FormulaEquipmentValue.LevelBonus;
    }
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
      case FormulaTokenType.Creature:
        return appendManual(this.getCreatureValueLabel(token.customValue), token.manualValue);
      case FormulaTokenType.Equipment:
        return appendManual(this.getEquipmentValueLabel(token), token.manualValue);
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
      case FormulaTokenType.Creature:
        return appendManual(this.getCreatureValueLabel(token.customValue), token.manualValue);
      case FormulaTokenType.Equipment:
        return appendManual(this.getEquipmentValueLabel(token), token.manualValue);
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

  private getCreatureValueLabel(customValue?: FormulaCreatureValue | null): string {
    switch (customValue) {
      case FormulaCreatureValue.Level:
        return 'Nivel';
      case FormulaCreatureValue.DefenseBonus1:
      case FormulaCreatureValue.ArmorDefenseBonus:
        return 'Bonus defesa 1';
      case FormulaCreatureValue.DefenseBonus2:
        return 'Bonus defesa 2';
      case FormulaCreatureValue.ArmorBonus:
        return 'Bonus armadura';
      default:
        return 'Criatura';
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
      case FormulaTokenType.Creature:
        return 'Valor da criatura';
      case FormulaTokenType.Equipment:
        return 'Valor de equipamento';
      case FormulaTokenType.Manual:
        return 'Valor manual';
      default:
        return 'Token';
    }
  }

  private getEquipmentValueLabel(token: FormulaToken): string {
    const slot = token.equipmentSlot ?? EquipableSlot.Chest;
    const slotLabel = this.getEquipableSlotLabel(slot);
    switch (token.equipmentValue) {
      case FormulaEquipmentValue.DefenseBonus1:
        return 'Bonus defesa 1';
      case FormulaEquipmentValue.DefenseBonus2:
        return 'Bonus defesa 2';
      default:
        return `Bonus ${slotLabel.toLowerCase()}`;
    }
  }

  private emitPreview(): void {
    this.expressionChange.emit(this.expressionPreview);
    this.descriptionChange.emit(this.descriptionPreview);
  }
}
