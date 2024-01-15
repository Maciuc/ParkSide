describe('Accesarea butonului Stiri', () => {
  it('Trebuie ca butonul Stiri sa deschida o noua pagina', () => {
    cy.visit('http://127.0.0.1:5500/acasa.html'); // URL-ul generat de Live Server

    // Accesam shadow dom-ul prin meniu-principal pentru a avea acces la butonul Stiri
    cy.get('meniu-principal')
      .shadow()
      .find('a[href="/static/PaginaStiri/Stiri.html"]')
      .click();

    // URL-ul s-a schimbat, fiind Stiri.html acum.
    cy.url().should('include', '/PaginaStiri/Stiri.html');
  });
});