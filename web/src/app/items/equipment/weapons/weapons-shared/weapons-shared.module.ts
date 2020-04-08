import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {WeaponCategorySelectComponent} from './weapon-category-select/weapon-category-select.component';
import {DropdownModule} from 'primeng/dropdown';
import {ReactiveFormsModule} from '@angular/forms';
import {BaseWeaponSelectComponent} from './base-weapon-select/base-weapon-select.component';
import {AutoCompleteModule} from 'primeng/autocomplete';


@NgModule({
  declarations: [WeaponCategorySelectComponent, BaseWeaponSelectComponent],
  imports: [
    CommonModule,
    DropdownModule,
    ReactiveFormsModule,
    AutoCompleteModule
  ], exports: [
    WeaponCategorySelectComponent, BaseWeaponSelectComponent
  ]
})
export class WeaponsSharedModule { }
