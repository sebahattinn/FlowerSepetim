<template>
  <div class="min-h-screen bg-[#FDFBF7] font-sans">
    
    <TheNavbar />

    <header class="container mx-auto px-6 py-12 md:py-16 text-center">
      <p class="text-[10px] md:text-xs font-bold tracking-[0.3em] text-gray-400 uppercase mb-3 animate-fade-in-up">
        DOĞADAN GELEN ZARAFET
      </p>

      <h1 class="text-4xl md:text-6xl font-serif text-[#1B4D3E] mb-4 leading-tight animate-fade-in-up delay-100">
        Çiçeklerin <br class="md:hidden" /> 
        <span class="italic text-[#D4AF37]">Büyülü</span> Dünyası
      </h1>

      <p class="text-gray-500 font-light text-sm md:text-base max-w-lg mx-auto mb-8 leading-relaxed animate-fade-in-up delay-200">
        Sevdiklerinizi mutlu etmenin en zarif yolu. Özenle seçilmiş taze çiçekler kapınıza gelsin.
      </p>

      <router-link
        to="/collection"
        class="inline-block bg-[#6C63FF] hover:bg-[#5a52d5] text-white px-8 py-3 rounded-sm text-xs font-bold tracking-[0.2em] transition transform hover:-translate-y-1 shadow-lg animate-fade-in-up delay-300"
      >
        ALIŞVERİŞE BAŞLA
      </router-link>
    </header>

    <main class="container mx-auto px-6 pb-20">
      <div class="flex justify-between items-end mb-8 border-b border-gray-200 pb-4">
        <h2 class="text-2xl md:text-3xl font-serif text-[#1B4D3E]">Yeni Gelenler</h2>

        <router-link
          to="/collection"
          class="text-[10px] font-bold text-gray-400 hover:text-[#1B4D3E] transition uppercase tracking-widest"
        >
          Tümünü Gör →
        </router-link>
      </div>

      <div v-if="loading" class="flex justify-center py-20">
        <div class="animate-spin rounded-full h-10 w-10 border-t-2 border-b-2 border-[#1B4D3E]"></div>
      </div>

      <div v-else-if="error" class="text-center py-10">
        <p class="text-red-500 font-medium text-sm">{{ error }}</p>
      </div>

      <div
        v-else
        class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-x-8 gap-y-12"
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
import { ref, onMounted } from 'vue';
import api from '../services/api';
import ProductCard from '../components/ProductCard.vue'; // ✅ Component kullanımı harika!
import TheNavbar from '../components/TheNavbar.vue';

const flowers = ref([]);
const loading = ref(true);
const error = ref(null);

const fetchFeaturedFlowers = async () => {
  try {
    // 🛑 ESKİ YÖNTEM: Tüm çiçekleri çekip slice yapıyorduk.
    // const response = await api.get('/Flowers')
    // flowers.value = response.data.slice(0, 3)

    // ✅ YENİ YÖNTEM (OPTIMIZED): Sadece vitrin ürünlerini çekiyoruz.
    const response = await api.get('/Flowers/featured');
    flowers.value = response.data; // Slice yapmaya gerek yok, backend zaten filtreli gönderiyor.

  } catch (err) {
    console.error(err);
    error.value = 'Vitrin ürünleri yüklenirken hata oluştu.';
  } finally {
    loading.value = false;
  }
}

onMounted(fetchFeaturedFlowers);
</script>

<style>
/* ✨ Animasyonlar */
.animate-fade-in-up {
  animation: fadeInUp 0.8s ease-out forwards;
  opacity: 0;
  transform: translateY(20px);
}

.delay-100 { animation-delay: 0.1s; }
.delay-200 { animation-delay: 0.2s; }
.delay-300 { animation-delay: 0.3s; }

@keyframes fadeInUp {
  to {
    opacity: 1;
    transform: translateY(0);
  }
}
</style>