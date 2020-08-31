import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShopComponent } from './shop/shop.component';
import { ShopListComponent } from './shop-list/shop-list.component';
import {CheckboxModule} from 'primeng/checkbox';
import {SharedModule} from '../shared/shared.module';
import {PickListModule} from 'primeng/picklist';
import {PanelModule} from 'primeng/panel';



@NgModule({
  declarations: [ShopComponent, ShopListComponent],
  exports: [
    ShopComponent
  ],
    imports: [
        CommonModule,
        CheckboxModule,
        SharedModule,
        PickListModule,
        PanelModule
    ]
})
export class ShopModule { }
