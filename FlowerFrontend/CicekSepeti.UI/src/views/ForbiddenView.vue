<template>
  <ErrorPageLayout
    icon="⛔"
    title="Yetkiniz Bulunmuyor"
    titleColorClass="text-error"
    iconBgClass="bg-gradient-to-br from-error/20 to-warning/20"
    infoBorderClass="border-error"
  >
    <template #description>
      Bu sayfaya erişim <strong class="text-error font-semibold">sadece yönetici yetkisine</strong> sahip kullanıcılar tarafından yapılabilir.
      <br />
      Hesabınız bu işlemi gerçekleştirmek için gerekli yetkilere sahip değil.
    </template>

    <template #actions>
      <router-link
        to="/"
        class="group relative inline-flex items-center justify-center px-8 py-3 text-white bg-primary hover:bg-primary-dark rounded-lg font-semibold tracking-wide transition-all duration-300 transform hover:scale-105 hover:shadow-lg overflow-hidden"
      >
        <span class="absolute inset-0 w-full h-full bg-accent opacity-0 group-hover:opacity-20 transition-opacity duration-300"></span>
        <span class="relative flex items-center gap-2">
          <svg class="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M3 12l2-2m0 0l7-7 7 7M5 10v10a1 1 0 001 1h3m10-11l2 2m-2-2v10a1 1 0 01-1 1h-3m-6 0a1 1 0 001-1v-4a1 1 0 011-1h2a1 1 0 011 1v4a1 1 0 001 1m-6 0h6" />
          </svg>
          ANA SAYFAYA DÖN
        </span>
      </router-link>

      <router-link
        to="/contact"
        class="group inline-flex items-center justify-center px-8 py-3 text-primary bg-white border-2 border-primary hover:bg-primary hover:text-white rounded-lg font-semibold tracking-wide transition-all duration-300 transform hover:scale-105 hover:shadow-lg"
      >
        <span class="flex items-center gap-2">
          <svg class="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M3 8l7.89 5.26a2 2 0 002.22 0L21 8M5 19h14a2 2 0 002-2V7a2 2 0 00-2-2H5a2 2 0 00-2 2v10a2 2 0 002 2z" />
          </svg>
          İLETİŞİME GEÇ
        </span>
      </router-link>
    </template>

    <template #info>
      <h3 class="text-xl font-serif text-error mb-4 font-bold">🔒 Neden Bu Mesajı Görüyorsunuz?</h3>
      <ul class="text-left text-gray-600 space-y-3 max-w-md mx-auto">
        <li class="flex items-start gap-3">
          <span class="flex-shrink-0 w-6 h-6 rounded-full bg-error/10 flex items-center justify-center text-error text-sm font-bold">•</span>
          <span>Bu sayfa sadece <strong>yönetici yetkisine</strong> sahip kullanıcılar tarafından görüntülenebilir</span>
        </li>
        <li class="flex items-start gap-3">
          <span class="flex-shrink-0 w-6 h-6 rounded-full bg-error/10 flex items-center justify-center text-error text-sm font-bold">•</span>
          <span>Hesabınız bu işlemi yapmak için gerekli izinlere sahip değil</span>
        </li>
        <li class="flex items-start gap-3">
          <span class="flex-shrink-0 w-6 h-6 rounded-full bg-error/10 flex items-center justify-center text-error text-sm font-bold">•</span>
          <span>Erişim gerekiyorsa, lütfen sistem yöneticinizden yetki talebinde bulunun</span>
        </li>
      </ul>
    </template>
  </ErrorPageLayout>
</template>

<script setup>
import ErrorPageLayout from '../components/ErrorPageLayout.vue'
</script>
