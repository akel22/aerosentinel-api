/** @type {import('tailwindcss').Config} */
export default {
  content: ["./index.html", "./src/**/*.{ts,tsx}"],
  theme: {
    extend: {
      colors: {
        brand: {
          black: "#181818",
          white: "#FFFFFF",
          gold: "#DEC154",
        },
      },
      fontFamily: {
        sans: [
          "Inter",
          "ui-sans-serif",
          "system-ui",
          "-apple-system",
          "BlinkMacSystemFont",
          "Segoe UI",
          "sans-serif",
        ],
      },
      boxShadow: {
        panel: "0 1px 2px rgba(24, 24, 24, 0.06), 0 10px 30px rgba(24, 24, 24, 0.04)",
      },
    },
  },
};
