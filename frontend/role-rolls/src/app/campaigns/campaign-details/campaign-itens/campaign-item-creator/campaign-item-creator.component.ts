import { Component, effect, EventEmitter, Input, Output, Signal } from '@angular/core';
import {
  ArmorCategory, ArmorTemplateModel,
  ItemTemplateModel,
  ItemType,
  WeaponCategory,
  WeaponTemplateModel
} from '../../../../models/ItemTemplateModel';
import { EditorAction } from '../../../../models/ModalEntityData';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { v4 as uuidv4 } from 'uuid';
import { SubscriptionManager } from '../../../../tokens/subscription-manager';
import { CampaignItemTemplatesService } from '../services/campaign-item-templates.service';
import { CampaignEditorDetailsServiceService } from '../../services/campaign-editor-details-service.service';
import { Campaign } from '../../../models/campaign';
import { Panel } from 'primeng/panel';
import { ButtonDirective } from 'primeng/button';
import { Select } from 'primeng/select';
import { NgIf } from '@angular/common';
import {LoggerService} from '@services/logger/logger.service';

@Component({
  selector: 'rr-campaign-item-creator',
  standalone: true,

  templateUrl: './campaign-item-creator.component.html',
  imports: [
    Panel,
    ButtonDirective,
    ReactiveFormsModule,
    Select,
    NgIf
  ],
  styleUrl: './campaign-item-creator.component.scss'
})
export class CampaignItemCreatorComponent {
  public item: ItemTemplateModel | WeaponTemplateModel;
  public action: EditorAction = EditorAction.create;
  @Output() public saved = new EventEmitter<void>();
  public form: FormGroup;
  @Input() public itemType: Signal<ItemType>;
  public itemTypeEnum = ItemType;
  public weaponCategories = [
    {key: 'Light', value: WeaponCategory.Light},
    {key: 'Medium', value: WeaponCategory.Medium},
    {key: 'Heavy', value: WeaponCategory.Heavy},
  ];
  public armorCategories = [
    {key: 'Light', value: ArmorCategory.Light},
    {key: 'Medium', value: ArmorCategory.Medium},
    {key: 'Heavy', value: ArmorCategory.Heavy},
  ];
  public get canSave(): boolean {
    return this.form.valid;
  }
  @Input() public campaign: Campaign;
  private subscription: SubscriptionManager = new SubscriptionManager();


  constructor(
    private formBuilder: FormBuilder,
    private service: CampaignItemTemplatesService,
    private detailsServiceService: CampaignEditorDetailsServiceService,
    private loggerService: LoggerService,
  ) {
    this.listenToItemTypeChanges();
  }

  ngOnInit(): void {
    this.createForm();
    this.subscription.add('itemTemplate', this.detailsServiceService.itemTemplate.subscribe((item: ItemTemplateModel) => {
      this.item = item;
    }));
    this.subscription.add('itemTemplateEditorAction', this.detailsServiceService.itemTemplateEditorAction.subscribe((editorAction: EditorAction) => {
      if (editorAction !== this.action) {
        this.action = editorAction;
      }
    }));
  }
  ngOnDestroy() {
    this.subscription.clear();
  }

  public async save() {
    if (this.action === EditorAction.create) {
      switch (this.itemType()) {
        case ItemType.Consumable:
          const itemTemplate = this.form.value as ItemTemplateModel;
          await this.service.addItem(itemTemplate).toPromise();
          break;
        case ItemType.Weapon:
          const weaponTemplate = this.form.value as WeaponTemplateModel;
          await this.service.addWeapon(weaponTemplate).toPromise();
          break;
        case ItemType.Armor:
          const template = this.form.value as ArmorTemplateModel;
          await this.service.addArmor(template).toPromise();
          break;
      }
    } else {
      switch (this.itemType()) {
        case ItemType.Consumable:
          const itemTemplate = this.form.value as ItemTemplateModel;
          await this.service.updateItem(this.item.id, itemTemplate).toPromise();
          break;
        case ItemType.Weapon:
          const weaponTemplate = this.form.value as WeaponTemplateModel;
          await this.service.updateWeapon(this.item.id, weaponTemplate).toPromise();
          break;
        case ItemType.Armor:
          const template = this.form.value as ArmorTemplateModel;
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
    });
    this.action = EditorAction.create;
  }

  private populateForm(item: ItemTemplateModel) {
    if (item && this.form) {
      this.form.get('name').setValue(item.name);
      this.form.get('id').setValue(item.id);
      this.form.get('campaignId').setValue(item.campaignId);
    }
    this.form.get('type').setValue(ItemType.Consumable);
    this.form.get('category').setValue(null);
    this.form.get('category').setValidators(Validators.nullValidator);
    this.form.get('category').updateValueAndValidity();
  }
  private populateFormAsWeapon(item: WeaponTemplateModel) {
    if (item && this.form) {
      this.populateForm(item);
      this.form.get('category').setValue(item.category);
    } else {
      this.form.get('category').setValue(null);
    }
    this.form.get('type').setValue(ItemType.Weapon);
    this.form.get('category').setValidators(Validators.required);
    this.form.get('category').updateValueAndValidity();
  }
  private populateFormAsArmor(item: ArmorTemplateModel) {
    if (item && this.form) {
      this.populateForm(item);
      this.form.get('category').setValue(item.category);
    } else {
      this.form.get('category').setValue(null);
    }
    this.form.get('type').setValue(ItemType.Armor);
    this.form.get('category').setValidators(Validators.required);
    this.form.get('category').updateValueAndValidity();
  }
  private configurarFormAsItem() {
    this.populateForm(this.item);
  }
  private configurarFormAaWeapon() {
    this.populateFormAsWeapon(this.item as WeaponTemplateModel);
  }
  private configurarFormAsArmor() {
    this.populateFormAsArmor(this.item as ArmorTemplateModel);
  }

  private listenToItemTypeChanges() {
    effect(() => {
      switch (this.itemType()) {
        case null:
          this.form.disable();
          break;
        default:
          this.form.enable();
          break;
      }
      switch (this.itemType()) {
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
    });
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
