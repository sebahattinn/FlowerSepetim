<template>
  <!-- Kar Yağışı Efekti -->
  <SnowEffect />

  <!-- Sevgililer Günü Teması -->
  <ValentineTheme />

  <!-- Sayfa içeriği -->
  <RouterView />

  <!-- Footer (route.meta.hideFooter === true ise gizlenir) -->
  <TheFooter v-if="showFooter" />
</template>

<script setup>
import { onMounted, onUnmounted, computed } from 'vue'
import { RouterView, useRouter, useRoute } from 'vue-router'
import { useAuthStore } from './stores/auth'
import TheFooter from './components/TheFooter.vue'
import SnowEffect from './components/SnowEffect.vue'
import ValentineTheme from './components/ValentineTheme.vue'

const authStore = useAuthStore()
const router = useRouter()
const route = useRoute()

// 🧠 Footer görünsün mü?
// meta.hideFooter === true → footer gizlenir
const showFooter = computed(() => !route.meta.hideFooter)

// 🧠 SENIOR LOGIC: Multi-Tab Logout Sync
const syncLogout = (event) => {
  if (event.key === 'token' && event.newValue === null) {
    console.warn('Başka bir sekmede çıkış yapıldı. Bu sekme de kapatılıyor...')

    authStore.token = null
    authStore.user = null

    router.push('/login')
  }
}

// 🔥 APP BOOTSTRAP
onMounted(() => {
  // Multi-tab logout listener
  window.addEventListener('storage', syncLogout)

  // Sayfa yenilendiyse → token varsa user state’i geri yükle
  if (authStore.token) {
    authStore.decodeAndSetUser()
  }
})

// 🧹 CLEANUP
onUnmounted(() => {
  window.removeEventListener('storage', syncLogout)
})
</script>