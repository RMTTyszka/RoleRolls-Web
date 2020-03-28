import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeroesSelectModalComponent } from './heroes-select-modal/heroes-select-modal.component';
import { HeroSelectComponent } from './hero-select/hero-select.component';
import {AutoCompleteModule} from 'primeng/autocomplete';
import {FormsModule} from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    AutoCompleteModule,
    FormsModule
  ],
  declarations: [HeroesSelectModalComponent, HeroSelectComponent],
  entryComponents: [HeroesSelectModalComponent],
  exports: [HeroesSelectModalComponent, HeroSelectComponent]
})
export class HeroesSharedModule { }
