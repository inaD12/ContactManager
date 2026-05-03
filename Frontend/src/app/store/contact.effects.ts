import { inject, Injectable } from "@angular/core";
import { createEffect, ofType, Actions } from "@ngrx/effects";
import { switchMap, map, catchError, of } from "rxjs";
import { ContactService } from "../services/contact.service";
import * as ContactActions from './contact.actions';

@Injectable()
export class ContactEffects {

  private actions$ = inject(Actions);
  private contactService = inject(ContactService);

  loadContacts$ = createEffect(() =>
    this.actions$.pipe(
        ofType(ContactActions.loadContacts),
        switchMap(({ request }) =>
        this.contactService.getAll(request).pipe(
            map(res =>
            ContactActions.loadContactsSuccess({
                contacts: res.data.items ?? [],
                totalRecords: res.data.totalCount ?? 0
            })
            ),
            catchError(error =>
            of(ContactActions.loadContactsFailure({ error }))
            )
        )
        )
    )
    );
}