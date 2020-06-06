import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';

import {ItemsRoutingModule} from './items-routing.module';
import {ItemsComponent} from './items.component';
import { MatListModule } from '@angular/material/list';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatTabsModule } from '@angular/material/tabs';
import {FlexLayoutModule} from '@angular/flex-layout';

@NgModule({
  imports: [
    CommonModule,
    ItemsRoutingModule,
    MatSidenavModule,
    MatListModule,
    FlexLayoutModule,
    MatTabsModule
  ],
  declarations: [ItemsComponent]
})
export class ItemsModule { }
