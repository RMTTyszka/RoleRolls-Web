import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {PowersComponent} from './powers/powers.component';
import {RouterModule, Routes} from '@angular/router';
import {PowersService} from './powers.service';
import {PowerEditorComponent} from './powers/power-editor/power-editor.component';
import {SharedModule} from '../shared/shared.module';

const routes: Routes = [
  {path: '', component: PowersComponent}
];

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forChild(routes)
  ],
  declarations: [PowersComponent, PowerEditorComponent],
  providers: [
    PowersService
  ],
  entryComponents: [PowerEditorComponent]
})
export class PowersModule { }
