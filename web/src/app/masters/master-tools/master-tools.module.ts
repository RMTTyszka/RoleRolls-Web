import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UpdateCreatureToolComponent } from './update-creature-tool/update-creature-tool.component';
import {HttpClientModule} from '@angular/common/http';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {ButtonModule} from 'primeng/button';
import {EffectsSharedModule} from '../../effects/effects-shared/effects-shared.module';
import {OverlayPanelModule} from 'primeng/overlaypanel';
import {MenuModule} from 'primeng/menu';
import {TieredMenuModule} from 'primeng/tieredmenu';
import {ChipsModule} from 'primeng/chips';
import {InputSwitchModule} from 'primeng/inputswitch';
import {FlexLayoutModule} from '@angular/flex-layout';
import { BonusToolComponent } from './bonus-tool/bonus-tool.component';
import {DropdownModule} from 'primeng/dropdown';
import {SelectButtonModule} from 'primeng/selectbutton';



@NgModule({
  declarations: [UpdateCreatureToolComponent, BonusToolComponent],
  imports: [
    CommonModule,
    HttpClientModule,
    ReactiveFormsModule,
    ButtonModule,
    EffectsSharedModule,
    OverlayPanelModule,
    MenuModule,
    TieredMenuModule,
    ChipsModule,
    InputSwitchModule,
    FlexLayoutModule,
    DropdownModule,
    SelectButtonModule,
    FormsModule
  ],
  exports: [UpdateCreatureToolComponent],
  entryComponents: [UpdateCreatureToolComponent]
})
export class MasterToolsModule { }
