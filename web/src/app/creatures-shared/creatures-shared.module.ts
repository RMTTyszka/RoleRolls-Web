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



@NgModule({
  declarations: [InventoryArmorSelectComponent, BaseInventorySelectComponent, InventoryWeaponSelectComponent, InventoryGlovesSelectComponent, InventoryBeltSelectComponent],
  exports: [
    InventoryArmorSelectComponent, InventoryWeaponSelectComponent, InventoryGlovesSelectComponent, InventoryGlovesSelectComponent, InventoryBeltSelectComponent, InventoryBeltSelectComponent
  ],
  imports: [
    CommonModule,
    FieldsetModule,
    AutoCompleteModule,
    ReactiveFormsModule
  ]
})
export class CreaturesSharedModule { }
