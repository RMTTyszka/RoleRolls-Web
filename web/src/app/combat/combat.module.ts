import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {CombatComponent} from './combat.component';
import {RouterModule, Routes} from '@angular/router';
import {HeroesSharedModule} from '../heroes/heroes-shared/heroes-shared.module';

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
import { CombatLogComponent } from './combat-log/combat-log.component';
import {PanelModule} from 'primeng/panel';
import {MenuModule} from 'primeng/menu';
import {TieredMenuModule} from 'primeng/tieredmenu';


@NgModule({
    imports: [
        CommonModule,
        HeroesSharedModule,

        CreaturesSharedModule,
        SharedModule,
        ToolbarModule,
        InputTextModule,
        MonstersSharedModule,
        SelectButtonModule,
        ReactiveFormsModule,
        SidebarModule,
        PanelModule,
        MenuModule,
        TieredMenuModule
    ],
    declarations: [CombatComponent, CombatListComponent, InitiativeComponent, CombatActionModalComponent, CombatLogComponent],
    exports: [CombatComponent, CombatListComponent, InitiativeComponent, CombatActionModalComponent, CombatLogComponent]
})
export class CombatModule { }
