import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {AppComponent} from './app.component';
import {AppRoutingModule} from './app-routing.module';
import {HomeComponent} from './home/home.component';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {SharedModule} from './shared/shared.module';
import {DeviceDetectorModule} from 'ngx-device-detector';
import {CommonModule} from '@angular/common';
import {MessageService} from 'primeng/api';
import {AuthenticationInterceptor} from './interceptors/authentication.interceptor';
import {LoginModule} from './login/login.module';
import { MainHeaderComponent } from './main-header/main-header.component';
import {ShopModule} from './shop/shop.module';


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    MainHeaderComponent
  ],
    imports: [
        CommonModule,
        BrowserModule,
        AppRoutingModule,
        HttpClientModule,
        BrowserAnimationsModule,
        SharedModule,
        DeviceDetectorModule.forRoot(),
        LoginModule,
        ShopModule
    ],
  providers: [MessageService,
  { provide: HTTP_INTERCEPTORS, useClass: AuthenticationInterceptor, multi: true }],
  bootstrap: [AppComponent]
})
export class AppModule { }
