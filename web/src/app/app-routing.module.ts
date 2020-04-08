import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {HomeComponent} from './home/home.component';

const routes: Routes = [
  {path: 'rolls', loadChildren: './rolls/rolls.module#RollsModule'},
  {path: 'home', component: HomeComponent},
  {path: 'items', loadChildren: './items/items.module#ItemsModule'},
  {path: 'races', loadChildren: './races/races.module#RacesModule'},
  {path: 'roles', loadChildren: './roles/roles.module#RolesModule'},
  {path: 'powers', loadChildren: './powers/powers.module#PowersModule'},
  {path: 'players', loadChildren: './players/players.module#PlayersModule'},
  {path: 'encounters', loadChildren: './encounters/encounters.module#EncountersModule'},
  {path: 'heroes', loadChildren: './heroes/heroes.module#HeroesModule'},
  {path: 'monsters', loadChildren: './monsters/monsters.module#MonstersModule'},
  {path: 'tests', loadChildren: './tests/tests.module#TestsModule'},
  {path: 'combat', loadChildren: './combat/combat.module#CombatModule'}
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  declarations: [],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
