import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {BaseWeaponsListComponent} from './base-weapons-list/base-weapons-list.component';
import {BaseWeaponsEditorComponent} from './base-weapons-editor/base-weapons-editor.component';
import {ToolbarModule} from 'primeng/toolbar';
import {DynamicDialogModule} from 'primeng/dynamicdialog';
import {SharedModule} from 'src/app/shared/shared.module';
import {WeaponsSharedModule} from '../weapons-shared/weapons-shared.module';
import {ToastModule} from 'primeng/toast';


@NgModule({
  declarations: [BaseWeaponsListComponent, BaseWeaponsEditorComponent],
  imports: [
    CommonModule,
    SharedModule,
    ToolbarModule,
    DynamicDialogModule,
    WeaponsSharedModule,
    ToastModule
  ],
  entryComponents: [BaseWeaponsEditorComponent]
})
export class BaseWeaponsModule { }
