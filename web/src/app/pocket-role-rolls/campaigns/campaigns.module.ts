import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PocketCampaignsComponent } from './pocket-campaigns/pocket-campaigns.component';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from 'src/app/shared/shared.module';
import {SlideMenuModule} from 'primeng/slidemenu';
import { PanelModule } from 'primeng/panel';
import { DynamicDialogModule } from 'primeng/dynamicdialog';
import { PocketCreatureEditorComponent } from '../pocket-creature-editor/pocket-creature-editor.component';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { TieredMenuModule } from 'primeng/tieredmenu';
import { SidebarModule } from 'primeng/sidebar';
import { DividerModule } from 'primeng/divider';
import { MessagesModule } from 'primeng/messages';
import {ProgressSpinnerModule} from 'primeng/progressspinner';
import {TimelineModule} from "primeng/timeline";
import {CardModule} from "primeng/card";
import {TabViewModule} from "primeng/tabview";
import {
  PocketCampaignBodyshellComponent
} from "./CampaignInstance/pocket-campaign-bodyshell/pocket-campaign-bodyshell.component";
import {CampaignCreatorComponent} from "./CampaignEditor/campaign-creator/campaign-creator.component";
import {CampaignRollsComponent} from "./CampaignInstance/campaign-rolls/campaign-rolls.component";
import {CampaignHeroesComponent} from "./CampaignInstance/campaign-heroes/campaign-heroes.component";
import {CampaignMonstersComponent} from "./CampaignInstance/campaign-monsters/campaign-monsters.component";
import {
  PocketCreatureSelectComponent
} from "./CampaignInstance/pocket-creature-select/pocket-creature-select.component";
import {RollDiceComponent} from "./CampaignInstance/campaign-rolls/roll-dice/roll-dice.component";
import {PocketTakeDamageComponent} from "./CampaignInstance/pocket-take-damage/pocket-take-damage.component";
import {SimulateCdComponent} from "./CampaignInstance/campaign-rolls/simulate-cd/simulate-cd.component";
import {CampaignCreatureRowComponent} from "./CampaignInstance/campaign-creature-row/campaign-creature-row.component";
import {CampaignHistoryComponent} from "./CampaignInstance/campaign-history/campaign-history.component";
import {HistoryDetailsComponent} from "./CampaignInstance/campaign-history/history-details/history-details.component";
import {
  CampaignEditorBodyShellComponent
} from "./CampaignEditor/campaign-editor-body-shell/campaign-editor-body-shell.component";
import {
  CampaignItensTemplateComponent
} from "src/app/pocket-role-rolls/campaigns/CampaignEditor/campaign-itens/campaign-itens-template.component";
import {
  CampaignItemCreatorComponent
} from "src/app/pocket-role-rolls/campaigns/CampaignEditor/campaign-itens/campaign-item-creator/campaign-item-creator.component";
import {InputTextModule} from "primeng/inputtext";
import {TabMenuModule} from "primeng/tabmenu";
import {RadioButtonModule} from "primeng/radiobutton";
import {CreatureEquipmentComponent} from 'src/app/pocket-role-rolls/pocket-creature-editor/creature-equipment/creature-equipment.component';
import {CreatureInventoryComponent} from 'src/app/pocket-role-rolls/pocket-creature-editor/creature-inventory/creature-inventory.component';

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
    HistoryDetailsComponent,
    CampaignEditorBodyShellComponent,
    CampaignItensTemplateComponent,
    CampaignItemCreatorComponent,
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
    TimelineModule,
    CardModule,
    TabViewModule,
    InputTextModule,
    TabMenuModule,
    RadioButtonModule,
    CreatureEquipmentComponent,
    CreatureInventoryComponent
  ]
})
export class CampaignsModule { }
