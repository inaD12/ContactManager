import { ContactService } from '../../services/contact.service';

import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { DatePickerModule } from 'primeng/datepicker';
import { CardModule } from 'primeng/card';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CreateContactRequest } from '../../models/create-contract-request.model';

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

  constructor(
    private fb: FormBuilder,
    private service: ContactService
  ) {}

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

    this.service.create(payload).subscribe({
      next: () => {
        console.log('Contact created');
        this.form.reset();
      },
      error: err => console.error(err)
    });
  }
  private toDateOnlyString(date: Date): string {
      return date.toISOString().split('T')[0];
    }
}