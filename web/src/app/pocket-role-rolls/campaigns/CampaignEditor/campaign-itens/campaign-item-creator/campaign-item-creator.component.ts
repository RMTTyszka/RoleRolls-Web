import {Component, effect, EventEmitter, Input, OnDestroy, OnInit, Output, signal} from '@angular/core';
import {
  ArmorCategory,
  ArmorTemplateModel,
  ItemTemplateModel,
  ItemType,
  WeaponCategory,
  WeaponTemplateModel
} from "src/app/shared/models/pocket/itens/ItemTemplateModel";
import {EditorAction} from "src/app/shared/dtos/ModalEntityData";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {v4 as uuidv4} from 'uuid';
import {CampaignItemTemplatesService} from "src/app/pocket-role-rolls/proxy-services/campaign-item-templates.service";
import {PocketCampaignModel} from "src/app/shared/models/pocket/campaigns/pocket.campaign.model";
import {SubscriptionManager} from "src/app/shared/utils/subscription-manager";
import {
  CampaignEditorDetailsServiceService
} from "src/app/pocket-role-rolls/campaigns/CampaignEditor/campaign-editor-details-service.service";


@Component({
  selector: 'rr-campaign-item-creator',
  templateUrl: './campaign-item-creator.component.html',
  styleUrls: ['./campaign-item-creator.component.scss']
})
export class CampaignItemCreatorComponent implements OnInit, OnDestroy {
  public item: ItemTemplateModel | WeaponTemplateModel;
  public action: EditorAction = EditorAction.create;
  @Output() public saved = new EventEmitter<never>();
  public form: FormGroup;
  public itemType = signal(ItemType.Consumable);
  public itemTypeEnum = ItemType;
  public weaponCategories = [
    {key: 'Light', value: WeaponCategory.Light},
    {key: 'Medium', value: WeaponCategory.Medium},
    {key: 'Heavy', value: WeaponCategory.Heavy},
  ]
  public armorCategories = [
    {key: 'Light', value: ArmorCategory.Light},
    {key: 'Medium', value: ArmorCategory.Medium},
    {key: 'Heavy', value: ArmorCategory.Heavy},
  ]
  public get canSave(): boolean {
    return this.form.valid;
  }
  @Input() private campaign: PocketCampaignModel;
private subscription: SubscriptionManager = new SubscriptionManager();
  itemTypesOptions = [
    {name: 'Item', value: ItemType.Consumable},
    {name: 'Weapon', value: ItemType.Weapon},
    {name: 'Armor', value: ItemType.Armor},
  ];

  constructor(
    private formBuilder: FormBuilder,
    private service: CampaignItemTemplatesService,
    private detailsServiceService: CampaignEditorDetailsServiceService,
  ) {
    effect(() => {
      switch (this.itemType()) {
        case ItemType.Consumable:
          this.configurarFormAsItem()
          break;
        case ItemType.Weapon:
          this.configurarFormAaWeapon()
          break;
        case ItemType.Armor:
          this.configurarFormAsArmor()
          break;
      }
    });
  }

  ngOnInit(): void {

    this.createForm();
    this.subscription.add('itemTemplate', this.detailsServiceService.itemTemplate.subscribe((item: ItemTemplateModel) => {
      this.item = item;
      if (item) {
        this.itemType.set(item.type)
      }
    }));
    this.subscription.add('itemTemplateEditorAction', this.detailsServiceService.itemTemplateEditorAction.subscribe((editorAction: EditorAction) => {
      if (editorAction !== this.action) {
        this.action = editorAction;
        if (editorAction === EditorAction.create) {
          this.itemType.set(ItemType.Consumable);
        }
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
          const itemTemplate = this.form.value as ItemTemplateModel
          await this.service.addItem(itemTemplate).toPromise();
          break
        case ItemType.Weapon:
          const weaponTemplate = this.form.value as WeaponTemplateModel
          await this.service.addWeapon(weaponTemplate).toPromise();
          break
        case ItemType.Armor:
          const template = this.form.value as ArmorTemplateModel
          await this.service.addArmor(template).toPromise();
          break
      }
    } else {
      switch (this.itemType()) {
        case ItemType.Consumable:
          const itemTemplate = this.form.value as ItemTemplateModel
          await this.service.updateItem(this.item.id, itemTemplate).toPromise();
          break
        case ItemType.Weapon:
          const weaponTemplate = this.form.value as WeaponTemplateModel
          await this.service.updateWeapon(this.item.id, weaponTemplate).toPromise();
          break
        case ItemType.Armor:
          const template = this.form.value as ArmorTemplateModel
          await this.service.updateArmor(this.item.id, template).toPromise();
          break
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
    })
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
      this.form.get('type').setValue(item.type);
      this.form.get('category').setValidators(Validators.nullValidator);
    }
  }
  private populateFormAsWeapon(item: WeaponTemplateModel) {
    if (item && this.form) {
      this.populateForm(item);
      this.form.get('category').setValue(item.category);
    } else {
      this.form.get('type').setValue(ItemType.Weapon);
    }
    this.form.get('category').setValidators(Validators.required);
  }
  private populateFormAsArmor(item: ArmorTemplateModel) {
    if (item && this.form) {
      this.populateForm(item);
      this.form.get('category').setValue(item.category);
    } else {
      this.form.get('type').setValue(ItemType.Armor);
    }
    this.form.get('category').setValidators(Validators.required);
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
}
