//  Design System - Central Design Tokens
// Bu dosyayı tüm projeye import ederek tutarlı tasarım sağlıyoruz

export const colors = {
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

  // Nötr Renkler
  surface: '#FFFFFF',
  text: {
    primary: '#1F2937',
    secondary: '#6B7280',
    disabled: '#9CA3AF',
  },
}

export const gradients = {
  primary: 'linear-gradient(135deg, #1A4D2E 0%, #2a6d4e 100%)',
  accent: 'linear-gradient(135deg, #D4AF37 0%, #e4c66f 100%)',
  success: 'linear-gradient(135deg, #10B981 0%, #34D399 100%)',
  error: 'linear-gradient(135deg, #EF4444 0%, #F87171 100%)',
  warning: 'linear-gradient(135deg, #F59E0B 0%, #FBBF24 100%)',
}

export const shadows = {
  sm: '0 2px 8px rgba(26, 77, 46, 0.08)',
  md: '0 4px 16px rgba(26, 77, 46, 0.12)',
  lg: '0 8px 24px rgba(26, 77, 46, 0.16)',
  xl: '0 12px 32px rgba(26, 77, 46, 0.20)',
  glass: '0 8px 32px 0 rgba(26, 77, 46, 0.1)',
}

export const animations = {
  duration: {
    fast: '150ms',
    normal: '300ms',
    slow: '500ms',
  },
  easing: {
    smooth: 'cubic-bezier(0.4, 0, 0.2, 1)',
    bounce: 'cubic-bezier(0.68, -0.55, 0.265, 1.55)',
  },
}

export const spacing = {
  section: {
    mobile: '3rem',
    desktop: '5rem',
  },
  container: {
    mobile: '1.5rem',
    desktop: '3rem',
  },
}
