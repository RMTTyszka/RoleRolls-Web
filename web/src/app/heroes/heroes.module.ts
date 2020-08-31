import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {HeroesListComponent} from './heroes-list/heroes-list.component';
import {HeroesEditorComponent} from './heroes-editor/heroes-editor.component';
import {RouterModule, Routes} from '@angular/router';
import {SharedModule} from '../shared/shared.module';
import {RaceSharedModule} from '../races/shared/race-shared.module';
import {RolesSharedModule} from '../roles/roles-shared/roles-shared.module';
import {HeroesGatewayComponent} from './heroes-gateway/heroes-gateway.component';
import {NewHeroEditorComponent} from './new-hero-editor/new-hero-editor.component';
import {DialogModule} from 'primeng/dialog';
import {NewHeroAddButtonComponent} from './new-hero-add-button/new-hero-add-button.component';
import {DynamicDialogModule} from 'primeng/dynamicdialog';
import {TooltipModule} from 'primeng/tooltip';
import {ToastModule} from 'primeng/toast';
import {TabViewModule} from 'primeng/tabview';
import {EquipmentComponent} from './equipment/equipment.component';
import {ArmorSharedModule} from '../items/equipment/armor/armors/armor-shared/armor-shared.module';
import {FieldsetModule} from 'primeng/fieldset';
import {HeroStatsComponent} from './hero-stats/hero-stats.component';
import {InventoryComponent} from './inventory/inventory.component';
import {AutoCompleteModule} from 'primeng/autocomplete';
import {CreaturesSharedModule} from '../creatures-shared/creatures-shared.module';
import { CreateHeroEquipmentComponent } from './create-hero-equipment/create-hero-equipment.component';
import { CreateHeroWeaponSelectComponent } from './create-hero-equipment/create-hero-weapon-select/create-hero-weapon-select.component';
import { CreateHeroArmorSelectComponentComponent } from './create-hero-equipment/create-hero-armor-select-component/create-hero-armor-select-component.component';
import { HeroFundsComponent } from './hero-funds/hero-funds.component';
import {ShopModule} from '../shop/shop.module';

const routes: Routes = [
  {path: '', component: HeroesGatewayComponent}
];

@NgModule({
    imports: [
        CommonModule,
        RouterModule.forChild(routes),
        SharedModule,
        DialogModule,
        DynamicDialogModule,
        RaceSharedModule,
        RolesSharedModule,
        TooltipModule,
        ToastModule,
        TabViewModule,
        ArmorSharedModule,
        FieldsetModule,
        AutoCompleteModule,
        CreaturesSharedModule,
        ShopModule
    ],
  declarations: [HeroesListComponent, HeroesEditorComponent, HeroesGatewayComponent, NewHeroEditorComponent, NewHeroAddButtonComponent, EquipmentComponent, HeroStatsComponent, InventoryComponent, CreateHeroEquipmentComponent, CreateHeroWeaponSelectComponent, CreateHeroArmorSelectComponentComponent, HeroFundsComponent],
  entryComponents: [HeroesGatewayComponent, HeroesEditorComponent, NewHeroEditorComponent]
})
export class HeroesModule { }
