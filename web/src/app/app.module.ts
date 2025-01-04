import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {AppComponent} from './app.component';
import {AppRoutingModule} from './app-routing.module';
import {HomeComponent} from './home/home.component';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {SharedModule} from './shared/shared.module';
import {CommonModule} from '@angular/common';
import {ConfirmationService, MessageService} from 'primeng/api';
import {AuthenticationInterceptor} from './interceptors/authentication.interceptor';
import {LoginModule} from './login/login.module';
import { MainHeaderComponent } from './main-header/main-header.component';
import {ShopModule} from './shop/shop.module';
import {ConfirmDialogModule} from 'primeng/confirmdialog';
import {UniverseInterceptor} from './interceptors/universe.interceptor';
import {AppColorService} from './app-color.service';
import {MatTabLink, MatTabNav, MatTabNavPanel} from '@angular/material/tabs';
import { providePrimeNG } from 'primeng/config';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import Nora from '@primeng/themes/nora';
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
    LoginModule,
    ShopModule,
    ConfirmDialogModule,
    MatTabNav,
    MatTabLink,
    MatTabNavPanel,
  ],
  providers: [
    MessageService,
    ConfirmationService,
  { provide: HTTP_INTERCEPTORS, useClass: AuthenticationInterceptor, multi: true },
  { provide: HTTP_INTERCEPTORS, useClass: UniverseInterceptor, multi: true },
    provideAnimationsAsync(),
    providePrimeNG({
      theme: {
        preset: Nora,
        options: {
          cssLayer: {
            name: 'primeng',
            order: 'tailwind-base,primeng,tailwind-utilities'
          }
        }
      }
    }),
    ],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor(appColorService: AppColorService) {
    setTimeout(() => {
      appColorService.init();
    }, 0);
  }
}

