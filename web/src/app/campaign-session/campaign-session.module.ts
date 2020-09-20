import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CampaignSessionRoutingModule } from './campaign-session-routing.module';
import { CampaignSessionGatewayComponent } from './campaign-session-gateway/campaign-session-gateway.component';
import {CampaignSessionService} from './campaign-session.service';
import {TabViewModule} from 'primeng/tabview';
import { CampaignHeroesComponent } from './campaign-heroes/campaign-heroes.component';
import { CampaignCombatComponent } from './campaign-combat/campaign-combat.component';
import {TableModule} from 'primeng/table';
import { CampaignHeroDetailsComponent } from './campaign-hero-details/campaign-hero-details.component';
import {CreaturesSharedModule} from '../creatures-shared/creatures-shared.module';
import {CombatModule} from '../combat/combat.module';


@NgModule({
  declarations: [CampaignSessionGatewayComponent, CampaignHeroesComponent, CampaignCombatComponent, CampaignHeroDetailsComponent],
    imports: [
        CommonModule,
        CampaignSessionRoutingModule,
        TabViewModule,
        TableModule,
        CreaturesSharedModule,
        CombatModule
    ],
  entryComponents: [CampaignSessionGatewayComponent],
})
export class CampaignSessionModule {
}
