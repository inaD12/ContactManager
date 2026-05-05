import { inject, Injectable } from "@angular/core";
import { createEffect, ofType, Actions } from "@ngrx/effects";
import { switchMap, map, catchError, of, withLatestFrom, filter, take } from "rxjs";
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
            catchError(error => {
              const message =
                error?.error?.message ??
                error?.message ??
                'An unknown error occurred';

              return of(
                ContactActions.loadContactsFailure({
                  error: message
                })
              );
            })
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
          catchError(error => {
            const message =
              error?.error?.message ??
              error?.message ??
              'An unknown error occurred';

            return of(
              ContactActions.createContactFailure({
                error: message
              })
            );
          })
        )
      )
    )
  );

  loadContactById$ = createEffect(() =>
    this.actions$.pipe(
      ofType(ContactActions.loadContactById),
      switchMap(action =>
        this.contactService.getById(action.id).pipe(
          map(res =>
            ContactActions.loadContactByIdSuccess({ contact: res.data })
          ),
          catchError(error => {
              const message =
                error?.error?.message ??
                error?.message ??
                'An unknown error occurred';

              return of(
                ContactActions.loadContactByIdFailure({
                  error: message
                })
              );
          })
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
          catchError(error => {
              const message =
                error?.error?.message ??
                error?.message ??
                'An unknown error occurred';

              return of(
                ContactActions.updateContactFailure({
                  error: message
                })
              );
          })
        )
      )
    )
  );

  deleteContact$ = createEffect(() =>
    this.actions$.pipe(
      ofType(ContactActions.deleteContact),
      switchMap(({ id }) =>
        this.contactService.delete(id).pipe(
          map(() =>
            ContactActions.deleteContactSuccess({ id })
          ),
          catchError(error => {
              const message =
                error?.error?.message ??
                error?.message ??
                'An unknown error occurred';

              return of(
                ContactActions.deleteContactFailure({
                  error: message
                })
              );
          })
        )
      )
    )
  );
}