import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {ArmorListComponent} from './armor-list/armor-list.component';
import {ArmorEditorComponent} from './armor-editor/armor-editor.component';
import {ToolbarModule} from 'primeng/toolbar';
import {SharedModule} from '../../../../shared/shared.module';
import {BaseArmorSharedModule} from '../baseArmor/base-armor-shared/base-armor-shared.module';
import {ReactiveFormsModule} from '@angular/forms';


@NgModule({
    declarations: [ArmorListComponent, ArmorEditorComponent],
    imports: [
        CommonModule,
        ToolbarModule,
        ReactiveFormsModule,
        SharedModule,
        BaseArmorSharedModule
    ]
})
export class ArmorModule { }
