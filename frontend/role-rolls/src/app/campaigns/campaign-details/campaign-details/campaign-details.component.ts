import { Component } from '@angular/core';
import { EditorAction } from '../../../models/EntityActionData';
import { Campaign } from '../../models/campaign';
import {
  CampaignItemConfigurationComponent
} from '../campaign-item-configuration/campaign-item-configuration.component';
import { CampaignItensComponent } from '../campaign-itens/campaign-itens.component';
import { CampaignTemplateComponent } from '../campaign-template/campaign-template.component';
import { NgIf } from '@angular/common';
import { Tab, TabList, TabPanel, TabPanels, Tabs } from 'primeng/tabs';
import {CreatureTypesComponent} from '@app/campaigns/campaign-details/creature-types/creature-types.component';
import { ArchetypesComponent } from '@app/campaigns/campaign-details/archetypes/archetypes.component';
import {
  CampaignCreaturesComponent
} from '@app/campaigns/campaign-details/campaign-creatures/campaign-creatures.component';
import { CreatureCategory } from '@app/campaigns/models/CreatureCategory';
import { EncountersComponent } from '@app/encounters/components/encounters/encounters.component';

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
    CreatureTypesComponent,
    ArchetypesComponent,
    CampaignCreaturesComponent,
    EncountersComponent
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

  protected readonly CreatureCategory = CreatureCategory;
}

