import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {MonsterBaseSelectorComponent} from './monster-model-selector/monster-model-selector.component';
import {SharedModule} from 'src/app/shared/shared.module';
import {MonsterSelectComponent} from './monster-select/monster-select.component';
import {OverlayPanelModule} from 'primeng/overlaypanel';
import {PanelModule} from 'primeng/panel';
import {AutoCompleteModule} from 'primeng/autocomplete';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    OverlayPanelModule,
    PanelModule,
    AutoCompleteModule
  ],
  exports: [MonsterBaseSelectorComponent, MonsterSelectComponent],
  declarations: [MonsterBaseSelectorComponent, MonsterSelectComponent],
  entryComponents: [MonsterBaseSelectorComponent]
})
export class MonstersSharedModule { }
