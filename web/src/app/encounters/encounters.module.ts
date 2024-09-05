import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {EncountersComponent} from './encounters/encounters.component';
import {RouterModule, Routes} from '@angular/router';
import {EncounterCreateEditComponent} from './encounter-create-edit/encounter-create-edit.component';
import {MatLegacyFormFieldModule as MatFormFieldModule} from '@angular/material/legacy-form-field';
import {MAT_LEGACY_DIALOG_DEFAULT_OPTIONS as MAT_DIALOG_DEFAULT_OPTIONS, MatLegacyDialogModule as MatDialogModule} from '@angular/material/legacy-dialog';
import {MatLegacyInputModule as MatInputModule} from '@angular/material/legacy-input';
import {ReactiveFormsModule} from '@angular/forms';
import {FlexLayoutModule} from '@angular/flex-layout';
import {SharedModule} from '../shared/shared.module';
import {DynamicDialogModule} from 'primeng/dynamicdialog';
import {MonstersModule} from '../monsters/monsters.module';
import {MonsterTemplateSharedModule} from '../monsters/monsters-bases/monster-template-shared/monster-template-shared.module';
import {PanelModule} from 'primeng/panel';

const routes: Routes = [
  {path: '', component: EncountersComponent, data: {primaryColor: '#000'}}
];

@NgModule({
    imports: [
        CommonModule,
        RouterModule.forChild(routes),
        MatFormFieldModule,
        MatDialogModule,
        MatInputModule,
        ReactiveFormsModule,
        FlexLayoutModule,
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
