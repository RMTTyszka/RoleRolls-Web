import {Component, EventEmitter, Input, OnDestroy, OnInit, Output, WritableSignal} from '@angular/core';
import {
  ItemTemplateModel,
  ItemType,
  WeaponSize,
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
import {MenuItem} from "primeng/api";
import {
  ItemCreatorItemType
} from "src/app/pocket-role-rolls/campaigns/CampaignEditor/campaign-itens/campaign-item-creator/tokens/item-creator-item-type";


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
  public itemTypeMenuItens: MenuItem[] = [];
  public itemType: ItemCreatorItemType = ItemCreatorItemType.Item;
  public itemTypeEnum = ItemCreatorItemType;
  public get canSave(): boolean {
    return this.form.valid;
  }
  @Input() private campaign: PocketCampaignModel;
private subscription: SubscriptionManager = new SubscriptionManager();
  selectedItemType: MenuItem | undefined | WritableSignal<MenuItem | undefined>;
  constructor(
    private formBuilder: FormBuilder,
    private service: CampaignItemTemplatesService,
    private detailsServiceService: CampaignEditorDetailsServiceService,
  ) {
    this.buildItemTypesMenu();
  }

  ngOnInit(): void {
    this.createForm();
    this.subscription.add('itemTemplate', this.detailsServiceService.itemTemplate.subscribe((item: ItemTemplateModel) => {
      this.item = item;
      this.populateForm(item)
    }));
    this.subscription.add('itemTemplateEditorAction', this.detailsServiceService.itemTemplateEditorAction.subscribe((editorAction: EditorAction) => {
      if (editorAction !== this.action) {
        this.action = editorAction;
        this.selectedItemType = null;
        this.buildItemTypesMenu();
      }
    }));
  }
  ngOnDestroy() {
    this.subscription.clear();
  }

  public async save() {
    const itemTemplate = this.form.value as ItemTemplateModel
    if (this.action === EditorAction.create) {
      await this.service.addItem(itemTemplate).toPromise();
    } else {
      await this.service.updateItem(this.item.id, itemTemplate).toPromise();
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
      size: [null, Validators.nullValidator],
    })
  }

  refreshForm() {
    this.form.reset({
      campaignId: this.campaign.id,
      id: uuidv4(),
    });
    this.action = EditorAction.create;
    this.buildItemTypesMenu();
  }

  private populateForm(item: ItemTemplateModel) {
    if (item && this.form) {
      this.form.get('name').setValue(item.name);
      this.form.get('id').setValue(item.id);
      this.form.get('campaignId').setValue(item.campaignId);
      this.form.get('type').setValue(item.type);
      this.form.get('size').setValidators(Validators.nullValidator);
    }
  }
  private populateFormAsWeapon(item: WeaponTemplateModel) {
    if (item && this.form) {
      this.populateForm(item);
      this.form.get('size').setValue(item.size);
      this.form.get('size').setValidators(Validators.required);
    }
  }
  private configurarFormAsItem() {
    this.itemType = ItemCreatorItemType.Item;
    this.populateForm(this.item);
  }
  private configurarFormAaWeapon() {
    this.itemType = ItemCreatorItemType.Weapon;
    this.populateFormAsWeapon(this.item as WeaponTemplateModel);
  }

  private buildItemTypesMenu() {
    this.itemTypeMenuItens = [
      {
        icon: 'fas fa-briefcase',
        tooltip: 'Item',
        command: event => {
          this.configurarFormAsItem();
        },
        disabled: this.action === EditorAction.update
      } as MenuItem,
        {
        icon: 'fas fa-gavel',
        tooltip: 'Weapon',
        command: event => {
          this.configurarFormAaWeapon();
        },
        disabled: this.action === EditorAction.update
      } as MenuItem,
    ]
    this.selectedItemType = this.itemTypeMenuItens[0];
    this.selectedItemType.command.call([]);
  }
}
