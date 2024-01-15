describe('Verificarea detaliilor despre jucatori', () => {
  it('Trebuie ca butonul Echipa sa deschida o noua pagina', () => {
    cy.visit('http://127.0.0.1:5500/acasa.html'); // URL-ul generat de Live Server

    // Accesam shadow dom-ul prin meniu-principal pentru a avea acces la butonul 'Echipa'
    cy.get('meniu-principal') 
      .shadow()
      .find('a[href="/static/echipa/echipa.html"]')
      .click();

    // URL-ul s-a schimbat, fiind echipa.html acum.
    cy.url().should('include', '/echipa/echipa.html');
});
	
    it('Player Profile Navigation', () => {
	cy.visit('http://127.0.0.1:5500/static/echipa/echipa.html');
    // Enable shadow DOM support
    Cypress.config('includeShadowDom', true);
	
	cy.get('div > div.stuff-container > grupare-rol.grupare-centrali').shadow()
	cy.get('a > div > div.top > div > div.img-container > OstafeRareș_c.png').shadow()
	cy.wait(1000)
	cy.get('info-card[imagine="/static/imagini/staff/OstafeRares_C.png"]')
      .shadow() // Accesare Shadow Root
	.find('.flip-image') // Imaginea e flipped
	.find('a.referinta-pagina-detalii[href*="personal/personal.html?id=6"]') // Selector
	.click();

    // Ne asiguram ca am ajuns pe link-ul corect
    cy.url().should('include', 'personal/personal.html?id=6');

  });
});