describe('Home Page Load', () => {
  it('Pagina se incarca cu succces.', () => {
    cy.visit('http://127.0.0.1:5500/acasa.html') // URL-ul furnizat de Live Server
    cy.get('body').should('be.visible'); // pagina prezinta body-ul cu care este intampinat utilizatorul
  });
});
