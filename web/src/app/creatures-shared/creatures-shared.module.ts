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



@NgModule({
  declarations: [InventoryArmorSelectComponent, BaseInventorySelectComponent, InventoryWeaponSelectComponent, InventoryGlovesSelectComponent, InventoryBeltSelectComponent, InventoryHeadpieceSelectComponent, CreatureResistancesComponent, InventoryNeckaccesorySelectComponent],
  exports: [
    InventoryArmorSelectComponent, InventoryWeaponSelectComponent, InventoryGlovesSelectComponent, InventoryGlovesSelectComponent, InventoryBeltSelectComponent, InventoryBeltSelectComponent, InventoryHeadpieceSelectComponent, CreatureResistancesComponent, InventoryNeckaccesorySelectComponent
  ],
  imports: [
    CommonModule,
    FieldsetModule,
    AutoCompleteModule,
    ReactiveFormsModule
  ]
})
export class CreaturesSharedModule { }
