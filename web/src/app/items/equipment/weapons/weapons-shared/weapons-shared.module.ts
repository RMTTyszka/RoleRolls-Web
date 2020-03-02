import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WeaponCategorySelectComponent } from './weapon-category-select/weapon-category-select.component';
import { DropdownModule } from 'primeng/dropdown';
import { ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [WeaponCategorySelectComponent],
  imports: [
    CommonModule,
    DropdownModule,
    ReactiveFormsModule
  ], exports: [
    WeaponCategorySelectComponent
  ]
})
export class WeaponsSharedModule { }
