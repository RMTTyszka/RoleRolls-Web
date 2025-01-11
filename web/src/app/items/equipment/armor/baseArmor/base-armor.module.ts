import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {BaseArmorListComponent} from './base-armor-list/base-armor-list.component';
import {BaseArmorEditorComponent} from './base-armor-editor/base-armor-editor.component';
import {SharedModule} from '../../../../shared/shared.module';
import {ToolbarModule} from 'primeng/toolbar';
import {EquipmentSharedModule} from '../shared/equipment-shared.module';


@NgModule({
    declarations: [BaseArmorListComponent, BaseArmorEditorComponent],
    imports: [
        CommonModule,
        SharedModule,
        ToolbarModule,
        EquipmentSharedModule,
    ], exports: [
        BaseArmorListComponent, BaseArmorEditorComponent,
    ]
})
export class BaseArmorModule { }
