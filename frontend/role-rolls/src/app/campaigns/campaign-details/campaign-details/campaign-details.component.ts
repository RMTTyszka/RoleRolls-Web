import { Component } from '@angular/core';
import { EditorAction } from '../../../models/ModalEntityData';
import { Campaign } from '../../models/campaign';
import {
  CampaignItemConfigurationComponent
} from '../campaign-item-configuration/campaign-item-configuration.component';
import { CampaignItensComponent } from '../campaign-itens/campaign-itens.component';
import { CampaignTemplateComponent } from '../campaign-template/campaign-template.component';
import { NgIf } from '@angular/common';
import { Tab, TabList, TabPanel, TabPanels, Tabs } from 'primeng/tabs';
import {CreatureTypesComponent} from '@app/campaigns/campaign-details/creature-types/creature-types.component';

@Component({
  selector: 'rr-campaign-details',
  standalone: true,

  templateUrl: './campaign-details.component.html',
  imports: [
    TabPanel,
    CampaignItemConfigurationComponent,
    CampaignItensComponent,
    CampaignTemplateComponent,
    Tabs,
    TabPanels,
    TabList,
    Tab,
    TabPanel,
    TabPanel,
    NgIf,
    CreatureTypesComponent
  ],
  styleUrl: './campaign-details.component.scss'
})
export class CampaignDetailsComponent {
  activeTab = '0';
  public action = EditorAction.create;
  public actionEnum = EditorAction;
  public entityId!: string;
  public campaign!: Campaign;
  constructor(
  ) {
/*    this.action = config.data.action;
    this.entityId = config.data.entityId;*/
  }

  ngOnInit(): void {

  }

  protected readonly caches = caches;

  campaignLoaded($event: Campaign) {
    this.campaign = $event;
  }
}

