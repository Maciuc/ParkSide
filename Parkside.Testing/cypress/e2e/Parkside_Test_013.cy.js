describe('Selectarea trofeelor dintr-un anumit an.', () => {
  it('Apasand pe butonul de Select, trebuie sa ni se afiseze trofeele castigate din anul selectat', () => {
    cy.visit('http://127.0.0.1:5500/static/istorie/istorie.html'); // URL-ul generat de Live Server, accesam direct Istorie

    cy.get('#yearDropdown')
       .select('2021')
       .should('have.value','2021')
});
});