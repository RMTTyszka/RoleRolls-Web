import { Component, effect, EventEmitter, Input, Output, Signal } from '@angular/core';

import { EditorAction } from '@app/models/EntityActionData';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { v4 as uuidv4 } from 'uuid';
import { SubscriptionManager } from '@app/tokens/subscription-manager';
import { CampaignItemTemplatesService } from '../services/campaign-item-templates.service';
import { CampaignEditorDetailsServiceService } from '../../services/campaign-editor-details-service.service';
import { Campaign } from '../../../models/campaign';
import { Panel } from 'primeng/panel';
import { ButtonDirective } from 'primeng/button';
import { Select } from 'primeng/select';
import { NgIf } from '@angular/common';
import { Checkbox } from 'primeng/checkbox';
import { InputText } from 'primeng/inputtext';
import {LoggerService} from '@services/logger/logger.service';
import {
  AnyItemTemplateModel,
  ArmorCategory,
  ArmorTemplateModel,
  armorCategoryLabel,
  isShieldCategory,
  ItemTemplateModel,
  ItemType,
  WeaponDamageType,
  weaponCategoryLabel,
  weaponDamageTypeLabel,
  WeaponCategory,
  WeaponTemplateModel
} from '@app/models/itens/ItemTemplateModel';
import { EquipableSlot } from '@app/models/itens/equipable-slot';

@Component({
  selector: 'rr-campaign-item-creator',
  standalone: true,

  templateUrl: './campaign-item-creator.component.html',
  imports: [
    Panel,
    ButtonDirective,
    ReactiveFormsModule,
    Select,
    NgIf,
    Checkbox,
    InputText
  ],
  styleUrl: './campaign-item-creator.component.scss'
})
export class CampaignItemCreatorComponent {
  public item: AnyItemTemplateModel | null = null;
  public action: EditorAction = EditorAction.create;
  @Output() public saved = new EventEmitter<void>();
  public form!: FormGroup;
  @Input() public itemType!: Signal<ItemType | null>;
  public itemTypeEnum = ItemType;
  public weaponCategories = [
    {key: weaponCategoryLabel(WeaponCategory.Light), value: WeaponCategory.Light},
    {key: weaponCategoryLabel(WeaponCategory.Medium), value: WeaponCategory.Medium},
    {key: weaponCategoryLabel(WeaponCategory.Heavy), value: WeaponCategory.Heavy},
    {key: weaponCategoryLabel(WeaponCategory.LightShield), value: WeaponCategory.LightShield},
    {key: weaponCategoryLabel(WeaponCategory.MediumShield), value: WeaponCategory.MediumShield},
    {key: weaponCategoryLabel(WeaponCategory.HeavyShield), value: WeaponCategory.HeavyShield},
  ];
  public weaponDamageTypes = [
    {key: weaponDamageTypeLabel(WeaponDamageType.Cutting), value: WeaponDamageType.Cutting},
    {key: weaponDamageTypeLabel(WeaponDamageType.Bludgeoning), value: WeaponDamageType.Bludgeoning},
    {key: weaponDamageTypeLabel(WeaponDamageType.Piercing), value: WeaponDamageType.Piercing},
    {key: weaponDamageTypeLabel(WeaponDamageType.Shield), value: WeaponDamageType.Shield},
  ];
  public armorCategories = [
    {key: armorCategoryLabel(ArmorCategory.Light), value: ArmorCategory.Light},
    {key: armorCategoryLabel(ArmorCategory.Medium), value: ArmorCategory.Medium},
    {key: armorCategoryLabel(ArmorCategory.Heavy), value: ArmorCategory.Heavy},
  ];
  public get currentItemType(): ItemType | null {
    return this.resolveCurrentItemType();
  }
  public get canSave(): boolean {
    return this.form?.valid ?? false;
  }
  @Input() public campaign!: Campaign;
  private subscription: SubscriptionManager = new SubscriptionManager();


  constructor(
    private formBuilder: FormBuilder,
    private service: CampaignItemTemplatesService,
    private detailsServiceService: CampaignEditorDetailsServiceService,
    private loggerService: LoggerService,
  ) {
  }

  ngOnInit(): void {
    this.createForm();
    this.listenToItemTypeChanges();
    this.subscription.add('itemTemplate', this.detailsServiceService.itemTemplate.subscribe((item: AnyItemTemplateModel) => {
      this.item = item;
      this.applyFormForCurrentType();
    }));
    this.subscription.add('itemTemplateEditorAction', this.detailsServiceService.itemTemplateEditorAction.subscribe((editorAction: EditorAction) => {
      if (editorAction !== this.action) {
        this.action = editorAction;
        this.applyFormForCurrentType();
      }
    }));
    this.applyFormForCurrentType();
  }
  ngOnDestroy() {
    this.subscription.clear();
  }

  public async save() {
    const currentItemType = this.resolveCurrentItemType();

    if (currentItemType === null || (this.action === EditorAction.update && !this.item)) {
      return;
    }

    this.applyDerivedValues();
    this.form.updateValueAndValidity();

    if (!this.form.valid) {
      return;
    }

    if (this.action === EditorAction.create) {
      switch (currentItemType) {
        case ItemType.Consumable:
          const itemTemplate = this.form.getRawValue() as ItemTemplateModel;
          await this.service.addItem(itemTemplate).toPromise();
          break;
        case ItemType.Weapon:
          const weaponTemplate = this.form.getRawValue() as WeaponTemplateModel;
          await this.service.addWeapon(weaponTemplate).toPromise();
          break;
        case ItemType.Armor:
          const template = this.form.getRawValue() as ArmorTemplateModel;
          await this.service.addArmor(template).toPromise();
          break;
      }
    } else {
      switch (currentItemType) {
        case ItemType.Consumable:
          const itemTemplate = this.form.getRawValue() as ItemTemplateModel;
          await this.service.updateItem(this.item.id, itemTemplate).toPromise();
          break;
        case ItemType.Weapon:
          const weaponTemplate = this.form.getRawValue() as WeaponTemplateModel;
          await this.service.updateWeapon(this.item.id, weaponTemplate).toPromise();
          break;
        case ItemType.Armor:
          const template = this.form.getRawValue() as ArmorTemplateModel;
          await this.service.updateArmor(this.item.id, template).toPromise();
          break;
      }
    }
    this.saved.next();
    this.refreshForm();
  }
  private createForm() {
    this.form = this.formBuilder.group({
      name: ['', Validators.required],
      campaignId: [this.campaign.id, Validators.required],
      id: [uuidv4(), Validators.required],
      type: [ItemType.Consumable, Validators.required],
      powerId: [null, Validators.nullValidator],
      category: [null, Validators.nullValidator],
      damageType: [null, Validators.nullValidator],
      slot: [null, Validators.nullValidator],
      isRanged: [false, Validators.nullValidator],
      range: [null, Validators.nullValidator],
    });

    this.form.get('isRanged').valueChanges.subscribe((isRanged: boolean) => {
      this.updateRangeValidation(isRanged);
    });
    this.form.get('category').valueChanges.subscribe(() => {
      this.applyDerivedValues();
    });

    if (this.loggerService.logLevel === "debug") {
      this.form.statusChanges.subscribe(() => {
        this.logInvalidFields();
      });
    }
  }

  refreshForm() {
    this.form.reset({
      campaignId: this.campaign.id,
      id: uuidv4(),
      type: this.itemType() ?? ItemType.Consumable,
      isRanged: false,
    }, { emitEvent: false });
    this.item = null;
    this.action = EditorAction.create;
    this.applyFormForCurrentType();
  }

  private populateForm(item: ItemTemplateModel | null) {
    this.patchFormValues({
      name: item?.name ?? '',
      id: item?.id ?? uuidv4(),
      campaignId: item?.campaignId ?? this.campaign.id,
      powerId: item?.powerId ?? null,
      type: ItemType.Consumable,
      category: null,
      damageType: null,
      slot: null,
      isRanged: false,
      range: null,
    });
    this.form.get('category').setValidators(Validators.nullValidator);
    this.form.get('damageType').setValidators(Validators.nullValidator);
    this.form.get('category').updateValueAndValidity({ emitEvent: false });
    this.form.get('damageType').updateValueAndValidity({ emitEvent: false });
    this.updateRangeValidation(false);
  }

  private populateFormAsWeapon(item: WeaponTemplateModel | null) {
    if (item && this.form) {
      this.populateForm(item);
      this.patchFormValues({
        category: item.category,
        damageType: item.damageType,
        isRanged: item.isRanged,
        range: item.range,
        slot: item.slot,
      });
    } else {
      this.populateForm(null);
      this.patchFormValues({
        type: ItemType.Weapon,
        category: WeaponCategory.Light,
        damageType: WeaponDamageType.Cutting,
        isRanged: false,
        range: null,
        slot: EquipableSlot.MainHand,
      });
    }
    this.form.get('type').setValue(ItemType.Weapon, { emitEvent: false });
    this.form.get('category').setValidators(Validators.required);
    this.form.get('damageType').setValidators(Validators.required);
    this.form.get('category').updateValueAndValidity({ emitEvent: false });
    this.form.get('damageType').updateValueAndValidity({ emitEvent: false });
    this.applyDerivedValues();
    this.updateRangeValidation(this.form.get('isRanged').value);
  }

  private populateFormAsArmor(item: ArmorTemplateModel | null) {
    if (item && this.form) {
      this.populateForm(item);
      this.patchFormValues({
        category: item.category,
        slot: item.slot ?? EquipableSlot.Chest,
      });
    } else {
      this.populateForm(null);
      this.patchFormValues({
        type: ItemType.Armor,
        category: ArmorCategory.Light,
        slot: EquipableSlot.Chest,
      });
    }
    this.form.get('type').setValue(ItemType.Armor, { emitEvent: false });
    this.form.get('category').setValidators(Validators.required);
    this.form.get('damageType').setValidators(Validators.nullValidator);
    this.form.get('category').updateValueAndValidity({ emitEvent: false });
    this.form.get('damageType').updateValueAndValidity({ emitEvent: false });
    this.updateRangeValidation(false);
    this.applyDerivedValues();
  }

  private configurarFormAsItem() {
    this.populateForm(this.item);
  }

  private configurarFormAaWeapon() {
    this.populateFormAsWeapon(this.item?.type === ItemType.Weapon ? this.item as WeaponTemplateModel : null);
  }
  private configurarFormAsArmor() {
    this.populateFormAsArmor(this.item?.type === ItemType.Armor ? this.item as ArmorTemplateModel : null);
  }

  private listenToItemTypeChanges() {
    effect(() => {
      const selectedType = this.itemType?.() ?? null;

      if (!this.form) {
        return;
      }

      if (this.action === EditorAction.update && this.item && this.item.type === selectedType) {
        this.applyFormForCurrentType();
        return;
      }

      this.item = null;
      this.action = EditorAction.create;
      this.applyFormForCurrentType();
    });
  }

  private applyFormForCurrentType() {
    if (!this.form || !this.itemType) {
      return;
    }

    const currentItemType = this.resolveCurrentItemType();

    if (currentItemType === null) {
      this.form.disable();
    } else {
      this.form.enable();
    }

    switch (currentItemType) {
      case null:
      case ItemType.Consumable:
        this.configurarFormAsItem();
        break;
      case ItemType.Weapon:
        this.configurarFormAaWeapon();
        break;
      case ItemType.Armor:
        this.configurarFormAsArmor();
        break;
    }
  }

  private applyDerivedValues() {
    if (!this.form) {
      return;
    }

    switch (this.resolveCurrentItemType()) {
      case ItemType.Weapon: {
        const category = this.form.get('category').value as WeaponCategory | null;
        const slot = isShieldCategory(category) ? EquipableSlot.OffHand : EquipableSlot.MainHand;
        this.form.get('slot').setValue(slot, { emitEvent: false });

        if (isShieldCategory(category) && this.form.get('damageType').value !== WeaponDamageType.Shield) {
          this.form.get('damageType').setValue(WeaponDamageType.Shield, { emitEvent: false });
        }

        if (!isShieldCategory(category) && this.form.get('damageType').value === WeaponDamageType.Shield) {
          this.form.get('damageType').setValue(WeaponDamageType.Cutting, { emitEvent: false });
        }
        break;
      }
      case ItemType.Armor:
        this.form.get('slot').setValue(EquipableSlot.Chest, { emitEvent: false });
        break;
      default:
        this.form.get('slot').setValue(null, { emitEvent: false });
        break;
    }
  }

  private updateRangeValidation(isRanged: boolean) {
    const rangeControl = this.form.get('range');
    if (isRanged && this.resolveCurrentItemType() === ItemType.Weapon) {
      rangeControl.setValidators(Validators.required);
    } else {
      rangeControl.clearValidators();
      rangeControl.setValue(null, { emitEvent: false });
    }

    rangeControl.updateValueAndValidity({ emitEvent: false });
  }

  private resolveCurrentItemType(): ItemType | null {
    if (this.action === EditorAction.update && this.item) {
      return this.item.type;
    }

    return this.itemType?.() ?? null;
  }

  private patchFormValues(values: Record<string, unknown>) {
    this.form.patchValue(values, { emitEvent: false });
  }

  logInvalidFields(): void {
    const invalidFields: string[] = [];

    // Percorre todos os controles do formulário
    Object.keys(this.form.controls).forEach((key) => {
      const control = this.form.get(key);

      // Se o controle for inválido, adiciona ao array de campos inválidos
      if (control?.invalid) {
        invalidFields.push(key);
      }
    });

    // Loga os campos inválidos no console
    if (invalidFields.length > 0) {
      console.log('Campos inválidos:', invalidFields);
    } else {
      console.log('Todos os campos são válidos');
    }
  }
}
