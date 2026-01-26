/** @type {import('tailwindcss').Config} */
export default {
  content: [
    "./index.html",
    "./src/**/*.{vue,js,ts,jsx,tsx}",
  ],
  theme: {
    extend: {
      colors: {
        // Dribbble tasarımındaki renk paleti:
        primary: '#1A4D2E',   // Koyu Zümrüt Yeşili (Butonlar, Başlıklar)
        secondary: '#F9F7F2', // Çok Açık Krem (Arka Plan)
        accent: '#D4AF37',    // Altın Sarısı (Detaylar)
        surface: '#FFFFFF',   // Kartların içi Beyaz
      },
      fontFamily: {
        sans: ['Lato', 'sans-serif'],           // Düz yazılar
        serif: ['Playfair Display', 'serif'],   // Başlıklar (O havalı font)
      }
    },
  },
  plugins: [
    require('daisyui'),
  ],
  daisyui: {
    themes: ["light"], // Sadece aydınlık tema olsun
  },
}