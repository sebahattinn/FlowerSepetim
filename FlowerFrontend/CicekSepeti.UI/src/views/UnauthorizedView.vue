<template>
  <ErrorPageLayout
    icon="🔐"
    title="Giriş Yapmanız Gerekiyor"
    titleColorClass="text-primary"
    iconBgClass="bg-gradient-to-br from-primary/20 to-accent/20"
    infoBorderClass="border-accent"
  >
    <template #description>
      Bu sayfaya erişebilmek için öncelikle hesabınıza <strong class="text-primary font-semibold">giriş yapmalısınız</strong>.
      <br />
      Hesabınız yoksa hemen kayıt olarak çiçek dünyamıza katılabilirsiniz.
    </template>

    <template #actions>
      <router-link
        to="/login"
        class="group relative inline-flex items-center justify-center px-8 py-3 text-white bg-primary hover:bg-primary-dark rounded-lg font-semibold tracking-wide transition-all duration-300 transform hover:scale-105 hover:shadow-lg overflow-hidden"
      >
        <span class="absolute inset-0 w-full h-full bg-accent opacity-0 group-hover:opacity-20 transition-opacity duration-300"></span>
        <span class="relative flex items-center gap-2">
          <svg class="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M11 16l-4-4m0 0l4-4m-4 4h14m-5 4v1a3 3 0 01-3 3H6a3 3 0 01-3-3V7a3 3 0 013-3h7a3 3 0 013 3v1" />
          </svg>
          GİRİŞ YAP
        </span>
      </router-link>

      <router-link
        to="/register"
        class="group inline-flex items-center justify-center px-8 py-3 text-primary bg-white border-2 border-primary hover:bg-primary hover:text-white rounded-lg font-semibold tracking-wide transition-all duration-300 transform hover:scale-105 hover:shadow-lg"
      >
        <span class="flex items-center gap-2">
          <svg class="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M18 9v3m0 0v3m0-3h3m-3 0h-3m-2-5a4 4 0 11-8 0 4 4 0 018 0zM3 20a6 6 0 0112 0v1H3v-1z" />
          </svg>
          KAYIT OL
        </span>
      </router-link>

      <router-link
        to="/"
        class="inline-flex items-center justify-center px-8 py-3 text-gray-700 bg-gray-100 hover:bg-gray-200 rounded-lg font-semibold tracking-wide transition-all duration-300"
      >
        <span class="flex items-center gap-2">
          <svg class="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M3 12l2-2m0 0l7-7 7 7M5 10v10a1 1 0 001 1h3m10-11l2 2m-2-2v10a1 1 0 01-1 1h-3m-6 0a1 1 0 001-1v-4a1 1 0 011-1h2a1 1 0 011 1v4a1 1 0 001 1m-6 0h6" />
          </svg>
          ANA SAYFA
        </span>
      </router-link>
    </template>

    <template #info>
      <h3 class="text-xl font-serif text-primary mb-4 font-bold">🌸 Üyelik Avantajları</h3>
      <ul class="text-left text-gray-600 space-y-3 max-w-md mx-auto">
        <li class="flex items-start gap-3">
          <span class="flex-shrink-0 w-6 h-6 rounded-full bg-primary/10 flex items-center justify-center text-primary text-sm font-bold">✓</span>
          <span>Özel indirimler ve kampanyalardan haberdar olun</span>
        </li>
        <li class="flex items-start gap-3">
          <span class="flex-shrink-0 w-6 h-6 rounded-full bg-primary/10 flex items-center justify-center text-primary text-sm font-bold">✓</span>
          <span>Sipariş geçmişinizi takip edin</span>
        </li>
        <li class="flex items-start gap-3">
          <span class="flex-shrink-0 w-6 h-6 rounded-full bg-primary/10 flex items-center justify-center text-primary text-sm font-bold">✓</span>
          <span>Hızlı ve güvenli alışveriş yapın</span>
        </li>
      </ul>
    </template>
  </ErrorPageLayout>
</template>

<script setup>
import ErrorPageLayout from '../components/ErrorPageLayout.vue'
</script>
