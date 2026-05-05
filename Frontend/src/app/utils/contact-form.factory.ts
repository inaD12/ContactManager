import { FormBuilder, Validators, FormGroup } from '@angular/forms';

export function createContactForm(fb: FormBuilder): FormGroup {
  return fb.group({
    firstName: ['', [
      Validators.required,
      Validators.minLength(2),
      Validators.maxLength(50)
    ]],
    surname: ['', [
      Validators.required,
      Validators.minLength(2),
      Validators.maxLength(50)
    ]],
    dateOfBirth: ['', [
      Validators.required
    ]],
    address: ['', [
      Validators.required,
      Validators.minLength(5),
      Validators.maxLength(250)
    ]],
    phoneNumber: ['', [
      Validators.required,
      Validators.minLength(7),
      Validators.maxLength(15),
      Validators.pattern(/^\+?[0-9]{7,15}$/)
    ]],
    iban: ['', [
      Validators.required,
      Validators.minLength(15),
      Validators.maxLength(34),
      Validators.pattern(/^[A-Z]{2}[0-9]{2}[A-Z0-9]{11,30}$/)
    ]]
  });
}