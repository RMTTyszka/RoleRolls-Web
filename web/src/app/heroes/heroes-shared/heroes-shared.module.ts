import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {HeroesSelectModalComponent} from './heroes-select-modal/heroes-select-modal.component';
import {HeroSelectComponent} from './hero-select/hero-select.component';
import {AutoCompleteModule} from 'primeng/autocomplete';
import {FormsModule} from '@angular/forms';
import {OverlayPanelModule} from 'primeng/overlaypanel';
import {PanelModule} from 'primeng/panel';
import {TooltipModule} from 'primeng/tooltip';
import {DynamicDialogModule} from 'primeng/dynamicdialog';
import {MasterToolsModule} from '../../masters/master-tools/master-tools.module';
import {CreaturesSharedModule} from '../../creatures-shared/creatures-shared.module';

@NgModule({
  imports: [
    CommonModule,
    AutoCompleteModule,
    FormsModule,
    PanelModule,
    TooltipModule,
    DynamicDialogModule,
    MasterToolsModule,
    CreaturesSharedModule
  ],
  declarations: [HeroesSelectModalComponent, HeroSelectComponent],
  entryComponents: [HeroesSelectModalComponent],
  exports: [HeroesSelectModalComponent, HeroSelectComponent]
})
export class HeroesSharedModule { }
