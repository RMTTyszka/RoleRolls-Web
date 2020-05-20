import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {HomeComponent} from './home/home.component';

const routes: Routes = [
  {path: 'rolls', loadChildren: () => import('./rolls/rolls.module').then(m => m.RollsModule)},
  {path: 'home', component: HomeComponent},
  {path: 'items', loadChildren: () => import('./items/items.module').then(m => m.ItemsModule)},
  {path: 'races', loadChildren: () => import('./races/races.module').then(m => m.RacesModule)},
  {path: 'roles', loadChildren: () => import('./roles/roles.module').then(m => m.RolesModule)},
  {path: 'powers', loadChildren: () => import('./powers/powers.module').then(m => m.PowersModule)},
  {path: 'players', loadChildren: () => import('./players/players.module').then(m => m.PlayersModule)},
  {path: 'encounters', loadChildren: () => import('./encounters/encounters.module').then(m => m.EncountersModule)},
  {path: 'heroes', loadChildren: () => import('./heroes/heroes.module').then(m => m.HeroesModule)},
  {path: 'monsters', loadChildren: () => import('./monsters/monsters.module').then(m => m.MonstersModule)},
  {path: 'tests', loadChildren: () => import('./tests/tests.module').then(m => m.TestsModule)},
  {path: 'combat', loadChildren: () => import('./combat/combat.module').then(m => m.CombatModule)}
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  declarations: [],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
