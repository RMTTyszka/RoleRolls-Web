import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {ItemsComponent} from './items.component';

const routes: Routes = [
  {
    path: '', component: ItemsComponent,
    children: [
      {
        path: 'equipment', loadChildren: './equipment/equipment.module#EquipmentModule'
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ItemsRoutingModule { }
