import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InventoryArmorSelectComponent } from './inventory-armor-select/inventory-armor-select.component';
import {AutoCompleteModule} from 'primeng/autocomplete';
import {FieldsetModule} from 'primeng/fieldset';
import { ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [InventoryArmorSelectComponent],
  exports: [
    InventoryArmorSelectComponent
  ],
  imports: [
    CommonModule,
    FieldsetModule,
    AutoCompleteModule,
    ReactiveFormsModule
  ]
})
export class CreaturesSharedModule { }
