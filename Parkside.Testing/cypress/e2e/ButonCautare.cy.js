it('Testează căutarea după nume, prenume și campionat', () => {
    // Vizitează pagina
    cy.visit('http://127.0.0.1:5500/static/echipa/echipa.html');
    cy.get('body').should('exist');

    // Introducerea datelor de căutare
    cy.get('Nume').type('Dascălu');
    cy.get('Prenume').type('Codrin');
    cy.get('numeCampionat').type('NumeCampionat');

    // Apăsarea butonului de căutare
    cy.get('#cauta-button').click();

    // Asigură-te că rezultatele sunt afișate corect
    cy.get('.rezultate-căutare').should('exist');


    cy.get('.rezultate-căutare').should('contain', 'Dascălu Codrin');
});
