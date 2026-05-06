import { Component, effect, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup,ReactiveFormsModule, ValidatorFn} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { CardModule } from 'primeng/card';
import { SkeletonModule } from 'primeng/skeleton';
import { DatePickerModule } from 'primeng/datepicker';
import { Contact } from '../../models/contact.model';
import { createContactForm } from '../../utils/contact-form.factory';
import { ContactsFacade } from '../../utils/contacts.facade';

@Component({
  selector: 'app-contact-edit',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    CardModule,
    SkeletonModule,
    DatePickerModule
  ],
  templateUrl: './contact-edit.html',
  styleUrl: './contact-edit.css',
})
export class ContactEdit implements OnInit {

  private route = inject(ActivatedRoute);
  private fb = inject(FormBuilder);
  private facade = inject(ContactsFacade);
  router = inject(Router);

  loading = this.facade.loading;
  contact = this.facade.contact;

  private contactId = this.route.snapshot.paramMap.get('id')!;

  form: FormGroup = createContactForm(this.fb);

  constructor() {
    effect(() => {
      const c = this.contact();
      if (c) this.buildForm(c);
    });
  }

  ngOnInit() {
    this.facade.loadContactIfMissing(this.contactId);
  }

  f(name: string) {
    return this.form.get(name);
  }

  private buildForm(c: Contact) {
    this.form.patchValue({
      firstName: c.firstName,
      surname: c.surname,
      dateOfBirth: new Date(c.dateOfBirth),
      address: c.address,
      phoneNumber: c.phoneNumber,
      iban: c.iban
    });
  }

  submit() {
    if (this.form.invalid) return;

    this.facade.updateContact(this.contactId, {
      newFirstName: this.form.value.firstName,
      newSurname: this.form.value.surname,
      newAddress: this.form.value.address,
      newPhoneNumber: this.form.value.phoneNumber,
      newIBAN: this.form.value.iban
    });

    this.router.navigate(['/contacts', this.contactId]);
  }
}