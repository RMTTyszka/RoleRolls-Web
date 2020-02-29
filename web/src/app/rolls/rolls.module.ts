import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RollsService } from './rolls.service';
import { RollsComponent } from './rolls.component';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    SharedModule
  ],
  declarations: [RollsComponent],
  providers: [
    RollsService
  ]
})
export class RollsModule { }
