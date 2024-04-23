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
      },
      colors: {
        'main-background': '#BBDAF6',
        'main-text': '#384D6C',
        'link-active': '#CCE4FF',
        'link-accent': '#acc9e3'
      }
    }
  },
  plugins: []
};
