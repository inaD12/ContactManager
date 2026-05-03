import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { DatePickerModule } from 'primeng/datepicker';
import { CardModule } from 'primeng/card';
import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, ValidatorFn, Validators } from '@angular/forms';
import { CreateContactRequest } from '../../models/create-contract-request.model';
import { Store } from '@ngrx/store';
import { createContact } from '../../store/contact.actions';
import { toSignal } from '@angular/core/rxjs-interop';
import { selectCreateError, selectCreateSuccess, selectLoading } from '../../store/contact.selectors';
import { MessageModule } from 'primeng/message';

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
export class ContactCreate implements OnInit {

  form!: FormGroup;

  private store = inject(Store);
  private fb = inject(FormBuilder);

  success = toSignal(this.store.select(selectCreateSuccess));
  error = toSignal(this.store.select(selectCreateError));
  loading = toSignal(this.store.select(selectLoading));

  f(name: string) {
    return this.form.get(name);
  }

  ngOnInit() {
    this.form = this.fb.group({
      firstName: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]],
      surname: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]],
      dateOfBirth: ['', [Validators.required, this.pastDateValidator()]],
      address: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(250)]],
      phoneNumber: ['', [Validators.required, Validators.minLength(7), Validators.maxLength(15), Validators.pattern(/^\+?[0-9]{7,15}$/)]],
      iban: ['', [Validators.required, Validators.minLength(15), Validators.maxLength(34), Validators.pattern(/^[A-Z]{2}[0-9]{2}[A-Z0-9]{11,30}$/)]],
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
    
  private pastDateValidator(): ValidatorFn {
    return (control) => {
      if (!control.value) return null;
      const date = control.value instanceof Date ? control.value : new Date(control.value);
      return date < new Date() ? null : { pastDate: true };
    };
  }
}