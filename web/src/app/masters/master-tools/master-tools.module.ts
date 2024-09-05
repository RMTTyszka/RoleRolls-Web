import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UpdateCreatureToolComponent } from './update-creature-tool/update-creature-tool.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {ButtonModule} from 'primeng/button';
import {EffectsSharedModule} from '../../effects/effects-shared/effects-shared.module';
import {OverlayPanelModule} from 'primeng/overlaypanel';
import {MenuModule} from 'primeng/menu';
import {TieredMenuModule} from 'primeng/tieredmenu';
import {ChipsModule} from 'primeng/chips';
import {InputSwitchModule} from 'primeng/inputswitch';

import { BonusToolComponent } from './bonus-tool/bonus-tool.component';
import {DropdownModule} from 'primeng/dropdown';
import {SelectButtonModule} from 'primeng/selectbutton';
import { InventoryMasterToolComponent } from './inventory-master-tool/inventory-master-tool.component';
import {PanelModule} from 'primeng/panel';
import { MasterItemInstantiatorComponent } from './master-item-instantiator/master-item-instantiator.component';
import {ItemsSharedModule} from '../../items-shared/items-shared.module';
import {SharedModule} from '../../shared/shared.module';
import {InputTextModule} from 'primeng/inputtext';



@NgModule({
    declarations: [UpdateCreatureToolComponent, BonusToolComponent, InventoryMasterToolComponent, MasterItemInstantiatorComponent],
    imports: [
        CommonModule,
        ReactiveFormsModule,
        ButtonModule,
        EffectsSharedModule,
        OverlayPanelModule,
        MenuModule,
        TieredMenuModule,
        ChipsModule,
        InputSwitchModule,

        DropdownModule,
        SelectButtonModule,
        FormsModule,
        PanelModule,
        ItemsSharedModule,
        SharedModule,
        InputTextModule,
    ],
    exports: [UpdateCreatureToolComponent, MasterItemInstantiatorComponent]
})
export class MasterToolsModule { }
