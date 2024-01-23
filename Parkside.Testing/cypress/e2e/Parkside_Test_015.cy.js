describe('Testarea Log in-ului', () => {
  it('Inserand datele de log in, trebuie sa fim autentificati pe website.', () => {
    cy.visit('http://localhost:5500/static/logare/signup.html'); // URL-ul generat de Live Server, accesand log in-ul direct

    cy.get('body > div > div.log > form > div:nth-child(2) > input').type('razvan.alexuc@student.usv.ro') // inseram text in campul de Email
    cy.get('body > div > div.log > form > div:nth-child(3) > input').type('usvtest2024') // inseram text in campul de Parola
    cy.get('body > div > div.log > form > input[type=checkbox]').check().should('be.checked') // bifam sa ramanem autentificati
    cy.get('body > div > div.log > form > button').click(); // butonul Log In este apasat

});
});