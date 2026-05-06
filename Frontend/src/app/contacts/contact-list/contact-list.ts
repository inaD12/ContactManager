import { Component, computed, OnInit, signal } from '@angular/core';
import { CommonModule} from '@angular/common';
import { TableLazyLoadEvent, TableModule } from 'primeng/table';
import { SkeletonModule } from 'primeng/skeleton';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';

import { Subject, debounceTime } from 'rxjs';

import { GetAllContactsRequest } from '../../models/get-all-contacts-request.model';
import { mapSortField, SortOrder } from '../../models/contact.enums';
import { Router, RouterLink } from '@angular/router';
import { inject } from '@angular/core';
import { ConfirmationService } from 'primeng/api';
import { ContactsFacade } from '../../utils/contacts.facade';
import { Contact } from '../../models/contact.model';

@Component({
  selector: 'app-contact-list',
  standalone: true,
  imports: [
    CommonModule,
    TableModule,
    SkeletonModule,
    InputTextModule,
    ButtonModule,
    RouterLink
  ],
  templateUrl: './contact-list.html',
})
export class ContactList implements OnInit {

  private facade = inject(ContactsFacade);
  private router = inject(Router);
  private confirm = inject(ConfirmationService);

  contacts = this.facade.contacts;
  loading = this.facade.loading;
  totalRecords = this.facade.totalRecords;

  filters: GetAllContactsRequest = {
    page: 1,
    pageSize: 10,
    firstName: '',
    surname: '',
    phoneNumber: '',
    address: '',
    minDateOfBirth: '',
    maxDateOfBirth: ''
  };

  showPagination = computed(() => {
    const total = this.totalRecords();
    const size = this.filters.pageSize ?? 0;

    return size > 0 && total > size;
  });

  private filterSubject = new Subject<void>();

  ngOnInit() {
    this.filterSubject
      .pipe(debounceTime(300))
      .subscribe(() => {
        this.filters.page = 1;
        this.facade.loadContacts(this.filters);
      });

    this.facade.loadContacts(this.filters);
  }

  onFilter(field: 'firstName' | 'surname', value: string) {
    this.filters = {
      ...this.filters,
      [field]: value
    };

    this.filterSubject.next();
  }

  loadContactsLazy(event: TableLazyLoadEvent) {

    this.filters = {
      ...this.filters,
      page: ((event.first ?? 0) / (event.rows ?? 10)) + 1,
      pageSize: event.rows ?? 10
    };

    if (event.sortField) {
      const sortField = Array.isArray(event.sortField)
        ? event.sortField[0]
        : event.sortField;

      const mapped = mapSortField(sortField);

      if (mapped !== null) {
        this.filters.sortBy = mapped;
        this.filters.sortOrder =
          event.sortOrder === 1 ? SortOrder.ASC : SortOrder.DESC;
      }
    }

    this.facade.loadContacts(this.filters);
  }

  openContact(contact: { id: string }) {
    this.router.navigate(['/contacts', contact.id]);
  }

  editContact(contact: { id: string }) {
    this.router.navigate(['/contacts', contact.id, 'edit']);
  }

  deleteContact(contact: Contact) {
    this.confirm.confirm({
      message: 'Are you sure you want to delete this contact?',
      header: 'Confirm Delete',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.facade.deleteContact(contact.id);
      }
    });
  }
}