import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InventoryArmorSelectComponent } from './inventory/inventory-armor-select/inventory-armor-select.component';
import {AutoCompleteModule} from 'primeng/autocomplete';
import {FieldsetModule} from 'primeng/fieldset';
import { ReactiveFormsModule } from '@angular/forms';
import { BaseInventorySelectComponent } from './inventory/base-inventory-select/base-inventory-select.component';
import { InventoryWeaponSelectComponent } from './inventory/inventory-weapon-select/inventory-weapon-select.component';
import { InventoryGlovesSelectComponent } from './inventory/inventory-glove-select/inventory-gloves-select.component';



@NgModule({
  declarations: [InventoryArmorSelectComponent, BaseInventorySelectComponent, InventoryWeaponSelectComponent, InventoryGlovesSelectComponent],
  exports: [
    InventoryArmorSelectComponent, InventoryWeaponSelectComponent, InventoryGlovesSelectComponent, InventoryGlovesSelectComponent
  ],
  imports: [
    CommonModule,
    FieldsetModule,
    AutoCompleteModule,
    ReactiveFormsModule
  ]
})
export class CreaturesSharedModule { }
