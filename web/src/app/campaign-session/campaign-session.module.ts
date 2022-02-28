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
import {CampaignRollsService} from './rolls/campaign-rolls.service';
import { CampaignRollsComponent } from './rolls/campaign-rolls/campaign-rolls.component';
import {PanelModule} from 'primeng/panel';
import {OverlayPanelModule} from 'primeng/overlaypanel';
import {FormsModule} from '@angular/forms';
import {CardModule} from 'primeng/card';
import {TooltipModule} from 'primeng/tooltip';
import {ToastModule} from 'primeng/toast';
import {ScrollPanelModule} from 'primeng/scrollpanel';
import { CampaignEncountersComponent } from './campaign-encounters/campaign-encounters.component';


@NgModule({
    declarations: [CampaignSessionGatewayComponent, CampaignHeroesComponent, CampaignCombatComponent, CampaignHeroDetailsComponent, CampaignRollsComponent, CampaignEncountersComponent],
    imports: [
        CommonModule,
        CampaignSessionRoutingModule,
        TabViewModule,
        TableModule,
        CreaturesSharedModule,
        CombatModule,
        PanelModule,
        OverlayPanelModule,
        FormsModule,
        CardModule,
        TooltipModule,
        ToastModule,
        ScrollPanelModule
    ],
    providers: [CampaignRollsService]
})
export class CampaignSessionModule {
}
