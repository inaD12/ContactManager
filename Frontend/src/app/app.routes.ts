import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'list',
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
  },
  {
    path: 'contacts/:id',
    loadComponent: () =>
      import('./contacts/contact-view/contact-view')
        .then(m => m.ContactView)
  },
  {
    path: 'contacts/:id/edit',
    loadComponent: () =>
      import('./contacts/contact-edit/contact-edit')
        .then(m => m.ContactEdit)
  }
];