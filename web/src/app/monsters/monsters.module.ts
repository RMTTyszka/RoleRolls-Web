import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {MonstersListComponent} from './monster-list/monsters-list.component';
import {RouterModule, Routes} from '@angular/router';
import {SharedModule} from '../shared/shared.module';
import {MonsterModelComponent} from './monsters-bases/monster-model-editor/monster-model.component';
import {MonsterComponent} from './monster/monster.component';
import {MonsterBaseListComponent} from './monsters-bases/monster-model-list/monster-model-list.component';
import {MonstersComponent} from './monsters.component';
import {RaceSharedModule} from '../races/shared/race-shared.module';
import {RolesSharedModule} from '../roles/roles-shared/roles-shared.module';
import {MonstersSharedModule} from './monsters-shared/monsters-shared.module';
import {ToolbarModule} from 'primeng/toolbar';
import {CreaturesSharedModule} from '../creatures-shared/creatures-shared.module';
import { MonsterCreateComponent } from './monster-create/monster-create.component';
import {DynamicDialogModule} from 'primeng/dynamicdialog';
import {InputTextModule} from 'primeng/inputtext';
import {MonsterTemplateProviderModule} from './monsters-bases/monster-template-provider/monster-template-provider.module';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {MatTab, MatTabGroup} from "@angular/material/tabs";


const routes: Routes = [
  {path: '', component: MonstersComponent},
  {path: 'model/:id', component: MonsterModelComponent},
  {path: 'model', component: MonsterModelComponent}
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    SharedModule,
    MonsterTemplateProviderModule,
    RaceSharedModule,
    RolesSharedModule,
    MonstersSharedModule,
    ToolbarModule,
    CreaturesSharedModule,
    DynamicDialogModule,
    InputTextModule,
    MatTab,
    MatTabGroup,
  ],
    declarations: [MonstersListComponent, MonsterComponent, MonsterModelComponent, MonsterBaseListComponent, MonstersComponent, MonsterCreateComponent],
    providers: [
        { provide: MatDialogRef, useValue: {} },
        { provide: MAT_DIALOG_DATA, useValue: {} }
    ],
    exports: []
})
export class MonstersModule { }
