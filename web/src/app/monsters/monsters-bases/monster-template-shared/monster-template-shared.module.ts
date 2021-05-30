import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {MonsterTemplateSelectComponent} from './monster-template-select/monster-template-select.component';
import {SharedModule} from '../../../shared/shared.module';
import {InputTextModule} from 'primeng/inputtext';
import {MonsterTemplateProviderModule} from '../monster-template-provider/monster-template-provider.module';



@NgModule({
  declarations: [MonsterTemplateSelectComponent],
  imports: [
    CommonModule,
    // PrimeNg
    InputTextModule,

    // RR
    SharedModule,
    MonsterTemplateProviderModule
  ],
   exports: [MonsterTemplateSelectComponent]
})
export class MonsterTemplateSharedModule { }
