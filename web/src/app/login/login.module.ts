import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LoginRoutingModule } from './login-routing.module';
import { LoginComponent } from './login/login.component';
import {ReactiveFormsModule} from '@angular/forms';
import {PanelModule} from 'primeng/panel';



@NgModule({
    declarations: [LoginComponent],
    exports: [
        LoginComponent
    ],
  imports: [
    CommonModule,
    LoginRoutingModule,
    ReactiveFormsModule,
    PanelModule,

  ]
})
export class LoginModule { }
