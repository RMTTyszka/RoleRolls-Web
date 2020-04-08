import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';

import {ItemsRoutingModule} from './items-routing.module';
import {ItemsComponent} from './items.component';
import {MatListModule, MatSidenavModule, MatTabsModule} from '@angular/material';
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
