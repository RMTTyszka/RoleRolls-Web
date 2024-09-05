import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {PowersComponent} from './powers/powers.component';
import {RouterModule, Routes} from '@angular/router';
import {PowersService} from './powers.service';
import {PowerEditorComponent} from './powers/power-editor/power-editor.component';
import {SharedModule} from '../shared/shared.module';
import {MatCard, MatCardContent, MatCardSubtitle, MatCardTitle} from "@angular/material/card";
import {MatPaginator} from "@angular/material/paginator";

const routes: Routes = [
  {path: '', component: PowersComponent}
];

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forChild(routes),
    MatCard,
    MatCardTitle,
    MatCardSubtitle,
    MatCardContent,
    MatPaginator
  ],
    declarations: [PowersComponent, PowerEditorComponent],
    providers: [
        PowersService
    ]
})
export class PowersModule { }
