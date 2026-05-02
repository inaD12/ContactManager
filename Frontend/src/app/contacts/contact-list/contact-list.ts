import { Component, OnInit, signal } from '@angular/core';
import { Contact } from '../../models/contact.model';
import { ContactService } from '../../services/contact.service';
import { TableModule } from 'primeng/table';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-contact-list',
  standalone: true,
  imports: [TableModule, DatePipe],
  templateUrl: './contact-list.html',
  styleUrl: './contact-list.css',
})
export class ContactList implements OnInit {

  contacts = signal<Contact[]>([]);
  loading = signal(false);

  constructor(private service: ContactService) {}

  ngOnInit() {
    this.loadContacts();
  }

  loadContacts() {
    this.loading.set(true);

    this.service.getAll({}).subscribe({
      next: (res) => {
        this.contacts.set(res.data.items ?? []);
        this.loading.set(false);
      },
      error: (err) => {
        console.error(err);
        this.loading.set(false);
      }
    });
  }
}