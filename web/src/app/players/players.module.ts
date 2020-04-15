import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {PlayersComponent} from './players/players.component';
import {RouterModule, Routes} from '@angular/router';


const routes: Routes = [
  {path: '', component: PlayersComponent}
];
@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  declarations: [PlayersComponent]
})
export class PlayersModule { }
