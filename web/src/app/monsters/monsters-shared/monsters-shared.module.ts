import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {MonsterBaseSelectorComponent} from './monster-model-selector/monster-model-selector.component';
import {SharedModule} from 'src/app/shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    SharedModule
  ],
  exports: [MonsterBaseSelectorComponent],
  declarations: [MonsterBaseSelectorComponent],
  entryComponents: [MonsterBaseSelectorComponent]
})
export class MonstersSharedModule { }
