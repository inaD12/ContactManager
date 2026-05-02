import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'create',
    pathMatch: 'full'
  },
  {
    path: 'create',
    loadComponent: () =>
      import('./contacts/contact-create/contact-create')
        .then(m => m.ContactCreate)
  },
  {
    path: 'list',
    loadComponent: () =>
      import('./contacts/contact-list/contact-list')
        .then(m => m.ContactList)
  }
];