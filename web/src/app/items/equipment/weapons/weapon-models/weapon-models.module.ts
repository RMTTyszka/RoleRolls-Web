import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {WeaponModelListComponent} from './weapon-model-list/weapon-model-list.component';
import {WeaponModelEditorComponent} from './weapon-model-editor/weapon-model-editor.component';
import {SharedModule} from 'src/app/shared/shared.module';
import {ToolbarModule} from 'primeng/toolbar';
import {WeaponsSharedModule} from '../weapons-shared/weapons-shared.module';
import {ReactiveFormsModule} from '@angular/forms';


@NgModule({
    declarations: [WeaponModelListComponent, WeaponModelEditorComponent],
    imports: [
        CommonModule,
        SharedModule,
        ToolbarModule,
        WeaponsSharedModule,
        ReactiveFormsModule
    ]
})
export class WeaponModelsModule { }
