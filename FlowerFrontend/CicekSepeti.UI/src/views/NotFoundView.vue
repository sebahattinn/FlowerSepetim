<template>
  <div class="min-h-screen bg-gradient-to-br from-secondary via-white to-secondary font-sans flex flex-col relative overflow-hidden">

    <!-- Arka plan dekoratif elementler -->
    <div class="absolute inset-0 overflow-hidden pointer-events-none">
      <div class="absolute top-20 -right-20 w-96 h-96 bg-accent/10 rounded-full blur-3xl"></div>
      <div class="absolute -bottom-32 -left-32 w-96 h-96 bg-primary/10 rounded-full blur-3xl"></div>
    </div>

    <TheNavbar />

    <main class="flex-grow container mx-auto px-6 py-12 flex items-center justify-center relative z-10">
      <div class="w-full max-w-6xl">

        <div class="grid md:grid-cols-2 gap-8 items-start">

          <!-- Sol taraf - Hata mesajı -->
          <div class="bg-white/80 backdrop-blur-xl rounded-2xl p-8 md:p-12 shadow-glass border border-white/50">

            <!-- Icon -->
            <div class="inline-flex items-center justify-center w-24 h-24 rounded-full bg-gradient-to-br from-primary/20 to-accent/20 mb-6 animate-float">
              <span class="text-5xl">🌸</span>
            </div>

            <!-- Başlık -->
            <h1 class="text-4xl md:text-5xl font-serif font-bold text-primary mb-4 animate-fade-in-up">
              Sayfa Bulunamadı
            </h1>

            <!-- Alt başlık -->
            <p class="text-gray-600 text-lg font-light leading-relaxed mb-8 animate-fade-in-up animation-delay-100">
              Aradığınız sayfa taşınmış, silinmiş veya <strong class="text-primary font-semibold">hiç var olmamış</strong> olabilir.
              <br />
              Ama merak etmeyin, biz buradayız!
            </p>

            <!-- Butonlar -->
            <div class="flex flex-col gap-3 animate-fade-in-up animation-delay-200">
              <router-link
                to="/"
                class="group relative inline-flex items-center justify-center px-8 py-3 text-white bg-primary hover:bg-primary-dark rounded-lg font-semibold tracking-wide transition-all duration-300 transform hover:scale-105 hover:shadow-lg overflow-hidden"
              >
                <span class="absolute inset-0 w-full h-full bg-accent opacity-0 group-hover:opacity-20 transition-opacity duration-300"></span>
                <span class="relative flex items-center justify-center gap-2">
                  <svg class="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M3 12l2-2m0 0l7-7 7 7M5 10v10a1 1 0 001 1h3m10-11l2 2m-2-2v10a1 1 0 01-1 1h-3m-6 0a1 1 0 001-1v-4a1 1 0 011-1h2a1 1 0 011 1v4a1 1 0 001 1m-6 0h6" />
                  </svg>
                  ANA SAYFAYA DÖN
                </span>
              </router-link>

              <router-link
                to="/collection"
                class="group inline-flex items-center justify-center px-8 py-3 text-primary bg-white border-2 border-primary hover:bg-primary hover:text-white rounded-lg font-semibold tracking-wide transition-all duration-300 transform hover:scale-105 hover:shadow-lg"
              >
                <span class="flex items-center justify-center gap-2">
                  <svg class="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M19 11H5m14 0a2 2 0 012 2v6a2 2 0 01-2 2H5a2 2 0 01-2-2v-6a2 2 0 012-2m14 0V9a2 2 0 00-2-2M5 11V9a2 2 0 012-2m0 0V5a2 2 0 012-2h6a2 2 0 012 2v2M7 7h10" />
                  </svg>
                  ÇİÇEKLERE GÖZ AT
                </span>
              </router-link>
            </div>

          </div>

          <!-- Sağ taraf - İletişim formu -->
          <div class="bg-white/80 backdrop-blur-xl rounded-2xl p-8 md:p-12 shadow-glass border border-white/50 border-t-4 border-t-accent animate-fade-in-up animation-delay-300">

            <h3 class="text-2xl font-serif text-primary mb-6 font-bold flex items-center gap-2">
              <span>💬</span>
              <span>Bize Ulaşın</span>
            </h3>

            <form @submit.prevent="sendToWhatsapp" class="space-y-4">
              <div class="relative">
                <input
                  v-model="form.name"
                  required
                  type="text"
                  placeholder="İsim Soyisim"
                  class="w-full bg-gray-50/80 border border-gray-200 p-4 rounded-lg focus:outline-none focus:ring-2 focus:ring-primary/50 focus:border-primary transition-all"
                />
              </div>

              <div class="relative">
                <input
                  v-model="form.phone"
                  required
                  type="tel"
                  placeholder="Telefon"
                  class="w-full bg-gray-50/80 border border-gray-200 p-4 rounded-lg focus:outline-none focus:ring-2 focus:ring-primary/50 focus:border-primary transition-all"
                />
              </div>

              <div class="relative">
                <textarea
                  v-model="form.message"
                  required
                  rows="4"
                  placeholder="Mesajınız..."
                  class="w-full bg-gray-50/80 border border-gray-200 p-4 rounded-lg focus:outline-none focus:ring-2 focus:ring-primary/50 focus:border-primary transition-all resize-none"
                ></textarea>
              </div>

              <button
                type="submit"
                class="group relative w-full bg-[#25D366] hover:bg-[#1da851] text-white py-4 rounded-lg font-bold tracking-widest transition-all duration-300 transform hover:scale-105 hover:shadow-lg overflow-hidden"
              >
                <span class="relative flex items-center justify-center gap-2">
                  <svg class="w-5 h-5" fill="currentColor" viewBox="0 0 24 24">
                    <path d="M17.472 14.382c-.297-.149-1.758-.867-2.03-.967-.273-.099-.471-.148-.67.15-.197.297-.767.966-.94 1.164-.173.199-.347.223-.644.075-.297-.15-1.255-.463-2.39-1.475-.883-.788-1.48-1.761-1.653-2.059-.173-.297-.018-.458.13-.606.134-.133.298-.347.446-.52.149-.174.198-.298.298-.497.099-.198.05-.371-.025-.52-.075-.149-.669-1.612-.916-2.207-.242-.579-.487-.5-.669-.51-.173-.008-.371-.01-.57-.01-.198 0-.52.074-.792.372-.272.297-1.04 1.016-1.04 2.479 0 1.462 1.065 2.875 1.213 3.074.149.198 2.096 3.2 5.077 4.487.709.306 1.262.489 1.694.625.712.227 1.36.195 1.871.118.571-.085 1.758-.719 2.006-1.413.248-.694.248-1.289.173-1.413-.074-.124-.272-.198-.57-.347m-5.421 7.403h-.004a9.87 9.87 0 01-5.031-1.378l-.361-.214-3.741.982.998-3.648-.235-.374a9.86 9.86 0 01-1.51-5.26c.001-5.45 4.436-9.884 9.888-9.884 2.64 0 5.122 1.03 6.988 2.898a9.825 9.825 0 012.893 6.994c-.003 5.45-4.437 9.884-9.885 9.884m8.413-18.297A11.815 11.815 0 0012.05 0C5.495 0 .16 5.335.157 11.892c0 2.096.547 4.142 1.588 5.945L.057 24l6.305-1.654a11.882 11.882 0 005.683 1.448h.005c6.554 0 11.89-5.335 11.893-11.893a11.821 11.821 0 00-3.48-8.413z"/>
                  </svg>
                  WHATSAPP'TAN GÖNDER
                </span>
              </button>
            </form>

            <div class="mt-6 text-center text-sm text-gray-500">
              Anında cevap alın
            </div>

          </div>

        </div>

      </div>
    </main>
  </div>
</template>

<script setup>
import { reactive } from 'vue'
import TheNavbar from '../components/TheNavbar.vue'

const form = reactive({ name: '', phone: '', message: '' })

const sendToWhatsapp = () => {
  const myNumber = "905343058284"
  const text = `Merhaba (404 Sayfasından), ben ${form.name}.%0A%0ATelefon: ${form.phone}%0A%0AMesaj: ${form.message}`
  window.open(`https://wa.me/${myNumber}?text=${text}`, '_blank')
}
</script>

<style scoped>
/* 🎨 Animasyonlar */
@keyframes float {
  0%, 100% {
    transform: translateY(0px);
  }
  50% {
    transform: translateY(-10px);
  }
}

@keyframes fadeInUp {
  from {
    opacity: 0;
    transform: translateY(30px);
  }
  to {
    opacity: 1;
    transform: translateY(0);
  }
}

.animate-float {
  animation: float 3s ease-in-out infinite;
}

.animate-fade-in-up {
  animation: fadeInUp 0.6s cubic-bezier(0.4, 0, 0.2, 1) forwards;
  opacity: 0;
}

.animation-delay-100 {
  animation-delay: 0.1s;
}

.animation-delay-200 {
  animation-delay: 0.2s;
}

.animation-delay-300 {
  animation-delay: 0.3s;
}

/* Glassmorphism gölge */
.shadow-glass {
  box-shadow: 0 8px 32px 0 rgba(26, 77, 46, 0.12);
}
</style>
