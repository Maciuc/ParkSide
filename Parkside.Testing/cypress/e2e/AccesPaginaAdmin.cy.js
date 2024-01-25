// cypress/integration/accessTest.spec.js

describe('Test de acces la pagina de administrare  http://localhost:5174/', () => {
  it('Trebuie să poată accesa adresa', () => {
    // Vizitează adresa specificată
    cy.visit('http://localhost:5173/');
    // În acest exemplu, verificăm existența unui element cu clasa 'some-element'
    cy.get('.some-element').should('exist');
  });
});
