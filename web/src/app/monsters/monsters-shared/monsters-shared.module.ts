import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {MonsterBaseSelectorComponent} from './monster-model-selector/monster-model-selector.component';
import {SharedModule} from 'src/app/shared/shared.module';
import {MonsterSelectComponent} from './monster-select/monster-select.component';
import {OverlayPanelModule} from 'primeng/overlaypanel';
import {PanelModule} from 'primeng/panel';
import {AutoCompleteModule} from 'primeng/autocomplete';
import {CreaturesSharedModule} from '../../creatures-shared/creatures-shared.module';
import {MatFormField} from "@angular/material/form-field";
import {MatCard, MatCardContent, MatCardSubtitle, MatCardTitle} from "@angular/material/card";
import {MatPaginator} from "@angular/material/paginator";

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    OverlayPanelModule,
    PanelModule,
    AutoCompleteModule,
    CreaturesSharedModule,
    MatFormField,
    MatCard,
    MatCardTitle,
    MatCardSubtitle,
    MatCardContent,
    MatPaginator
  ],
    exports: [MonsterBaseSelectorComponent, MonsterSelectComponent],
    declarations: [MonsterBaseSelectorComponent, MonsterSelectComponent]
})
export class MonstersSharedModule { }
