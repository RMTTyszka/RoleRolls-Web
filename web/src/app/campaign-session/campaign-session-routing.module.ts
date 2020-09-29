import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {CampaignSessionGatewayComponent} from './campaign-session-gateway/campaign-session-gateway.component';


const routes: Routes = [
  {path: '', component: CampaignSessionGatewayComponent},
  {path: ':campaignId', component: CampaignSessionGatewayComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CampaignSessionRoutingModule { }
