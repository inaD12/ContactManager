import { createReducer, on } from '@ngrx/store';
import { initialState } from './contact.state';
import * as ContactActions from './contact.actions';
import { Contact } from '../models/contact.model';

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
  })),


  on(ContactActions.loadContactById, (state) => ({
    ...state,
    loading: true,
    selectedContact: null
  })),

  on(ContactActions.loadContactByIdSuccess, (state, { contact }) => ({
    ...state,
    loading: false,
    selectedContact: contact
  })),

  on(ContactActions.loadContactByIdFailure, (state) => ({
    ...state,
    loading: false
  })),

  on(ContactActions.setSelectedContact, (state, { contact }) => ({
    ...state,
    selectedContact: contact
  })),

  on(ContactActions.updateContactSuccess, (state, { id, request }) => {

    const updateContact = (c: Contact) => ({
      ...c,
      firstName: request.newFirstName ?? c.firstName,
      surname: request.newSurname ?? c.surname,
      address: request.newAddress ?? c.address,
      phoneNumber: request.newPhoneNumber ?? c.phoneNumber,
      iban: request.newIBAN ?? c.iban
    });

    return {
      ...state,

      contacts: state.contacts.map(c =>
        c.id === id ? updateContact(c) : c
      ),

      selectedContact:
        state.selectedContact?.id === id
          ? updateContact(state.selectedContact)
          : state.selectedContact,

      loading: false
    };
  }),

  on(ContactActions.updateContact, state => ({
      ...state,
      loading: true
  })),

  on(ContactActions.updateContactFailure, state => ({
      ...state,
      loading: false
  })),

  on(ContactActions.deleteContactSuccess, (state, { id }) => ({
    ...state,

    contacts: state.contacts.filter(c => c.id !== id),

    selectedContact:
        state.selectedContact?.id === id
        ? null
        : state.selectedContact,

    loading: false
  })),

  on(ContactActions.deleteContact, state => ({
    ...state,
    loading: true
  })),

  on(ContactActions.deleteContactFailure, state => ({
    ...state,
    loading: false
  }))
);