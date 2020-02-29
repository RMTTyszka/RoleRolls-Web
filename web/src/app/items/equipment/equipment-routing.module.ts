import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {BaseArmorListComponent} from './armor/baseArmor/base-armor-list/base-armor-list.component';
import {EquipmentComponent} from './equipment.component';
import {ArmorListComponent} from './armor/armors/armor-list/armor-list.component';

const routes: Routes = [
  {
    path: '', component: EquipmentComponent,
    children: [
      {
        path: 'baseArmor', component: BaseArmorListComponent
      },
      {
        path: 'armor', component: ArmorListComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EquipmentRoutingModule { }
