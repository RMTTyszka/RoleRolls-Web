import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RollsService } from './rolls.service';
import { RollsComponent } from './rolls.component';
import { SharedModule } from '../shared/shared.module';
import {RollsRoutingModule} from './rolls-routing.module';
import { MakeTestComponent } from './make-test/make-test.component';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    RollsRoutingModule
  ],
  declarations: [RollsComponent, MakeTestComponent],
  providers: [
    RollsService
  ]
})
export class RollsModule { }
