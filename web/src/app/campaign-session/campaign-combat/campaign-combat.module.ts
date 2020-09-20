import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CompaignCombatListComponent } from './compaign-combat-list/compaign-combat-list.component';
import {CombatModule} from '../../combat/combat.module';



@NgModule({
  declarations: [CompaignCombatListComponent],
  imports: [
    CommonModule,
    CombatModule
  ]
})
export class CampaignCombatModule { }
