import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WeaponsTestComponent } from './weapons-test/weapons-test.component';
import { Routes, RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';

export const routes: Routes = [
  {path: '', component: WeaponsTestComponent},
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    SharedModule

  ],
  declarations: [WeaponsTestComponent]
})
export class TestsModule { }
