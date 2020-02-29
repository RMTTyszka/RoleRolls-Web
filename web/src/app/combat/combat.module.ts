import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CombatComponent } from './combat.component';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {path: '', component: CombatComponent}
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
  ],
  declarations: [CombatComponent]
})
export class CombatModule { }
