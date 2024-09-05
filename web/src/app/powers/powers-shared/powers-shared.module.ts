import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {PowerManagementComponent} from './power-management/power-management.component';
import {SharedModule} from 'src/app/shared/shared.module';
import {PowerSelectorComponent} from './power-selector/power-selector.component';
import {MatPaginator} from "@angular/material/paginator";
import {MatCard, MatCardContent, MatCardSubtitle, MatCardTitle} from "@angular/material/card";
import {MatFormField} from "@angular/material/form-field";

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    MatPaginator,
    MatCardContent,
    MatCardSubtitle,
    MatCardTitle,
    MatCard,
    MatFormField
  ],
    declarations: [
        PowerManagementComponent,
        PowerSelectorComponent
    ],
    exports: [PowerManagementComponent, PowerSelectorComponent]
})
export class PowersSharedModule { }
