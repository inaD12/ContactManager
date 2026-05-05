describe('Contact List', () => {

  beforeEach(() => {

    cy.intercept('GET', '**/api/contacts*', {
      fixture: 'contacts.json'
    }).as('getContacts');

    cy.visit('/list');

    cy.wait('@getContacts');
  });

  it('loads contacts page', () => {

    cy.contains('Contacts').should('be.visible');

    cy.get('[data-cy=contact-row]')
      .should('have.length', 2);
  });

  it('opens contact details', () => {

    cy.get('[data-cy=contact-row]')
      .first()
      .click();

    cy.location('pathname')
      .should('include', '/contacts/');
  });

  it('deletes a contact', () => {

    cy.intercept('DELETE', '**/api/contacts/*', {
      statusCode: 200,
      body: {}
    }).as('deleteContact');

    cy.get('[data-cy=contact-row]')
      .should('have.length', 2);

    cy.get('[data-cy=contact-row]')
      .first()
      .within(() => {
        cy.get('button.p-button-danger')
          .click();
      });

    cy.contains('Confirm Delete').should('be.visible');

    cy.contains('button', /yes/i)
      .click();

    cy.wait('@deleteContact');

    cy.get('[data-cy=contact-row]')
      .should('have.length', 1);
  });

});