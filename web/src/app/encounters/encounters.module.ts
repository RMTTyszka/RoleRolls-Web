import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {EncountersComponent} from './encounters/encounters.component';
import {RouterModule, Routes} from '@angular/router';
import {EncounterCreateEditComponent} from './encounter-create-edit/encounter-create-edit.component';
import {ReactiveFormsModule} from '@angular/forms';

import {SharedModule} from '../shared/shared.module';
import {DynamicDialogModule} from 'primeng/dynamicdialog';
import {MonsterTemplateSharedModule} from '../monsters/monsters-bases/monster-template-shared/monster-template-shared.module';
import {PanelModule} from 'primeng/panel';
import {MAT_DIALOG_DEFAULT_OPTIONS} from "@angular/material/dialog";

const routes: Routes = [
  {path: '', component: EncountersComponent, data: {primaryColor: '#000'}}
];

@NgModule({
    imports: [
        CommonModule,
        RouterModule.forChild(routes),
        ReactiveFormsModule,

        SharedModule,
        // Primeng
        DynamicDialogModule,
        // RR
        MonsterTemplateSharedModule,
        PanelModule
    ],
    declarations: [
        EncountersComponent,
        EncounterCreateEditComponent
    ],
    exports: [
        EncounterCreateEditComponent
    ],
    providers: [
        { provide: MAT_DIALOG_DEFAULT_OPTIONS, useValue: { hasBackdrop: false } }
    ]
})
export class EncountersModule { }
