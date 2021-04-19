import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ItemTemplateSelectComponent } from './item-template-select-modal/item-template-select.component';
import {ItemTemplateProviderModule} from './item-template-provider/item-template-provider.module';
import {AutoCompleteModule} from 'primeng/autocomplete';
import {SharedModule} from '../shared/shared.module';



@NgModule({
  declarations: [ItemTemplateSelectComponent],
  exports: [
    ItemTemplateSelectComponent
  ],
  imports: [
    CommonModule,
    ItemTemplateProviderModule,
    AutoCompleteModule,
    SharedModule
  ]
})
export class ItemsSharedModule { }
