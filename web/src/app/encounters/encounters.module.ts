import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EncountersComponent } from './encounters/encounters.component';
import { Routes, RouterModule } from '@angular/router';
import { EncounterCreateEditComponent } from './encounter-create-edit/encounter-create-edit.component';
import {MatFormFieldModule} from '@angular/material/form-field';
import { MAT_DIALOG_DEFAULT_OPTIONS, MatDialogModule } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { ReactiveFormsModule} from '@angular/forms';
import { FlexLayoutModule } from '@angular/flex-layout';

const routes: Routes = [
  {path: '', component: EncountersComponent}
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    MatFormFieldModule,
    MatDialogModule,
    MatInputModule,
    ReactiveFormsModule,
    FlexLayoutModule
  ],
  declarations: [
    EncountersComponent,
    EncounterCreateEditComponent
  ],
  entryComponents: [
    EncounterCreateEditComponent
  ],
  exports: [
    EncounterCreateEditComponent
  ],
  providers: [
    {provide: MAT_DIALOG_DEFAULT_OPTIONS, useValue: {hasBackdrop: false}}
  ]
})
export class EncountersModule { }
