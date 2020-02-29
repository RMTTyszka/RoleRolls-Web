import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RolesListComponent } from './roles-list/roles-list.component';
import { RolesEditorComponent } from './roles-editor/roles-editor.component';
import { SharedModule } from '../shared/shared.module';
import { Routes, RouterModule } from '@angular/router';

export const routes: Routes = [
  {path: '', component: RolesListComponent},
  // {path: 'edit/:id', component: RaceEditorComponent},
  // {path: 'new', component: RaceEditorComponent}
];

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forChild(routes)
  ],
  declarations: [RolesListComponent, RolesEditorComponent],
  providers: [],
  entryComponents: [RolesEditorComponent]
})
export class RolesModule { }
