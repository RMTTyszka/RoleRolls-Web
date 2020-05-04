import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UpdateCreatureToolComponent } from './update-creature-tool/update-creature-tool.component';
import {HttpClientModule} from '@angular/common/http';
import {ReactiveFormsModule} from '@angular/forms';
import {ButtonModule} from 'primeng/button';
import {EffectsSharedModule} from '../../effects/effects-shared/effects-shared.module';
import {OverlayPanelModule} from 'primeng/overlaypanel';
import {MenuModule} from 'primeng/menu';
import {TieredMenuModule} from 'primeng/tieredmenu';



@NgModule({
  declarations: [UpdateCreatureToolComponent],
  imports: [
    CommonModule,
    HttpClientModule,
    ReactiveFormsModule,
    ButtonModule,
    EffectsSharedModule,
    OverlayPanelModule,
    MenuModule,
    TieredMenuModule
  ],
  exports: [UpdateCreatureToolComponent],
  entryComponents: [UpdateCreatureToolComponent]
})
export class MasterToolsModule { }
