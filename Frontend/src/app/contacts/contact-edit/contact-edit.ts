import { Component, inject, OnInit } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { FormBuilder, FormGroup,ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { take } from 'rxjs';

import * as ContactSelectors from '../../store/contact.selectors';
import * as ContactActions from '../../store/contact.actions';
import { CardModule } from 'primeng/card';
import { SkeletonModule } from 'primeng/skeleton';
import { DatePickerModule } from 'primeng/datepicker';

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

  private store = inject(Store);
  private route = inject(ActivatedRoute);
  private fb = inject(FormBuilder);

  router = inject(Router);
  loading = toSignal(this.store.select(ContactSelectors.selectLoading));

  form: FormGroup = this.fb.group({
    firstName: [''],
    surname: [''],
    dateOfBirth: [''],
    address: [''],
    phoneNumber: [''],
    iban: ['']
  });

  contact = toSignal(
    this.store.select(ContactSelectors.selectSelectedContact),
    { initialValue: null }
  );

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id')!;

    this.store.select(ContactSelectors.selectContactByIdFromList(id))
      .pipe(take(1))
      .subscribe(existing => {

        if (existing) {
          this.store.dispatch(
            ContactActions.setSelectedContact({ contact: existing })
          );
          this.buildForm(existing);
        } else {
          this.store.dispatch(ContactActions.loadContactById({ id }));
        }

      });

    const contactSignal = this.contact;

    const interval = setInterval(() => {
      const c = contactSignal();
      if (c && !this.form) {
        this.buildForm(c);
        clearInterval(interval);
      }
    }, 50);
  }

  private buildForm(c: any) {
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

    const id = this.route.snapshot.paramMap.get('id')!;

    this.store.dispatch(
      ContactActions.updateContact({
        id,
        request: {
          newAddress: this.form.value.address,
          newPhoneNumber: this.form.value.phoneNumber,
          newIBAN: this.form.value.iban
        }
      })
    );

    this.router.navigate(['/contacts', id]);
  }

  private toDateOnlyString(date: Date): string {
    return date.toISOString().split('T')[0];
  }
}