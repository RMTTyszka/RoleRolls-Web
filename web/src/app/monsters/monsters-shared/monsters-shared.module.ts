import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {MonsterBaseSelectorComponent} from './monster-model-selector/monster-model-selector.component';
import {SharedModule} from 'src/app/shared/shared.module';
import {MonsterSelectComponent} from './monster-select/monster-select.component';
import {OverlayPanelModule} from 'primeng/overlaypanel';
import {PanelModule} from 'primeng/panel';
import {AutoCompleteModule} from 'primeng/autocomplete';
import {CreaturesSharedModule} from '../../creatures-shared/creatures-shared.module';

@NgModule({
    imports: [
        CommonModule,
        SharedModule,
        OverlayPanelModule,
        PanelModule,
        AutoCompleteModule,
        CreaturesSharedModule
    ],
    exports: [MonsterBaseSelectorComponent, MonsterSelectComponent],
    declarations: [MonsterBaseSelectorComponent, MonsterSelectComponent]
})
export class MonstersSharedModule { }
