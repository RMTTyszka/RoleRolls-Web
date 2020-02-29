import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeroesSelectModalComponent } from './heroes-select-modal/heroes-select-modal.component';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [HeroesSelectModalComponent],
  entryComponents: [HeroesSelectModalComponent],
  exports: [HeroesSelectModalComponent]
})
export class HeroesSharedModule { }
