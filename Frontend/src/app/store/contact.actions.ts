import { createAction, props } from '@ngrx/store';
import { Contact } from '../models/contact.model';
import { GetAllContactsRequest } from '../models/get-all-contacts-request.model';
import { CreateContactRequest } from '../models/create-contract-request.model';
import { UpdateContactRequest } from '../models/update-contact-request.model';

export const loadContacts = createAction('[Contacts] Load Contacts',
  props<{ request: GetAllContactsRequest }>()
);

export const loadContactsSuccess = createAction(
  '[Contacts] Load Contacts Success',
  props<{ contacts: Contact[], totalRecords: number }>()
);

export const loadContactsFailure = createAction(
  '[Contacts] Load Contacts Failure',
  props<{ error: any }>()
);

export const createContact = createAction(
  '[Contact] Create',
  props<{ request: CreateContactRequest }>()
);

export const createContactSuccess = createAction(
  '[Contact] Create Success',
  props<{ id: string; request: CreateContactRequest }>()
);

export const createContactFailure = createAction(
  '[Contact] Create Failure',
  props<{ error: string }>()
);


export const loadContactById = createAction(
  '[Contact] Load By Id',
  props<{ id: string }>()
);

export const loadContactByIdSuccess = createAction(
  '[Contact] Load By Id Success',
  props<{ contact: Contact }>()
);

export const loadContactByIdFailure = createAction(
  '[Contact] Load By Id Failure',
  props<{ error: string }>()
);

export const setSelectedContact = createAction(
  '[Contact] Set Selected',
  props<{ contact: Contact }>()
);

export const updateContact = createAction(
  '[Contact] Update Contact',
  props<{ id: string; request: UpdateContactRequest }>()
);

export const updateContactSuccess = createAction(
  '[Contact] Update Contact Success',
  props<{ id: string; request: UpdateContactRequest }>()
);

export const updateContactFailure = createAction(
  '[Contact] Update Contact Failure',
  props<{ error: string }>()
);


export const deleteContact = createAction(
  '[Contact] Delete Contact',
  props<{ id: string }>()
);

export const deleteContactSuccess = createAction(
  '[Contact] Delete Contact Success',
  props<{ id: string }>()
);

export const deleteContactFailure = createAction(
  '[Contact] Delete Contact Failure',
  props<{ error: string }>()
);