import { Component, OnInit, signal } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { TableModule } from 'primeng/table';
import { SkeletonModule } from 'primeng/skeleton';
import { InputTextModule } from 'primeng/inputtext';

import { Subject, debounceTime } from 'rxjs';

import { Contact } from '../../models/contact.model';
import { ContactService } from '../../services/contact.service';
import { GetAllContactsRequest } from '../../models/get-all-contacts-request.model';
import { mapSortField, SortOrder } from '../../models/contact.enums';

@Component({
  selector: 'app-contact-list',
  standalone: true,
  imports: [
    CommonModule,
    TableModule,
    SkeletonModule,
    InputTextModule,
    DatePipe
  ],
  templateUrl: './contact-list.html',
})
export class ContactList implements OnInit {

  contacts = signal<Contact[]>([]);
  loading = signal(false);
  totalRecords = signal(0);

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

  private filterSubject = new Subject<void>();

  constructor(private service: ContactService) {}

  ngOnInit() {

    this.filterSubject
      .pipe(debounceTime(300))
      .subscribe(() => {
        this.filters.page = 1;
        this.loadData(this.filters);
      });

    this.loadData(this.filters);
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

  onDateFilter(
    field: 'minDateOfBirth' | 'maxDateOfBirth',
    value: string
  ) {
    this.filters = {
      ...this.filters,
      [field]: value
    };

    this.filterSubject.next();
  }

  loadContactsLazy(event: any) {

    this.filters = {
      ...this.filters,
      page: (event.first / event.rows) + 1,
      pageSize: event.rows
    };

    if (event.sortField) {
      const mapped = mapSortField(event.sortField);

      if (mapped !== null) {
        this.filters.sortBy = mapped;
        this.filters.sortOrder =
          event.sortOrder === 1 ? SortOrder.ASC : SortOrder.DESC;
      }
    }

    this.loadData(this.filters);
  }

  private loadData(request: GetAllContactsRequest) {
    this.loading.set(true);

    this.service.getAll(request).subscribe({
      next: (res) => {
        this.contacts.set(res.data.items ?? []);
        this.totalRecords.set(res.data.totalCount ?? 0);
        this.loading.set(false);
      },
      error: (err) => {
        if (err.status === 404) {
          this.contacts.set([]);
          this.totalRecords.set(0);
        } else {
          console.error(err);
        }
        this.loading.set(false);
      }
    });
  }
}