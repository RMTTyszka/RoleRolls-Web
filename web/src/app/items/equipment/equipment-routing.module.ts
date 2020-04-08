import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {BaseArmorListComponent} from './armor/baseArmor/base-armor-list/base-armor-list.component';
import {EquipmentComponent} from './equipment.component';
import {ArmorListComponent} from './armor/armors/armor-list/armor-list.component';
import {BaseWeaponsListComponent} from './weapons/base-weapons/base-weapons-list/base-weapons-list.component';
import {WeaponModelListComponent} from './weapons/weapon-models/weapon-model-list/weapon-model-list.component';

const routes: Routes = [
  {
    path: '', component: EquipmentComponent,
    children: [
      {
        path: 'baseArmor', component: BaseArmorListComponent
      },
      {
        path: 'armor', component: ArmorListComponent
      },
      {
        path: 'baseWeapon', component: BaseWeaponsListComponent
      },
      {
        path: 'weapon', component: WeaponModelListComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EquipmentRoutingModule { }
