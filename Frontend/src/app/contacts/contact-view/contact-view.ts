import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CardModule } from 'primeng/card';
import { DatePipe } from '@angular/common';
import { SkeletonModule } from 'primeng/skeleton';
import { ContactsFacade } from '../../utils/contacts.facade';

@Component({
  selector: 'app-contact-view',
  imports: [
    CardModule,
    DatePipe,
    SkeletonModule
  ],
  templateUrl: './contact-view.html',
  styleUrl: './contact-view.css',
})
export class ContactView implements OnInit {

  private route = inject(ActivatedRoute);
  private facade = inject(ContactsFacade);

  loading = this.facade.loading;
  contact = this.facade.contact;

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id')!;
    this.facade.loadContactIfMissing(id);
  }
}