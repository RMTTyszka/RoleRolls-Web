import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PocketCampaignsComponent } from './pocket-campaigns/pocket-campaigns.component';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from 'src/app/shared/shared.module';
import { CampaignCreatorComponent } from './campaign-creator/campaign-creator.component';

const routes: Routes = [
  {path: '', component: PocketCampaignsComponent},
];

@NgModule({
  declarations: [
    PocketCampaignsComponent,
    CampaignCreatorComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),

    SharedModule,
  ]
})
export class CampaignsModule { }
