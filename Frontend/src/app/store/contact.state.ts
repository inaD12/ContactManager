import { Contact } from '../models/contact.model';

export type ContactState = {
  contacts: Contact[];
  totalRecords: number;
  loading: boolean;
  createSuccess: boolean;
  createError: string | null;
};

export const initialState: ContactState = {
  contacts: [],
  totalRecords: 0,
  loading: false,
  createSuccess: false,
  createError: null
};