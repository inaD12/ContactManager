import { Component, inject } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { ActivatedRoute } from '@angular/router';
import { Store } from '@ngrx/store';
import * as ContactSelectors from '../../store/contact.selectors';
import * as ContactActions from '../../store/contact.actions';
import { take } from 'rxjs';
import { CardModule } from 'primeng/card';
import { DatePipe } from '@angular/common';
import { SkeletonModule } from 'primeng/skeleton';

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
export class ContactView {

  private store = inject(Store);
  private route = inject(ActivatedRoute);

  loading = toSignal(this.store.select(ContactSelectors.selectLoading));
  contact = toSignal(
    this.store.select(ContactSelectors.selectSelectedContact),
    { initialValue: null }
  );

  constructor() {
    const id = this.route.snapshot.paramMap.get('id')!;

    this.store.select(ContactSelectors.selectContactByIdFromList(id))
      .pipe(take(1))
      .subscribe(existing => {

        if (existing) {
          this.store.dispatch(
            ContactActions.setSelectedContact({ contact: existing })
          );
        } else {
          this.store.dispatch(
            ContactActions.loadContactById({ id })
          );
        }

      });
  }
}