<template>
  <div class="min-h-screen flex items-center justify-center bg-secondary px-4">
    <div class="bg-white p-10 shadow-2xl w-full max-w-md border-t-4 border-primary">

      <!-- HEADER -->
      <div class="text-center mb-10">
        <h1 class="font-serif text-3xl text-primary font-bold mb-2">
          Hoş Geldiniz
        </h1>
        <p class="text-gray-500 text-sm font-sans tracking-wide">
          Lütfen hesabınıza giriş yapın
        </p>
      </div>

      <!-- FORM -->
      <form @submit.prevent="handleLogin" class="space-y-6">

        <!-- EMAIL -->
        <div class="form-control w-full">
          <label class="label">
            <span class="label-text text-gray-600 font-bold text-xs uppercase tracking-wider">
              Email Adresi
            </span>
          </label>
          <input 
            v-model="email"
            type="email"
            placeholder="ornek@email.com"
            class="input input-bordered w-full bg-secondary text-gray-900 placeholder-gray-400 focus:border-primary focus:outline-none rounded-none"
            required
          />
        </div>

        <!-- PASSWORD -->
        <div class="form-control w-full">
          <label class="label">
            <span class="label-text text-gray-600 font-bold text-xs uppercase tracking-wider">
              Şifre
            </span>
          </label>
          <input 
            v-model="password"
            type="password"
            placeholder="******"
            class="input input-bordered w-full bg-secondary text-gray-900 placeholder-gray-400 focus:border-primary focus:outline-none rounded-none"
            required
          />
        </div>

        <!-- REMEMBER ME + FORGOT -->
        <div class="flex items-center justify-between">
          <label class="flex items-center gap-2 cursor-pointer">
            <input 
              v-model="rememberMe"
              type="checkbox"
              class="checkbox checkbox-xs checkbox-primary rounded-none"
            />
            <span class="text-xs text-gray-600 font-bold uppercase tracking-wide">
              Beni Hatırla
            </span>
          </label>

          <a href="#" class="text-xs text-gray-400 hover:text-primary transition">
            Şifremi Unuttum?
          </a>
        </div>

        <!-- ERROR -->
        <div
          v-if="errorMessage"
          class="text-red-500 text-sm text-center font-bold bg-red-50 p-2 border border-red-100"
        >
          {{ errorMessage }}
        </div>

        <!-- BUTTON -->
        <button
          type="submit"
          class="btn w-full rounded-none font-serif tracking-widest bg-primary text-white hover:bg-green-900 border-none"
          :class="{ loading: isLoading }"
        >
          GİRİŞ YAP
        </button>

      </form>

      <!-- REGISTER -->
      <div class="text-center mt-6">
        <p class="text-xs text-gray-400">
          Hesabınız yok mu?
          <router-link to="/register" class="text-primary font-bold hover:underline">
            Kayıt Ol
          </router-link>
        </p>
      </div>

    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { useAuthStore } from '../stores/auth'

const authStore = useAuthStore()

// FORM STATE
const email = ref('')
const password = ref('')
const rememberMe = ref(false)

const isLoading = ref(false)
const errorMessage = ref('')

// 🔁 Sayfa açılınca kayıtlı email varsa doldur
onMounted(() => {
  const savedEmail = localStorage.getItem('rememberedEmail')
  if (savedEmail) {
    email.value = savedEmail
    rememberMe.value = true
  }
})

const handleLogin = async () => {
  isLoading.value = true
  errorMessage.value = ''

  try {
    await authStore.login(email.value, password.value)

    // 🧠 Beni Hatırla
    if (rememberMe.value) {
      localStorage.setItem('rememberedEmail', email.value)
    } else {
      localStorage.removeItem('rememberedEmail')
    }

  } catch (error) {
    if (error.response?.data) {
      errorMessage.value =
        typeof error.response.data === 'string'
          ? error.response.data
          : 'Giriş başarısız! Bilgilerinizi kontrol edin.'
    } else {
      errorMessage.value = 'Sunucuya bağlanılamadı!'
    }
  } finally {
    isLoading.value = false
  }
}
</script>