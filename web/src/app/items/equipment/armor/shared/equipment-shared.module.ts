import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {DropdownModule} from 'primeng/dropdown';
import {ReactiveFormsModule} from '@angular/forms';
import {ArmorCategorySelectComponent} from './armor-category-select/armor-category-select.component';



@NgModule({
  declarations: [ArmorCategorySelectComponent],
  imports: [
    CommonModule,
    DropdownModule,
    ReactiveFormsModule
  ],
  exports: [ArmorCategorySelectComponent]
})
export class EquipmentSharedModule { }
