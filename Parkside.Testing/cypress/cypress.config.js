const { defineConfig } = require('cypress')

module.exports = defineConfig({
  e2e: {
    baseUrl: 'http://127.0.0.1:5500/',
    includeShadowDom: true,
    supportFile: 'cypress/support/e2e.js',
    setupNodeEvents(on, config) {
    },
  },
})