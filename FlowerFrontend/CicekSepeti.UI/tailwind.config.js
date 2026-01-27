/** @type {import('tailwindcss').Config} */
export default {
  content: [
    "./index.html",
    "./src/**/*.{vue,js,ts,jsx,tsx}",
  ],
  theme: {
    extend: {
      colors: {
        // Ana Renkler
        primary: {
          DEFAULT: '#1A4D2E',
          light: '#2a6d4e',
          dark: '#0f2e1b',
        },
        secondary: {
          DEFAULT: '#F9F7F2',
          dark: '#F0EDE3',
        },
        accent: {
          DEFAULT: '#D4AF37',
          light: '#e4c66f',
          dark: '#b8941f',
        },
        // Durum Renkleri
        success: {
          DEFAULT: '#10B981',
          light: '#D1FAE5',
        },
        warning: {
          DEFAULT: '#F59E0B',
          light: '#FEF3C7',
        },
        error: {
          DEFAULT: '#EF4444',
          light: '#FEE2E2',
        },
        info: {
          DEFAULT: '#3B82F6',
          light: '#DBEAFE',
        },
        surface: '#FFFFFF',
      },
      fontFamily: {
        sans: ['Lato', 'sans-serif'],
        serif: ['Playfair Display', 'serif'],
      },
      boxShadow: {
        'glass': '0 8px 32px 0 rgba(26, 77, 46, 0.12)',
        'glass-lg': '0 12px 40px 0 rgba(26, 77, 46, 0.16)',
      },
    },
  },
  plugins: [
    require('daisyui'),
  ],
  daisyui: {
    themes: ["light"], // Sadece aydınlık tema olsun
  },
}