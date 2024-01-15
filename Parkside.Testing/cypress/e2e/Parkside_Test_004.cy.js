describe('Accesarea butonului Clasament', () => {
  it('Trebuie ca butonul Clasament sa deschida o noua pagina', () => {
    cy.visit('http://127.0.0.1:5500/acasa.html'); // URL-ul generat de Live Server

    // Accesam shadow dom-ul prin meniu-principal pentru a avea acces la butonul Clasament
    cy.get('meniu-principal')
      .shadow()
      .find('a[href="/static/clasament/clasament.html"]')
      .click();

    // URL-ul s-a schimbat, fiind clasament.html acum.
    cy.url().should('include', '/clasament/clasament.html');
  });
});