<template>
  <nav class="w-full bg-secondary">
    <div class="container mx-auto px-6 py-6 flex justify-between items-center">
      
      <!-- LOGO -->
      <router-link
        to="/"
        class="text-2xl font-serif text-primary font-bold tracking-widest hover:opacity-80 transition"
      >
        Nehir Çiçekçilik
      </router-link>

      <!-- ORTA MENÜ -->
      <div class="hidden md:flex gap-8 font-sans text-xs font-bold tracking-widest text-gray-500 uppercase">
        <router-link to="/collection" active-class="text-primary" class="hover:text-primary transition">
          KOLEKSİYON
        </router-link>
        <router-link to="/story" active-class="text-primary" class="hover:text-primary transition">
          HİKAYEMİZ
        </router-link>
        <router-link to="/contact" active-class="text-primary" class="hover:text-primary transition">
          İLETİŞİM
        </router-link>
      </div>

      <!-- SAĞ MENÜ -->
      <div class="flex items-center gap-6 font-sans text-xs font-bold tracking-widest text-gray-500 uppercase">
        
        <!-- 🔐 ADMIN PANEL LINK -->
        <router-link
          v-if="authStore.isAuthenticated && authStore.userRole === 'Admin'"
          to="/admin"
          class="text-accent hover:text-primary transition border border-accent px-3 py-1"
        >
          YÖNETİM PANELİ
        </router-link>

        <!-- AUTH -->
        <template v-if="authStore.isAuthenticated">
          <button
            @click="handleLogout"
            class="text-red-500 hover:text-red-700 transition"
          >
            ÇIKIŞ
          </button>

          <span class="text-primary cursor-pointer hover:underline">
            SEPET (0)
          </span>
        </template>

        <template v-else>
          <router-link to="/login" class="hover:text-primary transition">
            GİRİŞ
          </router-link>
          <router-link to="/register" class="hover:text-primary transition">
            KAYIT
          </router-link>
        </template>

      </div>
    </div>
  </nav>
</template>

<script setup>
import { useAuthStore } from '../stores/auth'
import { useRouter } from 'vue-router'

const authStore = useAuthStore()
const router = useRouter()

const handleLogout = () => {
  authStore.logout()
  router.push('/login')
}
</script>