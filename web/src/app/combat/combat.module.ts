import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {CombatComponent} from './combat.component';
import {RouterModule, Routes} from '@angular/router';
import {HeroesSharedModule} from '../heroes/heroes-shared/heroes-shared.module';
import {FlexLayoutModule} from '@angular/flex-layout';
import {CreaturesSharedModule} from '../creatures-shared/creatures-shared.module';
import { CombatListComponent } from './combat-list/combat-list.component';
import {SharedModule} from '../shared/shared.module';
import {ToolbarModule} from 'primeng/toolbar';
import {InputTextModule} from 'primeng/inputtext';
import {MonstersSharedModule} from '../monsters/monsters-shared/monsters-shared.module';
import { InitiativeComponent } from './initiative/initiative.component';
import { CombatActionModalComponent } from './action-modal/combat-action-modal.component';
import {SelectButtonModule} from 'primeng/selectbutton';
import {ReactiveFormsModule} from '@angular/forms';
import {SidebarModule} from 'primeng/sidebar';

const routes: Routes = [
  {path: '', component: CombatListComponent},
  {path: 'manage-combat', component: CombatComponent},
  {path: 'manage-combat/:id', component: CombatComponent},
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    HeroesSharedModule,
    FlexLayoutModule,
    CreaturesSharedModule,
    SharedModule,
    ToolbarModule,
    InputTextModule,
    MonstersSharedModule,
    SelectButtonModule,
    ReactiveFormsModule,
    SidebarModule
  ],
  declarations: [CombatComponent, CombatListComponent, InitiativeComponent, CombatActionModalComponent],
  entryComponents: [CombatActionModalComponent]
})
export class CombatModule { }
