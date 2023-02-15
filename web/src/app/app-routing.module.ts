import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {HomeComponent} from './home/home.component';
import {HeroesColorConfig} from './heroes/heroes-color-config';
import {MonstersColorConfig} from './monsters/monsters-color-config';
import {ItemsColorConfig} from './items/items-color-config';

const routes: Routes = [
  {path: 'pocket', loadChildren: () => import('./pocket-role-rolls/pocket-role-rolls.module').then(m => m.PocketRoleRollsModule)},
  {path: 'login', loadChildren: () => import('./login/login.module').then(m => m.LoginModule)},
  {path: 'rolls', loadChildren: () => import('./rolls/rolls.module').then(m => m.RollsModule)},
  {path: 'home', component: HomeComponent},
  {path: 'items', loadChildren: () => import('./items/items.module').then(m => m.ItemsModule), data: { colors: new ItemsColorConfig()}},
  {path: 'races', loadChildren: () => import('./races/races.module').then(m => m.RacesModule)},
  {path: 'roles', loadChildren: () => import('./roles/roles.module').then(m => m.RolesModule)},
  {path: 'powers', loadChildren: () => import('./powers/powers.module').then(m => m.PowersModule)},
  {path: 'players', loadChildren: () => import('./players/players.module').then(m => m.PlayersModule)},
  {path: 'encounters', loadChildren: () => import('./encounters/encounters.module').then(m => m.EncountersModule)},
  {path: 'heroes', loadChildren: () => import('./heroes/heroes.module').then(m => m.HeroesModule), data: {colors: new HeroesColorConfig()}},
  {path: 'monsters', loadChildren: () => import('./monsters/monsters.module').then(m => m.MonstersModule),
    data: {colors: new MonstersColorConfig()}},
  {path: 'tests', loadChildren: () => import('./tests/tests.module').then(m => m.TestsModule)},
  {path: 'combat', loadChildren: () => import('./combat/combat.module').then(m => m.CombatModule)},
  {path: 'campaigns', loadChildren: () => import('./campaign/campaign.module').then(m => m.CampaignModule)},
  {path: 'campaign-session', loadChildren: () => import('./campaign-session/campaign-session.module').then(m => m.CampaignSessionModule)},
  {path: '**', redirectTo: 'home'}
];

@NgModule({
  imports: [ RouterModule.forRoot(routes, {enableTracing: false}) ],
  declarations: [],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
