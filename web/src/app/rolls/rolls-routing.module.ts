import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {HomeComponent} from '../home/home.component';
import {RollsComponent} from './rolls.component';
import {MakeTestComponent} from './make-test/make-test.component';


const routes: Routes = [
  {path: '', redirectTo: 'makeTest'},
  {path: 'makeTest', component: MakeTestComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RollsRoutingModule { }
