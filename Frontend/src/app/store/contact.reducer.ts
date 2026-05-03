import { createReducer, on } from '@ngrx/store';
import { initialState } from './contact.state';
import * as ContactActions from './contact.actions';

export const contactReducer = createReducer(
  initialState,

  on(ContactActions.loadContacts, state => ({
    ...state,
    loading: true
  })),

  on(ContactActions.loadContactsSuccess, (state, { contacts, totalRecords }) => ({
    ...state,
    contacts,
    totalRecords,
    loading: false
  })),

  on(ContactActions.loadContactsFailure, state => ({
    ...state,
    loading: false
  })),

  on(ContactActions.createContact, state => ({
    ...state,
    loading: true,
    createSuccess: false,
    createError: null
  })),

  on(ContactActions.createContactSuccess, (state, { id, request }) => ({
    ...state,
    contacts: [
        {
        id,
        ...request
        },
        ...state.contacts
    ],
    createSuccess: true,
    loading: false
  })),

  on(ContactActions.createContactFailure, (state, { error }) => ({
    ...state,
    loading: false,
    createSuccess: false,
    createError: error
  }))

);