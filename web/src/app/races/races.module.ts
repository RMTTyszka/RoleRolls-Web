import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RacesComponent} from './races/races.component';
import {RouterModule, Routes} from '@angular/router';
import {RacesService} from './races.service';
import {MatIconModule} from '@angular/material/icon';
import {MatToolbarModule} from '@angular/material/toolbar';
import {FormsModule} from '@angular/forms';
import {RaceEditorComponent} from './race-editor/race-editor.component';
import {SharedModule} from '../shared/shared.module';

import {PowersSharedModule} from '../powers/powers-shared/powers-shared.module';
import {DynamicDialogModule} from 'primeng/dynamicdialog';
import {MatTableModule} from "@angular/material/table";
import {MatSelectModule} from "@angular/material/select";
import {MatInputModule} from "@angular/material/input";

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
