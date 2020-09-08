import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CampaignListComponent } from './campaign-list/campaign-list.component';
import {ToolbarModule} from 'primeng/toolbar';
import {SharedModule} from '../shared/shared.module';
import { CampaignEditorComponent } from './campaign-editor/campaign-editor.component';
import {RouterModule, Routes} from '@angular/router';
import {DynamicDialogModule} from 'primeng/dynamicdialog';
import {PlayerSharedModule} from '../players/player-shared/player-shared.module';
import { CampaignInvitationComponent } from './campaign-invitation/campaign-invitation.component';
import {TabViewModule} from 'primeng/tabview';
import {FieldsetModule} from 'primeng/fieldset';
import { CampaignPlayerSelectComponent } from './campaign-player-select/campaign-player-select.component';
import {PanelModule} from 'primeng/panel';
import {ListboxModule} from 'primeng/listbox';

const routes: Routes = [
  {path: '', component: CampaignListComponent}
];


@NgModule({
  declarations: [CampaignListComponent, CampaignEditorComponent, CampaignInvitationComponent, CampaignPlayerSelectComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    ToolbarModule,
    DynamicDialogModule,
    SharedModule,
    PlayerSharedModule,
    TabViewModule,
    FieldsetModule,
    PanelModule,
    ListboxModule
  ],
  entryComponents: [CampaignEditorComponent, CampaignListComponent, CampaignPlayerSelectComponent]
})
export class CampaignModule { }
