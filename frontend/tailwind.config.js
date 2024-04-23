/** @type {import('tailwindcss').Config} */
export default {
  content: ['./index.html', './src/**/*.{js,ts,jsx,tsx}'],
  theme: {
    extend: {
      fontFamily: {
        comfortaa: ['"TT Firs Neue Trl"', 'sans-seris']
      },
      backgroundImage: {
        document: 'url("./src/assets/document.svg")'
      }
    }
  },
  plugins: []
};
