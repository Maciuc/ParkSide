describe('Accesarea butonului Istorie', () => {
  it('Trebuie ca butonul Istorie sa deschida o noua pagina', () => {
    cy.visit('http://127.0.0.1:5500/acasa.html'); // URL-ul generat de Live Server

    // Accesam shadow dom-ul prin meniu-principal pentru a avea acces la butonul Istorie
    cy.get('meniu-principal')
      .shadow()
      .find('a[href="/static/istorie/istorie.html"]')
      .click();

    // URL-ul s-a schimbat, fiind Istorie.html acum.
    cy.url().should('include', 'static/istorie/istorie.html');
  });
});