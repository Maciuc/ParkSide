describe('Testarea butonului de Inapoi sus', () => {
  it('Apasand pe butonul Inapoi sus, ne va duce spre header-ul homepage-ului.', () => {
    cy.visit('http://127.0.0.1:5500/acasa.html'); // URL-ul generat de Live Server, accesand homepage
    cy.get('body').should('be.visible'); // pagina prezinta body-ul cu care este intampinat utilizatorul

    cy.get('subsol-principal')
	.shadow()
	.find('div > div.button-inapoi-sus')
	.trigger('mouseover')
	.wait(900)
	.click({ force: true }); // Trebuie sa fortam click-ul, altfel nu ne va redirectiona catre sponsorul ales.
});
});