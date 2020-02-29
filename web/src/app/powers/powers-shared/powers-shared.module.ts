import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PowerManagementComponent } from './power-management/power-management.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { PowerSelectorComponent } from './power-selector/power-selector.component';

@NgModule({
  imports: [
    CommonModule,
    SharedModule
  ],
  declarations: [
    PowerManagementComponent,
    PowerSelectorComponent
  ],
  exports: [PowerManagementComponent, PowerSelectorComponent],
  entryComponents: [PowerSelectorComponent]
})
export class PowersSharedModule { }
