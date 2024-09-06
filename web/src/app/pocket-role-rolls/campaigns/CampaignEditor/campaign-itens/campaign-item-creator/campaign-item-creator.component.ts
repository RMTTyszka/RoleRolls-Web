import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {ItemTemplateModel} from "src/app/shared/models/pocket/itens/ItemTemplateModel";
import {EditorAction} from "src/app/shared/dtos/ModalEntityData";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {v4 as uuidv4} from 'uuid';
import {CampaignItemTemplatesService} from "src/app/pocket-role-rolls/proxy-services/campaign-item-templates.service";
import {PocketCampaignModel} from "src/app/shared/models/pocket/campaigns/pocket.campaign.model";
import {SubscriptionManager} from "src/app/shared/utils/subscription-manager";
import {
  PocketCampaignDetailsService
} from "src/app/pocket-role-rolls/campaigns/CampaignInstance/pocket-campaign-bodyshell/pocket-campaign-details.service";


@Component({
  selector: 'rr-campaign-item-creator',
  templateUrl: './campaign-item-creator.component.html',
  styleUrls: ['./campaign-item-creator.component.scss']
})
export class CampaignItemCreatorComponent implements OnInit {
  @Input() public item: ItemTemplateModel;
  @Input() public action: EditorAction;
  @Output() public saved = new EventEmitter<never>();
  public form: FormGroup;
  public get canSave(): boolean {
    return this.form.valid;
  }
  @Input() private campaign: PocketCampaignModel;

  constructor(
    private formBuilder: FormBuilder,
    private service: CampaignItemTemplatesService,
  ) {
  }

  ngOnInit(): void {
    this.createForm();
  }

  public async save() {
    const itemTemplate = this.form.value as ItemTemplateModel
    if (this.action === EditorAction.create) {
      await this.service.addItem(itemTemplate).toPromise();
    } else {
      await this.service.updateItem(this.item.id, itemTemplate).toPromise();
    }
    this.saved.next();
    this.form.reset();
  }
  private createForm() {
    this.form = this.formBuilder.group({
      name: ['', Validators.required],
      campaignId: [this.campaign.id, Validators.required],
      id: [uuidv4(), Validators.required],
    })
  }

  refreshForm() {
    this.form.reset();
    this.action = EditorAction.create;
  }
}
