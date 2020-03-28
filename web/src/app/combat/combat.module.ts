import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CombatComponent } from './combat.component';
import { Routes, RouterModule } from '@angular/router';
import {HeroesSharedModule} from '../heroes/heroes-shared/heroes-shared.module';

const routes: Routes = [
  {path: '', component: CombatComponent}
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    HeroesSharedModule,
  ],
  declarations: [CombatComponent]
})
export class CombatModule { }
