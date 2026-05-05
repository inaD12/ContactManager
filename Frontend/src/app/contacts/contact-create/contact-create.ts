import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { DatePickerModule } from 'primeng/datepicker';
import { CardModule } from 'primeng/card';
import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, ValidatorFn} from '@angular/forms';
import { CreateContactRequest } from '../../models/create-contract-request.model';
import { MessageModule } from 'primeng/message';
import { createContactForm } from '../../utils/contact-form.factory';
import { ContactsFacade } from '../../utils/contacts.facade';

@Component({
  selector: 'app-contact-create',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    InputTextModule,
    ButtonModule,
    CardModule,
    DatePickerModule,
    MessageModule
  ],
  templateUrl: './contact-create.html',
  styleUrl: './contact-create.css',
})
export class ContactCreate {

  private fb = inject(FormBuilder);
  private facade = inject(ContactsFacade);

  success = this.facade.createSuccess;
  error = this.facade.createError;
  loading = this.facade.loading;

  form: FormGroup = createContactForm(this.fb);

  f(name: string) {
    return this.form.get(name);
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

    this.facade.createContact(payload);

    this.form.reset();
  }

  private toDateOnlyString(date: Date): string {
    return date.toISOString().split('T')[0];
  }
}