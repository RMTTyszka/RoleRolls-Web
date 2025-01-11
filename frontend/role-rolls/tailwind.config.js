/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [ "./src/**/*.{html,ts,css,scss}"],
  theme: {
    extend: {},
  },
  plugins: [require('tailwindcss-primeui')]
}

