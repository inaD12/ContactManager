import { Component, inject, OnInit } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { take } from 'rxjs';

import * as ContactSelectors from '../../store/contact.selectors';
import * as ContactActions from '../../store/contact.actions';
import { CardModule } from 'primeng/card';
import { SkeletonModule } from 'primeng/skeleton';

@Component({
  selector: 'app-contact-edit',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    CardModule,
    SkeletonModule
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

  form!: FormGroup;

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
    this.form = this.fb.group({
      firstName: [c.firstName, Validators.required],
      surname: [c.surname, Validators.required],
      dateOfBirth: [new Date(c.dateOfBirth), Validators.required],
      address: [c.address, Validators.required],
      phoneNumber: [c.phoneNumber, Validators.required],
      iban: [c.iban, Validators.required]
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