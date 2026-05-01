import { environment } from '../../environments/environment';

export const ENDPOINTS = {
  contacts: {
    root: `${environment.apiUrl}/api/contacts`,
    byId: (id: string) => `${environment.apiUrl}/api/contacts/${id}`,
  }
};