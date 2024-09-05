import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';

import {ItemsRoutingModule} from './items-routing.module';
import {ItemsComponent} from './items.component';
import { MatSidenavModule } from '@angular/material/sidenav';


@NgModule({
  imports: [
    CommonModule,
    ItemsRoutingModule,
    MatSidenavModule,
  ],
  declarations: [ItemsComponent]
})
export class ItemsModule { }
