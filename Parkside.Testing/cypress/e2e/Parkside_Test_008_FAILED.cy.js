describe('Verificarea detaliilor despre jucatori', () => {
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
	
    it('Player Profile Navigation', () => {
	cy.visit('http://127.0.0.1:5500/static/echipa/echipa.html');
    // Enable shadow DOM support
    Cypress.config('includeShadowDom', true);
	
	cy.get('div > div.stuff-container > grupare-rol.grupare-centrali').shadow()
	cy.get('a > div > div.top > div > div.img-container > OstafeRareș_c.png').shadow()
	cy.wait(1000)
	cy.get('info-card[imagine="/static/imagini/staff/OstafeRares_C.png"]')
      .shadow() // Access the shadow root if needed
	.find('.flip-image') // We assume that the flip has completed and this container is now visible
	.find('a.referinta-pagina-detalii[href*="personal/personal.html?id=6"]') // The actual selector for the link after the card flip
	.click();

    // After clicking, you may want to assert that the URL has changed
    cy.url().should('include', 'personal/personal.html?id=6');


    // Optionally, check that Rares's profile content is visible
    // cy.contains('h1', 'Ostafe Rares').should('be.visible');
  });
});