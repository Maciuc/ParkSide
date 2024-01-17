describe('Identificarea stirilor si aflarea mai multor detalii', () => {
  it('Butonul trebuie sa ne redirectioneze catre stirea completa', () => {
    cy.visit('http://127.0.0.1:5500/acasa.html'); // URL-ul generat de Live Server

    // Accesam shadow dom-ul prin meniu-principal pentru a avea acces la butonul Stiri
    cy.get('meniu-principal')
      .shadow()
      .find('a[href="/static/PaginaStiri/Stiri.html"]')
      .click();	

    cy.contains('a', 'Arhiva') // Incercam sa identificam un anchor ce contine text-ul: "Arhiva"
      .should('be.visible') // Ne asiguram ca butonul este vizibil
      .click(); // Apasam butonul

    cy.url().should('include', 'ArhivaStiri'); // Am fost redirectionati pe pagina corespunzătoare

    cy.contains('.titlupagina', 'Arhiva de știri'); // "Arhiva de stiri" a fost identificata
});
});