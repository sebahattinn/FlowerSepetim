<template>
  <!-- 🧠 TIKLANABİLİR KART -->
  <div @click="goToDetail" class="group cursor-pointer">
    
    <div class="relative overflow-hidden aspect-[3/4] mb-4">
      <img 
        :src="product.imageUrl || 'https://images.unsplash.com/photo-1596073419667-9d77d59f033f?q=80&w=1000&auto=format&fit=crop'" 
        class="h-full w-full object-cover transition duration-500 group-hover:scale-105"
        alt="Çiçek"
      />

      <!-- 🛒 Sepete Ekle -->
      <button
        @click.stop
        class="absolute bottom-4 left-1/2 -translate-x-1/2 translate-y-10 opacity-0 
               group-hover:translate-y-0 group-hover:opacity-100 
               transition duration-300 bg-white text-primary px-6 py-2 
               font-serif text-sm uppercase tracking-widest shadow-lg 
               hover:bg-primary hover:text-white">
        Sepete Ekle
      </button>
    </div>

    <div class="text-center">
      <h3 class="font-serif text-xl text-gray-800 mb-1 group-hover:text-primary transition">
        {{ product.name }}
      </h3>

      <p class="text-xs text-gray-500 font-sans uppercase tracking-wider mb-2">
        {{ product.color }}
      </p>

      <p class="font-bold text-primary text-lg">
        {{ product.price }} ₺
      </p>
    </div>
  </div>
</template>

<script setup>
import { useRouter } from 'vue-router';

const props = defineProps({
  product: {
    type: Object,
    required: true
  }
});

const router = useRouter();

// 🌸 Detay Sayfasına Git
const goToDetail = () => {
  router.push(`/flower/${props.product.id}`);
};
</script>