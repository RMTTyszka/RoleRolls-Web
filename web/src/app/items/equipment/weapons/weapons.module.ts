import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {WeaponModelsModule} from './weapon-models/weapon-models.module';
import {BaseWeaponsModule} from './base-weapons/base-weapons.module';


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    WeaponModelsModule,
    BaseWeaponsModule
  ]
})
export class WeaponsModule { }
