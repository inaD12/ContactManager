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

  createContact$ = createEffect(() =>
    this.actions$.pipe(
      ofType(ContactActions.createContact),
      switchMap(({ request }) =>
        this.contactService.create(request).pipe(
          map(response =>
            ContactActions.createContactSuccess({
                id: response.data.id,
                request
            })
          ),
          catchError(error =>
            of(ContactActions.createContactFailure({ error }))
          )
        )
      )
    )
  );

  loadContactById$ = createEffect(() =>
    this.actions$.pipe(
      ofType(ContactActions.loadContactById),
      switchMap(({ id }) =>
        this.contactService.getById(id).pipe(
          map(res =>
            ContactActions.loadContactByIdSuccess({
              contact: res.data
            })
          ),
          catchError(error =>
            of(ContactActions.loadContactByIdFailure({ error }))
          )
        )
      )
    )
  );

  updateContact$ = createEffect(() =>
    this.actions$.pipe(
      ofType(ContactActions.updateContact),
      switchMap(({ id, request }) =>
        this.contactService.update(id, request).pipe(
          map(() =>
            ContactActions.updateContactSuccess({ id, request })
          ),
          catchError(error =>
            of(ContactActions.updateContactFailure({ error }))
          )
        )
      )
    )
  );
}