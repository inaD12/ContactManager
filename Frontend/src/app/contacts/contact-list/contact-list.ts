import { Component, computed, OnInit, signal } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { TableLazyLoadEvent, TableModule } from 'primeng/table';
import { SkeletonModule } from 'primeng/skeleton';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';

import { Subject, debounceTime } from 'rxjs';

import { toSignal } from '@angular/core/rxjs-interop';
import { GetAllContactsRequest } from '../../models/get-all-contacts-request.model';
import { mapSortField, SortOrder } from '../../models/contact.enums';
import { Router, RouterLink } from '@angular/router';
import { Store } from '@ngrx/store';
import * as ContactActions from '../../store/contact.actions';
import * as ContactSelectors from '../../store/contact.selectors';
import { inject } from '@angular/core';
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

  private store = inject(Store);
  private router = inject(Router);

  contacts = toSignal(
    this.store.select(ContactSelectors.selectAllContacts),
    { initialValue: [] as Contact[] }
  );
  loading = toSignal(this.store.select(ContactSelectors.selectLoading), { initialValue: false });
  totalRecords = toSignal(this.store.select(ContactSelectors.selectTotalRecords), { initialValue: 0 });

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

        this.store.dispatch(
          ContactActions.loadContacts({ request: this.filters })
        );
      });

    this.store.dispatch(
      ContactActions.loadContacts({ request: this.filters })
    );
  }

  onFilter(
    field: 'firstName' | 'surname' | 'phoneNumber' | 'address',
    value: string
  ) {
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

    this.store.dispatch(
      ContactActions.loadContacts({ request: this.filters })
    );
  }

  openContact(contact: { id: string }) {
    this.router.navigate(['/contacts', contact.id]);
  }
  
  editContact(contact: { id: string }) {
    this.router.navigate(['/contacts', contact.id, 'edit']);
  }

  deleteContact(contact: { id: string }): void {
    console.log('Delete contact:', contact);

  }
}