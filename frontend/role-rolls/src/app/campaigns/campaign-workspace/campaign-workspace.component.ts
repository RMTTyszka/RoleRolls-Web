import { Component, input } from '@angular/core';
import { Tab, TabList, TabPanel, TabPanels, Tabs } from 'primeng/tabs';
import {
  CampaignItemConfigurationComponent
} from '@app/campaigns/campaign-details/campaign-item-configuration/campaign-item-configuration.component';
import { CampaignItensComponent } from '@app/campaigns/campaign-details/campaign-itens/campaign-itens.component';
import { CampaignTemplateComponent } from '@app/campaigns/campaign-details/campaign-template/campaign-template.component';
import { CreatureTypesComponent } from '@app/campaigns/campaign-details/creature-types/creature-types.component';
import { ArchetypesComponent } from '@app/campaigns/campaign-details/archetypes/archetypes.component';
import {
  CampaignCreaturesComponent
} from '@app/campaigns/campaign-details/campaign-creatures/campaign-creatures.component';
import { CreatureCategory } from '@app/campaigns/models/CreatureCategory';
import { EncountersComponent } from '@app/encounters/components/encounters/encounters.component';
import {
  CampaignSessionPanelComponent
} from '@app/campaign-session/campaign-session-panel/campaign-session-panel.component';
import {
  CampaignCreatureConditionsComponent
} from '@app/campaigns/campaign-details/campaign-creature-conditions/campaign-creature-conditions.component';

@Component({
  selector: 'rr-campaign-workspace',
  standalone: true,
  imports: [
    TabPanel,
    CampaignItemConfigurationComponent,
    CampaignItensComponent,
    CampaignTemplateComponent,
    Tabs,
    TabPanels,
    TabList,
    Tab,
    CreatureTypesComponent,
    ArchetypesComponent,
    CampaignCreaturesComponent,
    EncountersComponent,
    CampaignSessionPanelComponent,
    CampaignCreatureConditionsComponent
  ],
  templateUrl: './campaign-workspace.component.html',
  styleUrl: './campaign-workspace.component.scss'
})
export class CampaignWorkspaceComponent {
  public readonly showSessionTab = input(false);
  public activeTab = 'template';

  ngOnInit(): void {
    if (this.showSessionTab()) {
      this.activeTab = 'session';
    }
  }

  protected readonly CreatureCategory = CreatureCategory;
}
