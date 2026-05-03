import { createSelector, createFeatureSelector } from '@ngrx/store';
import { ContactState } from './contact.state';

export const selectContactsState =
  createFeatureSelector<ContactState>('contacts');

export const selectAllContacts = createSelector(
  selectContactsState,
  state => state.contacts
);

export const selectLoading = createSelector(
  selectContactsState,
  state => state.loading
);

export const selectTotalRecords = createSelector(
  selectContactsState,
  state => state.totalRecords
);