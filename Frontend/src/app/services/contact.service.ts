import { Contact } from '../models/contact.model';
import { ENDPOINTS } from '../config/endpoints';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { APIResponse } from '../models/api-response.model';
import { PaginatedResponse } from '../models/paginated-response.model';
import { ContactCommandResponse } from '../models/contact-command-response.model';
import { UpdateContactRequest } from '../models/update-contact-request.model';
import { GetAllContactsRequest } from '../models/get-all-contacts-request.model';
import { CreateContactRequest } from '../models/create-contract-request.model';

@Injectable({
  providedIn: 'root',
})
export class ContactService {

  constructor(private http: HttpClient) {}

  getAll(request: GetAllContactsRequest) {
    return this.http.get<APIResponse<PaginatedResponse<Contact>>>(
      ENDPOINTS.contacts.root,
      {
        params: { ...request }
      }
    );
  }

  getById(id: string) {
    return this.http.get<APIResponse<Contact>>(
      ENDPOINTS.contacts.byId(id)
    );
  }

  create(request: CreateContactRequest) {
    return this.http.post<APIResponse<ContactCommandResponse>>(
      ENDPOINTS.contacts.root,
      request
    );
  }

  update(id: string, request: UpdateContactRequest) {
    return this.http.put<APIResponse<null>>(
      ENDPOINTS.contacts.byId(id),
      request
    );
  }

  delete(id: string) {
    return this.http.delete<APIResponse<null>>(
      ENDPOINTS.contacts.byId(id)
    );
  }
}