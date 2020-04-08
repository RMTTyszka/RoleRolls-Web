import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {CombatComponent} from './combat.component';
import {RouterModule, Routes} from '@angular/router';
import {HeroesSharedModule} from '../heroes/heroes-shared/heroes-shared.module';
import {FlexLayoutModule} from '@angular/flex-layout';
import {CreaturesSharedModule} from '../creatures-shared/creatures-shared.module';

const routes: Routes = [
  {path: '', component: CombatComponent}
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    HeroesSharedModule,
    FlexLayoutModule,
    CreaturesSharedModule
  ],
  declarations: [CombatComponent]
})
export class CombatModule { }
