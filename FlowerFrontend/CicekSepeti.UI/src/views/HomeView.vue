<template>
  <div class="min-h-screen bg-gradient-to-br from-secondary via-white to-secondary/50 font-sans overflow-hidden">

    <TheNavbar />

    <header class="relative container mx-auto px-6 py-12 md:py-20">

      <div class="absolute inset-0 overflow-hidden pointer-events-none">
        <div class="absolute top-10 -right-32 w-96 h-96 bg-gradient-to-br from-accent/30 to-primary/20 rounded-full blur-3xl animate-blob"></div>
        <div class="absolute -bottom-20 -left-32 w-96 h-96 bg-gradient-to-tr from-primary/30 to-accent/20 rounded-full blur-3xl animate-blob animation-delay-2000"></div>
        <div class="absolute top-1/2 left-1/2 -translate-x-1/2 -translate-y-1/2 w-96 h-96 bg-gradient-to-br from-accent/20 to-primary/30 rounded-full blur-3xl animate-blob animation-delay-4000"></div>
      </div>

      <div class="relative z-10 max-w-5xl mx-auto">

        <div class="flex justify-center mb-6 animate-fade-in-down">
          <div class="inline-flex items-center gap-2 px-6 py-2 bg-white/80 backdrop-blur-md rounded-full border border-accent/20 shadow-lg">
            <span class="w-2 h-2 bg-accent rounded-full animate-pulse"></span>
            <span class="text-xs font-bold tracking-[0.3em] text-gray-600 uppercase">Doğadan Gelen Zarafet</span>
            <span class="text-accent">✨</span>
          </div>
        </div>

        <h1 class="text-center mb-6 animate-fade-in-up">
          <span class="block text-5xl md:text-7xl lg:text-8xl font-serif font-bold text-primary leading-none mb-4">
            Nehir
          </span>
          <span class="block text-6xl md:text-8xl lg:text-9xl font-serif font-bold italic bg-gradient-to-r from-accent via-accent-light to-accent bg-clip-text text-transparent leading-none">
            Çiçek Atölyesi
          </span>
        </h1>

        <p class="text-center text-gray-600 text-lg md:text-xl font-light max-w-2xl mx-auto mb-10 leading-relaxed animate-fade-in-up animation-delay-200">
          Sevdiklerinizi mutlu etmenin en zarif yolu. Özenle seçilmiş taze çiçekler,
          <span class="font-semibold text-primary">aynı gün</span> kapınıza gelsin.
        </p>

        <div class="flex flex-col sm:flex-row gap-4 justify-center items-center animate-fade-in-up animation-delay-400">

          <router-link
            to="/collection"
            class="group relative px-10 py-5 bg-gradient-to-r from-primary via-primary-dark to-primary hover:from-primary-dark hover:via-primary hover:to-primary-dark rounded-2xl text-white font-bold text-sm tracking-wider transition-all duration-500 transform hover:scale-105 hover:shadow-2xl shadow-xl overflow-hidden"
          >
            <div class="absolute inset-0 bg-gradient-to-r from-accent/0 via-accent/30 to-accent/0 opacity-0 group-hover:opacity-100 transition-opacity duration-500"></div>
            <span class="relative flex items-center gap-3">
              <svg class="w-6 h-6 transform group-hover:rotate-12 transition-transform duration-300" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M19 11H5m14 0a2 2 0 012 2v6a2 2 0 01-2 2H5a2 2 0 01-2-2v-6a2 2 0 012-2m14 0V9a2 2 0 00-2-2M5 11V9a2 2 0 012-2m0 0V5a2 2 0 012-2h6a2 2 0 012 2v2M7 7h10" />
              </svg>
              ALIŞVERİŞE BAŞLA
              <svg class="w-4 h-4 transform group-hover:translate-x-1 transition-transform duration-300" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 5l7 7-7 7" />
              </svg>
            </span>
          </router-link>

          <router-link
            to="/story"
            class="group px-10 py-5 bg-white/80 backdrop-blur-md hover:bg-white border-2 border-primary/20 hover:border-primary rounded-2xl text-primary font-bold text-sm tracking-wider transition-all duration-300 transform hover:scale-105 hover:shadow-xl shadow-md"
          >
            <span class="flex items-center gap-2">
              HİKAYEMİZİ KEŞFET
              <svg class="w-4 h-4 transform group-hover:translate-x-1 transition-transform duration-300" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M14 5l7 7m0 0l-7 7m7-7H3" />
              </svg>
            </span>
          </router-link>
        </div>

      </div>
    </header>

    <main class="container mx-auto px-6 pb-24">

      <div class="flex justify-between items-end mb-12 pb-6 border-b-2 border-gray-200">
        <div>
          <p class="text-xs font-bold tracking-[0.2em] text-gray-400 uppercase mb-2">Özel Seçim</p>
          <h2 class="text-3xl md:text-4xl font-serif font-bold text-primary">Yeni Gelenler</h2>
        </div>

        <router-link
          to="/collection"
          class="group flex items-center gap-2 text-sm font-bold text-gray-500 hover:text-primary transition uppercase tracking-wider"
        >
          Tümünü Gör
          <svg class="w-5 h-5 transform group-hover:translate-x-1 transition-transform duration-300" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M17 8l4 4m0 0l-4 4m4-4H3" />
          </svg>
        </router-link>
      </div>

      <div v-if="loading" class="flex justify-center py-20">
        <div class="relative">
          <div class="w-20 h-20 border-4 border-primary/20 border-t-primary rounded-full animate-spin"></div>
          <div class="absolute top-1/2 left-1/2 -translate-x-1/2 -translate-y-1/2 text-2xl">🌸</div>
        </div>
      </div>

      <div v-else-if="error" class="text-center py-20">
        <div class="inline-flex items-center gap-3 px-6 py-4 bg-error-light rounded-2xl text-error font-medium">
          <svg class="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 8v4m0 4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
          </svg>
          {{ error }}
        </div>
      </div>

      <div
        v-else
        class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-8 md:gap-10"
      >
        <ProductCard
          v-for="(flower, index) in flowers"
          :key="flower.id"
          :product="flower"
          :index="index"
        />
      </div>
    </main>

  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import api from '../services/api'
import ProductCard from '../components/ProductCard.vue'
import TheNavbar from '../components/TheNavbar.vue'

const flowers = ref([])
const loading = ref(true)
const error = ref(null)

const fetchFeaturedFlowers = async () => {
  try {
    const response = await api.get('/Flowers/featured')
    flowers.value = response.data
  } catch (err) {
    console.error(err)
    error.value = 'Vitrin ürünleri yüklenirken hata oluştu.'
  } finally {
    loading.value = false
  }
}

onMounted(fetchFeaturedFlowers)
</script>

<style scoped>
/* 🎨 Premium Animations */
@keyframes blob {
  0%, 100% {
    transform: translate(0, 0) scale(1);
  }
  33% {
    transform: translate(30px, -50px) scale(1.1);
  }
  66% {
    transform: translate(-20px, 20px) scale(0.9);
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

@keyframes fadeInDown {
  from {
    opacity: 0;
    transform: translateY(-30px);
  }
  to {
    opacity: 1;
    transform: translateY(0);
  }
}

.animate-blob {
  animation: blob 7s infinite;
}

.animation-delay-2000 {
  animation-delay: 2s;
}

.animation-delay-4000 {
  animation-delay: 4s;
}

.animate-fade-in-up {
  animation: fadeInUp 0.8s cubic-bezier(0.4, 0, 0.2, 1) forwards;
  opacity: 0;
}

.animate-fade-in-down {
  animation: fadeInDown 0.8s cubic-bezier(0.4, 0, 0.2, 1) forwards;
  opacity: 0;
}

.animation-delay-200 {
  animation-delay: 0.2s;
}

.animation-delay-400 {
  animation-delay: 0.4s;
}

.animation-delay-600 {
  animation-delay: 0.6s;
}
</style>