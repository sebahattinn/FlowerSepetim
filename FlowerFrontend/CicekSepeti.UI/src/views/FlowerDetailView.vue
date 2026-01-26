<template>
  <div class="min-h-screen bg-secondary flex flex-col">
    
    <!-- NAV -->
    <nav class="flex justify-between items-center px-6 py-6 md:px-12">
      <router-link to="/" class="text-2xl font-serif text-primary font-bold tracking-widest">
        CICEKSEPETI
      </router-link>
      <router-link to="/" class="text-sm font-bold text-primary hover:underline">
        ← Geri Dön
      </router-link>
    </nav>

    <main class="flex-grow container mx-auto px-6 py-10">
      
      <!-- LOADING -->
      <div v-if="loading" class="flex justify-center items-center h-64">
        <span class="loading loading-spinner text-primary loading-lg"></span>
      </div>

      <!-- ERROR -->
      <div v-else-if="error" class="text-center text-red-500 py-20 font-serif text-xl">
        {{ error }}
        <router-link to="/" class="text-sm text-gray-500 underline mt-4 block">
          Anasayfaya Dön
        </router-link>
      </div>

      <!-- CONTENT -->
      <div v-else class="grid grid-cols-1 md:grid-cols-2 gap-12 lg:gap-20 items-center">
        
        <!-- IMAGE -->
        <div class="relative group">
          <div class="absolute -inset-4 border-2 border-primary/20 translate-x-4 translate-y-4 -z-10 rounded-sm"></div>
          <img 
            :src="flower.imageUrl || 'https://images.unsplash.com/photo-1562690868-60bbe7293e94?auto=format&fit=crop&q=80&w=1000'" 
            :alt="flower.name"
            class="w-full h-[500px] object-cover shadow-xl rounded-sm"
          />
        </div>

        <!-- INFO -->
        <div class="flex flex-col space-y-6">
          
          <div>
            <span class="text-xs font-bold tracking-[0.2em] text-gray-500 uppercase">
              Özel Tasarım
            </span>

            <h1 class="text-5xl font-serif text-primary mt-2 mb-2">
              {{ flower.name }}
            </h1>

            <!-- 💰 FİYAT -->
            <p class="text-2xl font-light text-accent">
              {{ flower.price }} ₺
            </p>

            <!-- 📦 STOK DURUMU -->
            <div class="mt-2">
              <span
                v-if="flower.stockQuantity <= 0"
                class="text-red-500 font-bold tracking-wider text-sm flex items-center gap-2"
              >
                <span class="w-2 h-2 bg-red-500 rounded-full animate-pulse"></span>
                TÜKENDİ
              </span>

              <span
                v-else-if="flower.stockQuantity < 5"
                class="text-orange-500 font-bold tracking-wider text-sm flex items-center gap-2"
              >
                <span class="w-2 h-2 bg-orange-500 rounded-full animate-bounce"></span>
                SON {{ flower.stockQuantity }} ÜRÜN!
              </span>

              <span
                v-else
                class="text-green-600 font-bold tracking-wider text-sm flex items-center gap-2"
              >
                <span class="w-2 h-2 bg-green-600 rounded-full"></span>
                STOKTA VAR
              </span>
            </div>
          </div>

          <!-- DESCRIPTION -->
          <div class="border-t border-b border-gray-300 py-6">
            <p class="text-gray-600 font-light leading-relaxed">
              {{ flower.description || 
              "Bu özel aranjman, mevsimin en taze çiçekleriyle uzman tasarımcılarımız tarafından özenle hazırlanmıştır." }}
            </p>
          </div>

          <!-- ACTION -->
          <div class="flex flex-col gap-4">
            
            <!-- 📲 WHATSAPP -->
            <a 
              :href="flower.stockQuantity > 0 ? whatsappLink : '#'" 
              target="_blank"
              class="btn text-white border-none rounded-none w-full font-serif tracking-widest 
                     flex items-center justify-center gap-2 h-14 text-lg transition-all duration-300"
              :class="flower.stockQuantity > 0 
                ? 'bg-[#25D366] hover:bg-[#128C7E]' 
                : 'bg-gray-400 cursor-not-allowed opacity-50 pointer-events-none'"
            >
              <svg
                v-if="flower.stockQuantity > 0"
                xmlns="http://www.w3.org/2000/svg"
                width="24"
                height="24"
                fill="currentColor"
                viewBox="0 0 16 16"
              >
                <path d="M13.601 2.326A7.854 7.854 0 0 0 8.001 0C3.697 0 .173 3.555.173 7.852c0 1.348.35 2.668 1.021 3.82L0 16l4.553-1.155c1.11.605 2.356.924 3.635.924 6.549 0 10.199-5.38 10.199-10.196 0-2.099-.818-4.046-2.298-5.247z"/>
              </svg>

              {{ flower.stockQuantity > 0
                ? 'WHATSAPP İLE SİPARİŞ VER'
                : 'ÜZGÜNÜZ, TÜKENDİ' }}
            </a>

            <p class="text-xs text-center text-gray-400">
              Siparişiniz için direkt tasarımcılarımızla görüşün.
            </p>
          </div>

        </div>
      </div>
    </main>
  </div>
</template>

<script setup>
import { ref, onMounted, computed } from 'vue';
import { useRoute } from 'vue-router';
import api from '../services/api';

const route = useRoute();
const flower = ref({});
const loading = ref(true);
const error = ref(null);


const PHONE_NUMBER = "905343058284";

const whatsappLink = computed(() => {
  if (!flower.value.name) return "#";
  const message = `Merhaba, ${flower.value.name} (Fiyat: ${flower.value.price}₺) hakkında bilgi almak ve sipariş vermek istiyorum.`;
  return `https://wa.me/${PHONE_NUMBER}?text=${encodeURIComponent(message)}`;
});

const fetchFlowerDetail = async () => {
  try {
    const response = await api.get(`/Flowers/${route.params.id}`);
    flower.value = response.data;
  } catch {
    error.value = "Ürün bilgileri alınamadı.";
  } finally {
    loading.value = false;
  }
};

onMounted(fetchFlowerDetail);
</script>