// /** @type {import('tailwindcss').Config} */
// module.exports = {
//   content: [],
//   theme: {
//     extend: {},
//   },
//   plugins: [],
// }

module.exports = {
  content: [
    "./src/**/*.{js,jsx,ts,tsx}",
    "./index.html",
    "./node_modules/tailwind-datepicker-react/dist/**/*.js",
  ],
  theme: {
    extend: {
      // fontFamily: {
      //   Starjedi: ["Starjedi"],
      //   Varino: ["Varino"],
      //   HanSolo: ["HanSolo"],
      //   Typewriter: ["Typewriter"],
      //   Ananda: ["Ananda"]
      // },
      // opacity: {
      //   85: ".85",
      // },
      // letterSpacing: {
      //   wide: ".025em",
      //   wider: ".05em",
      //   widest: ".10em",
      // },
      backgroundImage: {
        'bg-image': "url('./public/assets/images/fond.jpg')",
        // 'auth': "url('./src/app/assets/images/general/011.jpg')",
      },
      colors: {
        // gris
        secondary: {
          light: "#d6d6d6",
          DEFAULT: "#aaaaaa",
          dark: "#777777",
        },
        // noir
        black: {
          light: "#262626",
          DEFAULT: "#11031D",
          dark: "#000000",
        }
      },
    },
  },
  plugins: [require("@tailwindcss/forms")],
};
