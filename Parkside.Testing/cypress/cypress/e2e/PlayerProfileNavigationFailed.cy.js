it('Player Profile Navigation', () => {
    cy.visit('http://127.0.0.1:5500/static/echipa/echipa.html');
  
    // Așteaptă până când elementul devine vizibil
    cy.get('div > div.stuff-container > grupare-rol.grupare-centrali')
      .should('be.visible')
      .shadow();
  
    // Așteaptă până când elementul imagine devine vizibil
    cy.get('a > div > div.top > div > div.img-container > OstafeRareș_c.png')
      .should('be.visible')
      .shadow()
      .click();
  
    // După ce faci clic, asigură-te că URL-ul a fost modificat
    cy.url().should('include', 'personal/personal.html?id=6');
  
    // Alte asigurări sau verificări necesare
    cy.get('h1').should('contain', 'Ostafe Rares');
    cy.go('back');
  });
  