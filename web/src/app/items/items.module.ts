import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';

import {ItemsRoutingModule} from './items-routing.module';
import {ItemsComponent} from './items.component';
import { MatLegacyListModule as MatListModule } from '@angular/material/legacy-list';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatLegacyTabsModule as MatTabsModule } from '@angular/material/legacy-tabs';


@NgModule({
  imports: [
    CommonModule,
    ItemsRoutingModule,
    MatSidenavModule,
    MatListModule,

    MatTabsModule
  ],
  declarations: [ItemsComponent]
})
export class ItemsModule { }
