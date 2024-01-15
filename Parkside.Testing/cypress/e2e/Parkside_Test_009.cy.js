describe('Verificarea link-urilor catre sponsori', () => {
  it('Homepage se incarca cu succes, iar link-ul catre sponsor functioneaza.', () => {
    cy.visit('http://127.0.0.1:5500/acasa.html') // URL-ul furnizat de Live Server
    cy.get('body').should('be.visible'); // pagina prezinta body-ul cu care este intampinat utilizatorul
	cy.get('subsol-principal')
	.shadow()
	.find('div > div:nth-child(2) > div.container-sponsori > a:nth-child(1) > img')
	.trigger('mouseover')
	.wait(900)
	.click({ force: true }); // Trebuie sa fortam click-ul, altfel nu ne va redirectiona catre sponsorul ales.
  });
});
