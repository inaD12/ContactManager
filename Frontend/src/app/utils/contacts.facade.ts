import { inject, Injectable } from "@angular/core";
import { Store } from "@ngrx/store";
import * as ContactSelectors from '../store/contact.selectors';
import * as ContactActions from '../store/contact.actions';
import { take } from "rxjs";
import { CreateContactRequest } from "../models/create-contract-request.model";
import { GetAllContactsRequest } from "../models/get-all-contacts-request.model";
import { UpdateContactRequest } from "../models/update-contact-request.model";
import { Contact } from "../models/contact.model";

@Injectable({ providedIn: 'root' })
export class ContactsFacade {
  private store = inject(Store);

  loading = this.store.selectSignal(ContactSelectors.selectLoading);
  contact = this.store.selectSignal(ContactSelectors.selectSelectedContact);
  contacts = this.store.selectSignal(ContactSelectors.selectAllContacts);
  totalRecords = this.store.selectSignal(ContactSelectors.selectTotalRecords);
  createSuccess = this.store.selectSignal(ContactSelectors.selectCreateSuccess);
  createError = this.store.selectSignal(ContactSelectors.selectCreateError);

  loadContactById(id: string) {
    this.store.dispatch(ContactActions.loadContactById({ id }));
  }

  setSelectedContact(contact: Contact) {
    this.store.dispatch(ContactActions.setSelectedContact({ contact }));
  }

  loadContactIfMissing(id: string) {
    this.store.select(ContactSelectors.selectContactByIdFromList(id))
      .pipe(take(1))
      .subscribe(existing => {
        if (existing) {
          this.setSelectedContact(existing);
        } else {
          this.loadContactById(id);
        }
      });
  }

  updateContact(id: string, request: UpdateContactRequest) {
    this.store.dispatch(ContactActions.updateContact({ id, request }));
  }

  createContact(request: CreateContactRequest) {
    this.store.dispatch(ContactActions.createContact({ request }));
  }

  deleteContact(id: string) {
    this.store.dispatch(ContactActions.deleteContact({ id }));
  }

  loadContacts(request: GetAllContactsRequest) {
    this.store.dispatch(ContactActions.loadContacts({ request }));
  }
}