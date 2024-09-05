import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {SharedModule} from 'src/app/shared/shared.module';
import {RaceSelectComponent} from './race-select/race-select.component';
import {AutoCompleteModule} from 'primeng/autocomplete';
import {MatCardModule} from "@angular/material/card";

@NgModule({
  imports: [
    CommonModule,
    MatCardModule,
    SharedModule,
    AutoCompleteModule
  ],
  declarations: [RaceSelectComponent],
  exports: [RaceSelectComponent],
})
export class RaceSharedModule { }
