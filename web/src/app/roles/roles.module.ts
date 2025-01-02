import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RolesListComponent} from './roles-list/roles-list.component';
import {RolesEditorComponent} from './roles-editor/roles-editor.component';
import {SharedModule} from '../shared/shared.module';
import {RouterModule, Routes} from '@angular/router';
import {
  MatCard,
  MatCardActions,
  MatCardContent,
  MatCardHeader,
  MatCardSubtitle,
  MatCardTitle
} from '@angular/material/card';
import {MatFormField} from '@angular/material/form-field';

export const routes: Routes = [
  {path: '', component: RolesListComponent},
  // {path: 'edit/:id', component: RaceEditorComponent},
  // {path: 'new', component: RaceEditorComponent}
];

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forChild(routes),
    MatCardActions,
    MatFormField,
    MatCardContent,
    MatCardSubtitle,
    MatCardTitle,
    MatCardHeader,
    MatCard
  ],
    declarations: [RolesListComponent, RolesEditorComponent],
    providers: []
})
export class RolesModule { }
