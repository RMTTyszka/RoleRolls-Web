import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CampaignItemConfigurationComponent } from './campaign-item-configuration/campaign-item-configuration.component';
import { CampaignItensComponent } from './campaign-itens/campaign-itens.component';
import { CampaignTemplateComponent } from './campaign-template/campaign-template.component';
import { CampaignDetailsComponent } from './campaign-details/campaign-details.component';
import { CampaignItemCreatorComponent } from './campaign-itens/campaign-item-creator/campaign-item-creator.component';
import { TabPanel, TabView } from 'primeng/tabview';



@NgModule({
  declarations: [
    CampaignItemConfigurationComponent,
    CampaignItensComponent,
    CampaignTemplateComponent,
    CampaignDetailsComponent,
    CampaignItemCreatorComponent
  ],
  imports: [
    CommonModule,
    TabView,
    TabPanel
  ]
})
export class CampaignDetailsModule { }
