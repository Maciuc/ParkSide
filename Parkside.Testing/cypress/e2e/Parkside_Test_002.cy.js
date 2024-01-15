describe('Accesarea butonului Echipa', () => {
  it('Trebuie ca butonul Echipa sa deschida o noua pagina', () => {
    cy.visit('http://127.0.0.1:5500/acasa.html'); // URL-ul generat de Live Server

    // Accesam shadow dom-ul prin meniu-principal pentru a avea acces la butonul 'Echipa'
    cy.get('meniu-principal') // Replace with the correct selector for your shadow host element
      .shadow()
      .find('a[href="/static/echipa/echipa.html"]')
      .click();

    // URL-ul s-a schimbat, fiind echipa.html acum.
    cy.url().should('include', '/echipa/echipa.html');
  });
});