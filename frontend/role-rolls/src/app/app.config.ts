import {APP_INITIALIZER, ApplicationConfig, provideAppInitializer, provideZoneChangeDetection} from '@angular/core';
import { provideRouter } from '@angular/router';
import Aura from '@primeng/themes/aura';
import Nora from '@primeng/themes/nora';
import Material from '@primeng/themes/material';
import { routes } from './app.routes';
import { HTTP_INTERCEPTORS, provideHttpClient, withFetch, withInterceptorsFromDi } from '@angular/common/http';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { providePrimeNG } from 'primeng/config';
import { ConfirmationService, MessageService } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { AuthenticationInterceptor } from './interceptors/authentication.interceptor';
import RrPrimengPreset from './theming/rr-primeng-preset';
import {asyncConfigInitializer} from '@services/configuration/app-configuration.service';

export const appConfig: ApplicationConfig = {
  providers: [provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideHttpClient(withFetch()
      ,withInterceptorsFromDi()),
    provideAnimationsAsync(),
    provideAppInitializer(asyncConfigInitializer),
    providePrimeNG({
      theme: {
        preset: RrPrimengPreset,
        options: {
          darkModeSelector: '.my-app-dark',
          cssLayer: {
            name: 'primeng',
            order: 'tailwind-base,primeng,tailwind-utilities'
          }
        }
      }
    }),
    { provide: HTTP_INTERCEPTORS, useClass: AuthenticationInterceptor, multi: true },
    MessageService,
    ConfirmationService,
    DialogService,
  ]
};
