import { routes } from './app.routes';
import { providePrimeNG } from 'primeng/config';
import Aura from '@primeuix/themes/aura';
import { ApplicationConfig, provideBrowserGlobalErrorListeners } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient } from '@angular/common/http';
import { provideStore } from '@ngrx/store';
import { contactReducer } from './store/contact.reducer';
import { provideEffects } from '@ngrx/effects';
import { ContactEffects } from './store/contact.effects';
import { ConfirmationService } from 'primeng/api';

export const appConfig: ApplicationConfig = {
  providers: [
    provideHttpClient(),
    provideBrowserGlobalErrorListeners(),
    provideRouter(routes),
    provideStore({
      contacts: contactReducer
    }),
    ConfirmationService,
    provideEffects([ContactEffects]),
    providePrimeNG({
      theme: {
        preset: Aura,
        options: {
          cssLayer: {
            name: 'primeng',
            order: 'reset, primeng, theme'
          }
        }
      }
    })
  ]
};
