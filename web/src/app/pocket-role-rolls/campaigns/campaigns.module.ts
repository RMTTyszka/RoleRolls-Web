import { ConfirmDialogModule } from 'primeng/confirmdialog';
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
import { PanelModule } from 'primeng/panel';
import { DynamicDialogModule } from 'primeng/dynamicdialog';
import { PocketCreatureEditorComponent } from '../pocket-creature-editor/pocket-creature-editor.component';
import { PocketCreatureSelectComponent } from './pocket-creature-select/pocket-creature-select.component';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { TieredMenuModule } from 'primeng/tieredmenu';
import { SidebarModule } from 'primeng/sidebar';
import { DividerModule } from 'primeng/divider';
import { RollDiceComponent } from './campaign-rolls/roll-dice/roll-dice.component';
import { MessagesModule } from 'primeng/messages';
import { PocketTakeDamageComponent } from './pocket-take-damage/pocket-take-damage.component';
import { SimulateCdComponent } from './campaign-rolls/simulate-cd/simulate-cd.component';
import {ProgressSpinnerModule} from 'primeng/progressspinner';
import { CampaignCreatureRowComponent } from './campaign-creature-row/campaign-creature-row.component';
import { CampaignHistoryComponent } from './campaign-history/campaign-history.component';
import {TimelineModule} from "primeng/timeline";

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
    CampaignMonstersComponent,
    PocketCreatureEditorComponent,
    PocketCreatureSelectComponent,
    RollDiceComponent,
    PocketTakeDamageComponent,
    SimulateCdComponent,
    CampaignCreatureRowComponent,
    CampaignHistoryComponent,
  ],
    imports: [
        CommonModule,
        RouterModule.forChild(routes),

        SlideMenuModule,

        SharedModule,
        PanelModule,
        DynamicDialogModule,
        AutoCompleteModule,
        TieredMenuModule,
        SidebarModule,
        MessagesModule,
        ConfirmDialogModule,
        DividerModule,
        ProgressSpinnerModule,
        TimelineModule
    ]
})
export class CampaignsModule { }
