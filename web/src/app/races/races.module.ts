import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RacesComponent} from './races/races.component';
import {RouterModule, Routes} from '@angular/router';
import {RacesService} from './races.service';
import {MatLegacyTableModule as MatTableModule} from '@angular/material/legacy-table';
import {MatIconModule} from '@angular/material/icon';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatLegacySelectModule as MatSelectModule} from '@angular/material/legacy-select';
import {MatLegacyInputModule as MatInputModule} from '@angular/material/legacy-input';
import {FormsModule} from '@angular/forms';
import {RaceEditorComponent} from './race-editor/race-editor.component';
import {SharedModule} from '../shared/shared.module';

import {PowersSharedModule} from '../powers/powers-shared/powers-shared.module';
import {DynamicDialogModule} from 'primeng/dynamicdialog';

export const routes: Routes = [
  {path: '', component: RacesComponent},
  // {path: 'edit/:id', component: RaceEditorComponent},
  // {path: 'new', component: RaceEditorComponent}
];

@NgModule({
    imports: [
        CommonModule,
        RouterModule.forChild(routes),
        MatTableModule,
        MatIconModule,
        MatToolbarModule,
        MatSelectModule,
        MatInputModule,
        FormsModule,
        SharedModule,
        PowersSharedModule,
        DynamicDialogModule
    ],
    declarations: [RacesComponent, RaceEditorComponent],
    providers: [
        RacesService
    ]
})
export class RacesModule { }
