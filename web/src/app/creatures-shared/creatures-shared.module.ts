import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InventoryArmorSelectComponent } from './inventory/inventory-armor-select/inventory-armor-select.component';
import {AutoCompleteModule} from 'primeng/autocomplete';
import {FieldsetModule} from 'primeng/fieldset';
import { ReactiveFormsModule } from '@angular/forms';
import { BaseInventorySelectComponent } from './inventory/base-inventory-select/base-inventory-select.component';
import { InventoryWeaponSelectComponent } from './inventory/inventory-weapon-select/inventory-weapon-select.component';
import { InventoryGlovesSelectComponent } from './inventory/inventory-glove-select/inventory-gloves-select.component';
import { InventoryBeltSelectComponent } from './inventory/inventory-belt-select/inventory-belt-select.component';
import { InventoryHeadpieceSelectComponent } from './inventory/inventory-headpiece-select/inventory-headpiece-select.component';
import { CreatureResistancesComponent } from './creature-resistances/creature-resistances.component';
import { InventoryNeckaccesorySelectComponent } from './inventory/inventory-neckaccesory-select/inventory-neckaccesory-select.component';
import { InventoryRingSelectComponent } from './inventory/inventory-ring-select/inventory-ring-select.component';
import { AttackDetailsComponent } from './attack-details/attack-details.component';
import {PanelModule} from 'primeng/panel';
import {FlexLayoutModule} from '@angular/flex-layout';



@NgModule({
  declarations: [InventoryArmorSelectComponent, BaseInventorySelectComponent, InventoryWeaponSelectComponent, InventoryGlovesSelectComponent, InventoryBeltSelectComponent, InventoryHeadpieceSelectComponent, CreatureResistancesComponent, InventoryNeckaccesorySelectComponent, InventoryRingSelectComponent, AttackDetailsComponent],
  exports: [
    InventoryArmorSelectComponent, InventoryWeaponSelectComponent, InventoryGlovesSelectComponent, InventoryGlovesSelectComponent, InventoryBeltSelectComponent, InventoryBeltSelectComponent, InventoryHeadpieceSelectComponent, CreatureResistancesComponent, InventoryNeckaccesorySelectComponent, InventoryRingSelectComponent, AttackDetailsComponent
  ],
  imports: [
    CommonModule,
    FieldsetModule,
    AutoCompleteModule,
    ReactiveFormsModule,
    PanelModule,
    FlexLayoutModule
  ]
})
export class CreaturesSharedModule { }
