import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WeaponModelListComponent } from './weapon-model-list/weapon-model-list.component';
import { WeaponModelEditorComponent } from './weapon-model-editor/weapon-model-editor.component';



@NgModule({
  declarations: [WeaponModelListComponent, WeaponModelEditorComponent],
  imports: [
    CommonModule
  ]
})
export class WeaponModelsModule { }
