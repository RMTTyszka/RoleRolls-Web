import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ArmorSelectorComponent } from './armor-selector/armor-selector.component';
import {ReactiveFormsModule} from '@angular/forms';
import {AutoCompleteModule} from 'primeng/autocomplete';



@NgModule({
  declarations: [ArmorSelectorComponent],
  exports: [
    ArmorSelectorComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    AutoCompleteModule
  ]
})
export class ArmorSharedModule { }
