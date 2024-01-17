describe('Redirectionarea catre stirile corespunzatoare.', () => {
  it('Apasand pe hyperlink, trebuie sa ne redirectioneze catre stirea corecta.', () => {
    cy.visit('http://127.0.0.1:5500/static/PaginaStiri/ArhivaStiri.html'); // URL-ul generat de Live Server, accesam direct arhiva de stiri

    cy.contains('a', 'Titlu Stire 1') // Incercam sa identificam un anchor ce contine text-ul: "Titlu Stire 1"
      .should('be.visible') // Ne asiguram ca butonul este vizibil
      .click(); // Apasam butonul

    cy.url().should('include', 'ExempluStire'); // Am fost redirectionati pe pagina corespunzătoare

    cy.contains('.containercontinut', 'Titlu stire lorem ipsum dolor sit amet'); // Titlul este corect
});
});