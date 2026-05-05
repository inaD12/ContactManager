import { SortOrder, ContactSortField } from "./contact.enums";

export type GetAllContactsRequest = {
  firstName?: string;
  surname?: string;
  minDateOfBirth?: string;
  maxDateOfBirth?: string;
  address?: string;
  phoneNumber?: string;

  sortOrder?: SortOrder;
  sortBy?: ContactSortField;

  page?: number;
  pageSize?: number;
}