import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PocketCampaignsComponent } from './pocket-campaigns/pocket-campaigns.component';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from 'src/app/shared/shared.module';
import { CampaignCreatorComponent } from './campaign-creator/campaign-creator.component';
import { PocketCampaignBodyshellComponent } from './pocket-campaign-bodyshell/pocket-campaign-bodyshell.component';
import {SlideMenuModule} from 'primeng/slidemenu';
import { CampaignRollsComponent } from './campaign-rolls/campaign-rolls.component';
import { CampaignHeroesComponent } from './campaign-heroes/campaign-heroes.component';
import { CampaignMonstersComponent } from './campaign-monsters/campaign-monsters.component';
const routes: Routes = [
  {path: ':id', component: PocketCampaignBodyshellComponent},
  {path: '', component: PocketCampaignsComponent}
];

@NgModule({
  declarations: [
    PocketCampaignsComponent,
    CampaignCreatorComponent,
    PocketCampaignBodyshellComponent,
    CampaignRollsComponent,
    CampaignHeroesComponent,
    CampaignMonstersComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),

    SlideMenuModule,

    SharedModule,
  ]
})
export class CampaignsModule { }
