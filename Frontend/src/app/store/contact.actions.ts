import { createAction, props } from '@ngrx/store';
import { Contact } from '../models/contact.model';
import { GetAllContactsRequest } from '../models/get-all-contacts-request.model';
import { CreateContactRequest } from '../models/create-contract-request.model';

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
  props<{ error: any }>()
);