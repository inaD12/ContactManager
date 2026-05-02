export enum SortOrder {
  ASC = 1,
  DESC = 2,
}

export enum ContactSortField {
  Id = 0,
  FirstName = 1,
  Surname = 2,
  DateOfBirth = 3,
  Address = 4,
  PhoneNumber = 5,
}

export function mapSortField(field: string): ContactSortField | null {
  switch (field) {
    case 'id':
      return ContactSortField.Id;
    case 'firstName':
      return ContactSortField.FirstName;
    case 'surname':
      return ContactSortField.Surname;
    case 'dateOfBirth':
      return ContactSortField.DateOfBirth;
    case 'address':
      return ContactSortField.Address;
    case 'phoneNumber':
      return ContactSortField.PhoneNumber;
    default:
      return null;
  }
}