import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShopComponent } from './shop/shop.component';
import { ShopListComponent } from './shop-list/shop-list.component';
import {CheckboxModule} from 'primeng/checkbox';
import {SharedModule} from '../shared/shared.module';
import {PickListModule} from 'primeng/picklist';
import {PanelModule} from 'primeng/panel';
import { MonsterShopComponent } from './monster-shop/monster-shop.component';
import {RadioButtonModule} from 'primeng/radiobutton';



@NgModule({
  declarations: [ShopComponent, ShopListComponent, MonsterShopComponent],
  exports: [
    ShopComponent,
    MonsterShopComponent,
    MonsterShopComponent
  ],
    imports: [
        CommonModule,
        CheckboxModule,
        SharedModule,
        PickListModule,
        PanelModule,
        RadioButtonModule
    ]
})
export class ShopModule { }
