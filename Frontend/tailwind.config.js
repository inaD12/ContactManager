/** @type {import('tailwindcss').Config} */
module.exports = {
  darkMode: 'class',

  content: [
    "./src/**/*.{html,ts}",
    "./node_modules/primeng/**/*.{js,ts,html}"
  ],

  theme: {
    extend: {},
  },

  plugins: [
    require('tailwindcss-primeui')
  ],

  corePlugins: {
    preflight: false
  }
};