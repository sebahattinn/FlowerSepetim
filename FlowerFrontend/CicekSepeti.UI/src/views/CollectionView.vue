<template>
  <div class="min-h-screen bg-gradient-to-br from-secondary via-white to-secondary/50 flex flex-col overflow-hidden">

    <TheNavbar />

    <!-- 🎯 HEADER -->
    <header class="relative bg-white border-b border-gray-100">
      <div class="container mx-auto px-6 py-16 md:py-20 text-center">

        <!-- Animated Background Pattern -->
        <div class="absolute inset-0 overflow-hidden pointer-events-none opacity-10">
          <div class="absolute top-10 -right-20 w-64 h-64 bg-accent rounded-full blur-3xl"></div>
          <div class="absolute -bottom-20 -left-20 w-64 h-64 bg-primary rounded-full blur-3xl"></div>
        </div>

        <div class="relative z-10">
          <!-- Badge -->
          <div class="flex justify-center mb-4 animate-fade-in-down">
            <div class="inline-flex items-center gap-2 px-5 py-2 bg-primary/10 backdrop-blur-sm rounded-full border border-primary/20">
              <span class="text-xs font-bold tracking-[0.2em] text-primary uppercase">Tüm Tasarımlar</span>
              <span>🌸</span>
            </div>
          </div>

          <!-- Title -->
          <h1 class="text-5xl md:text-6xl font-serif font-bold text-primary mb-4 animate-fade-in-up">
            Koleksiyon
          </h1>

          <!-- Subtitle -->
          <p class="text-gray-600 text-lg max-w-2xl mx-auto animate-fade-in-up animation-delay-200">
            Her tarz ve bütçe için özel olarak seçilmiş çiçek tasarımlarımızı keşfedin
          </p>
        </div>

      </div>
    </header>

    <!-- 🌸 CONTENT -->
    <main class="container mx-auto px-6 py-12 md:py-16">

      <!-- LOADING -->
      <div v-if="loading" class="flex justify-center py-32">
        <div class="relative">
          <div class="w-24 h-24 border-4 border-primary/20 border-t-primary rounded-full animate-spin"></div>
          <div class="absolute top-1/2 left-1/2 -translate-x-1/2 -translate-y-1/2 text-3xl animate-pulse">🌸</div>
        </div>
      </div>

      <!-- ERROR -->
      <div v-else-if="error" class="text-center py-32">
        <div class="inline-flex items-center gap-3 px-8 py-5 bg-error-light rounded-2xl text-error font-medium text-lg">
          <svg class="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 8v4m0 4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
          </svg>
          {{ error }}
        </div>
      </div>

      <!-- PRODUCTS GRID -->
      <div v-else>

        <!-- Stats Bar -->
        <div class="flex justify-between items-center mb-8 pb-6 border-b-2 border-gray-200">
          <div>
            <p class="text-sm text-gray-500 mb-1">Toplam</p>
            <p class="text-2xl font-bold text-primary">{{ flowers.length }} Ürün</p>
          </div>

          <!-- View Options (Future: Grid/List toggle) -->
          <div class="hidden md:flex items-center gap-4">
            <div class="flex items-center gap-2 text-sm text-gray-500">
              <svg class="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M4 6h16M4 12h16M4 18h16" />
              </svg>
              <span>Grid View</span>
            </div>
          </div>
        </div>

        <!-- Products Grid -->
        <div class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-8 md:gap-10">
          <ProductCard
            v-for="(flower, index) in flowers"
            :key="flower.id"
            :product="flower"
            :index="index"
          />
        </div>

        <!-- Empty State -->
        <div v-if="flowers.length === 0" class="text-center py-32">
          <div class="text-6xl mb-4">🌸</div>
          <h3 class="text-2xl font-serif font-bold text-gray-800 mb-2">Henüz Ürün Yok</h3>
          <p class="text-gray-500">Çok yakında yeni çiçeklerimizi ekleyeceğiz!</p>
        </div>

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

onMounted(async () => {
  try {
    const response = await api.get('/Flowers')
    flowers.value = response.data
  } catch (err) {
    error.value = 'Koleksiyon yüklenirken bir hata oluştu.'
  } finally {
    loading.value = false
  }
})
</script>

<style scoped>
/* 🎨 Animations */
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
</style>
