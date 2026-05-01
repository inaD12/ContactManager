import { Contact } from '../models/contact.model';
import { ENDPOINTS } from '../config/endpoints';
import { CreateContact } from '../models/createContract.model';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class ContactService {

  constructor(private http: HttpClient) {}

  getAll() {
    return this.http.get<Contact[]>(ENDPOINTS.contacts.root);
  }

  getById(id: string) {
    return this.http.get<Contact>(ENDPOINTS.contacts.byId(id));
  }

  create(contact: CreateContact) {
    return this.http.post(ENDPOINTS.contacts.root, contact);
  }

  update(id: string, contact: Contact) {
    return this.http.put(ENDPOINTS.contacts.byId(id), contact);
  }

  delete(id: string) {
    return this.http.delete(ENDPOINTS.contacts.byId(id));
  }
}