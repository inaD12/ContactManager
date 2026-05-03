import { ContactService } from '../../services/contact.service';

import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { DatePickerModule } from 'primeng/datepicker';
import { CardModule } from 'primeng/card';
import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CreateContactRequest } from '../../models/create-contract-request.model';
import { Store } from '@ngrx/store';
import { createContact } from '../../store/contact.actions';
import { toSignal } from '@angular/core/rxjs-interop';
import { selectCreateError, selectCreateSuccess, selectLoading } from '../../store/contact.selectors';

@Component({
  selector: 'app-contact-create',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    InputTextModule,
    ButtonModule,
    CardModule,
    DatePickerModule
  ],
  templateUrl: './contact-create.html',
  styleUrl: './contact-create.css',
})
export class ContactCreate implements OnInit {

  form!: FormGroup;

  private store = inject(Store);
  private fb = inject(FormBuilder);

  success = toSignal(this.store.select(selectCreateSuccess));
  error = toSignal(this.store.select(selectCreateError));
  loading = toSignal(this.store.select(selectLoading));

  ngOnInit() {
    this.form = this.fb.group({
      firstName: ['', Validators.required],
      surname: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
      address: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      iban: ['', Validators.required],
    });
  }

  submit() {
    if (this.form.invalid) return;

    const raw = this.form.value;

    const payload: CreateContactRequest = {
      firstName: raw.firstName,
      surname: raw.surname,
      dateOfBirth: this.toDateOnlyString(raw.dateOfBirth),
      address: raw.address,
      phoneNumber: raw.phoneNumber,
      iban: raw.iban
    };

    this.store.dispatch(
      createContact({ request: payload })
    );

    this.form.reset();
  }

  private toDateOnlyString(date: Date): string {
      return date.toISOString().split('T')[0];
    }
}