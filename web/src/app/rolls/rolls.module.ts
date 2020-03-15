import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RollsService } from './rolls.service';
import { RollsComponent } from './rolls.component';
import { SharedModule } from '../shared/shared.module';
import {RollsRoutingModule} from './rolls-routing.module';
import { MakeTestComponent } from './make-test/make-test.component';
import {FlexLayoutModule} from '@angular/flex-layout';
import {InputTextModule} from 'primeng/inputtext';
import {KeyFilterModule} from 'primeng/keyfilter';
import {InputMaskModule} from 'primeng/inputmask';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {CardModule} from 'primeng/card';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    RollsRoutingModule,

    FlexLayoutModule,
    ReactiveFormsModule,
    FormsModule,
    InputTextModule,
    KeyFilterModule,
    InputMaskModule,
    CardModule
  ],
  declarations: [RollsComponent, MakeTestComponent],
  providers: [
    RollsService
  ]
})
export class RollsModule { }
