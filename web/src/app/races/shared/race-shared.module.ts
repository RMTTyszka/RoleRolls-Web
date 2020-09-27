import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {MatCardModule} from '@angular/material/card';
import {SharedModule} from 'src/app/shared/shared.module';
import {RaceSelectComponent} from './race-select/race-select.component';
import {AutoCompleteModule} from 'primeng/autocomplete';

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
