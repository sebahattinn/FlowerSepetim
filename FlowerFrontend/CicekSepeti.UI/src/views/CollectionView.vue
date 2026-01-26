<template>
  <div class="min-h-screen bg-secondary flex flex-col">
    
    <!-- 🔝 NAVBAR (Component) -->
    <TheNavbar />

    <!-- 🎯 HEADER -->
    <header class="text-center py-16 bg-white border-b border-gray-100">
      <p class="text-xs font-bold tracking-[0.2em] text-gray-400 uppercase mb-3">
        Tüm Tasarımlar
      </p>
      <h1 class="text-4xl md:text-5xl font-serif text-primary">
        Koleksiyon
      </h1>
    </header>

    <!-- 🌸 CONTENT -->
    <main class="container mx-auto px-6 py-12">
      
      <!-- LOADING -->
      <div v-if="loading" class="flex justify-center py-20">
        <span class="loading loading-spinner text-primary loading-lg"></span>
      </div>

      <!-- ERROR -->
      <div v-else-if="error" class="text-center text-red-500 py-10">
        {{ error }}
      </div>

      <!-- PRODUCTS -->
      <div
        v-else
        class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-x-8 gap-y-16"
      >
        <ProductCard
          v-for="flower in flowers"
          :key="flower.id"
          :product="flower"
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