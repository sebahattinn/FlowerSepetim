<template>
  <div class="min-h-screen flex items-center justify-center bg-secondary px-4">
    
    <div class="bg-white p-10 shadow-2xl w-full max-w-md border-t-4 border-primary">
      
      <div class="text-center mb-8">
        <h1 class="font-serif text-3xl text-primary font-bold mb-2">
          Aramıza Katılın
        </h1>
        <p class="text-gray-500 text-sm font-sans tracking-wide">
          Çiçeklerin büyülü dünyasına adım atın
        </p>
      </div>

      <form @submit.prevent="handleRegister" class="space-y-4">
        
        <div class="flex gap-4">
          <div class="form-control w-full">
            <label class="label">
              <span class="label-text text-gray-600 font-bold text-xs uppercase">
                Ad
              </span>
            </label>
            <input
              v-model="formData.firstName"
              type="text"
              placeholder="Adınız"
              class="input input-bordered w-full bg-secondary text-gray-900 placeholder-gray-400 focus:border-primary focus:outline-none rounded-none"
              required
            />
          </div>

          <div class="form-control w-full">
            <label class="label">
              <span class="label-text text-gray-600 font-bold text-xs uppercase">
                Soyad
              </span>
            </label>
            <input
              v-model="formData.lastName"
              type="text"
              placeholder="Soyadınız"
              class="input input-bordered w-full bg-secondary text-gray-900 placeholder-gray-400 focus:border-primary focus:outline-none rounded-none"
              required
            />
          </div>
        </div>

        <div class="form-control w-full">
          <label class="label">
            <span class="label-text text-gray-600 font-bold text-xs uppercase">
              Email
            </span>
          </label>
          <input
            v-model="formData.email"
            type="email"
            placeholder="ornek@email.com"
            class="input input-bordered w-full bg-secondary text-gray-900 placeholder-gray-400 focus:border-primary focus:outline-none rounded-none"
            required
          />
        </div>

        <div class="form-control w-full">
          <label class="label">
            <span class="label-text text-gray-600 font-bold text-xs uppercase">
              Şifre
            </span>
          </label>
          <input
            v-model="formData.password"
            type="password"
            placeholder="******"
            class="input input-bordered w-full bg-secondary text-gray-900 placeholder-gray-400 focus:border-primary focus:outline-none rounded-none"
            required
          />
        </div>

        <div
          v-if="message"
          :class="isError ? 'text-red-500' : 'text-green-600'"
          class="text-sm text-center font-bold"
        >
          {{ message }}
        </div>

        <!-- 🔥 ENTEGRE EDİLEN BUTON -->
        <button 
          type="submit" 
          class="btn w-full rounded-none font-serif tracking-widest bg-primary text-white hover:bg-green-900 border-none"
          :class="{ loading: isLoading }"
        >
          KAYIT OL
        </button>

      </form>
      
      <div class="text-center mt-6">
        <p class="text-xs text-gray-400">
          Zaten hesabınız var mı?
          <router-link
            to="/login"
            class="text-primary font-bold hover:underline"
          >
            Giriş Yap
          </router-link>
        </p>
      </div>

    </div>
  </div>
</template>

<script setup>
import { ref, reactive } from 'vue'
import { useAuthStore } from '../stores/auth'
import { useRouter } from 'vue-router'

const authStore = useAuthStore()
const router = useRouter()

const formData = reactive({
  firstName: '',
  lastName: '',
  email: '',
  password: ''
})

const isLoading = ref(false)
const message = ref('')
const isError = ref(false)

const handleRegister = async () => {
  isLoading.value = true
  message.value = ''
  isError.value = false

  try {
    await authStore.register(formData)
    message.value = 'Kayıt başarılı! Giriş sayfasına yönlendiriliyorsunuz...'

    setTimeout(() => {
      router.push('/login')
    }, 2000)
  } catch (error) {
    isError.value = true
    message.value = 'Kayıt başarısız. Lütfen bilgileri kontrol edin.'
  } finally {
    isLoading.value = false
  }
}
</script>