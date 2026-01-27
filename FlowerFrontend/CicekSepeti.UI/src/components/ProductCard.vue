<template>
  <div
    @click="goToDetail"
    class="group relative cursor-pointer animate-fade-in-scale"
    :style="{ animationDelay: `${index * 100}ms` }"
  >

    <div class="relative bg-white rounded-3xl overflow-hidden shadow-lg hover:shadow-2xl transition-all duration-500 transform hover:-translate-y-2">

      <div class="relative aspect-[3/4] overflow-hidden bg-gradient-to-br from-gray-50 to-gray-100">

        <img
          :src="product.imageUrl || 'https://images.unsplash.com/photo-1596073419667-9d77d59f033f?q=80&w=1000&auto=format&fit=crop'"
          :alt="product.name"
          class="w-full h-full object-cover transition-transform duration-700 group-hover:scale-110"
        />

        <div class="absolute inset-0 bg-gradient-to-t from-primary/60 via-primary/0 to-transparent opacity-0 group-hover:opacity-100 transition-opacity duration-500"></div>

        <div class="absolute top-4 left-4 flex flex-col gap-2">
          <span v-if="product.isFeatured" class="px-3 py-1 bg-accent text-white text-xs font-bold rounded-full shadow-lg backdrop-blur-sm">
            ⭐ YENİ
          </span>
          <span v-if="product.discountPercentage" class="px-3 py-1 bg-error text-white text-xs font-bold rounded-full shadow-lg backdrop-blur-sm">
            %{{ product.discountPercentage }} İNDİRİM
          </span>
        </div>

        <button
          @click.stop="toggleFavorite"
          class="absolute top-4 right-4 w-10 h-10 flex items-center justify-center bg-white/90 backdrop-blur-md hover:bg-white rounded-full shadow-lg transition-all duration-300 transform hover:scale-110 opacity-0 group-hover:opacity-100"
        >
          <svg
            :class="{ 'fill-error text-error': isFavorite, 'text-gray-400': !isFavorite }"
            class="w-5 h-5 transition-colors duration-300"
            fill="none"
            stroke="currentColor"
            viewBox="0 0 24 24"
          >
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M4.318 6.318a4.5 4.5 0 000 6.364L12 20.364l7.682-7.682a4.5 4.5 0 00-6.364-6.364L12 7.636l-1.318-1.318a4.5 4.5 0 00-6.364 0z" />
          </svg>
        </button>

        <div class="absolute bottom-0 left-0 right-0 p-4 translate-y-full group-hover:translate-y-0 transition-transform duration-500">
          <button
            @click.stop="addToCart"
            class="w-full py-3 bg-white hover:bg-primary text-primary hover:text-white rounded-xl font-bold text-sm tracking-wide transition-all duration-300 shadow-xl flex items-center justify-center gap-2 group/btn"
          >
            <svg class="w-5 h-5 transform group-hover/btn:rotate-12 transition-transform duration-300" fill="none" stroke="currentColor" viewBox="0 0 24 24">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M3 3h2l.4 2M7 13h10l4-8H5.4M7 13L5.4 5M7 13l-2.293 2.293c-.63.63-.184 1.707.707 1.707H17m0 0a2 2 0 100 4 2 2 0 000-4zm-8 2a2 2 0 11-4 0 2 2 0 014 0z" />
            </svg>
            SEPETE EKLE
          </button>
        </div>

      </div>

      <div class="p-5">

        <div class="mb-2">
          <span class="inline-block px-3 py-1 bg-gray-100 text-gray-600 text-xs font-semibold rounded-full uppercase tracking-wider">
            {{ product.color || 'Çok Renkli' }}
          </span>
        </div>

        <h3 class="font-serif text-xl font-bold text-gray-800 mb-2 group-hover:text-primary transition-colors duration-300 line-clamp-1">
          {{ product.name }}
        </h3>

        <div class="flex items-center gap-2 mb-3">
          <div :class="product.stockQuantity > 0 ? 'bg-success' : 'bg-error'" class="w-2 h-2 rounded-full"></div>
          <span :class="product.stockQuantity > 0 ? 'text-success' : 'text-error'" class="text-xs font-semibold">
            {{ product.stockQuantity > 0 ? 'Stokta Var' : 'Tükendi' }}
          </span>
        </div>

        <div class="flex items-center justify-between">
          <div>
            <span v-if="product.oldPrice" class="text-sm text-gray-400 line-through mr-2">
              {{ product.oldPrice }} ₺
            </span>
            <span class="text-2xl font-bold text-primary">
              {{ product.price }} ₺
            </span>
          </div>

          <button
            @click.stop="goToDetail"
            class="w-10 h-10 flex items-center justify-center bg-primary/10 hover:bg-primary text-primary hover:text-white rounded-full transition-all duration-300 transform hover:scale-110"
          >
            <svg class="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M15 12a3 3 0 11-6 0 3 3 0 016 0z" />
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M2.458 12C3.732 7.943 7.523 5 12 5c4.478 0 8.268 2.943 9.542 7-1.274 4.057-5.064 7-9.542 7-4.477 0-8.268-2.943-9.542-7z" />
            </svg>
          </button>
        </div>

      </div>

    </div>

  </div>
</template>

<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'

const props = defineProps({
  product: {
    type: Object,
    required: true
  },
  index: {
    type: Number,
    default: 0
  }
})

const router = useRouter()
const isFavorite = ref(false)

const goToDetail = () => {
  router.push(`/flower/${props.product.id}`)
}

const toggleFavorite = () => {
  isFavorite.value = !isFavorite.value
  // TODO: API call to add/remove favorite
}

const addToCart = () => {
  // TODO: Add to cart functionality
  console.log('Added to cart:', props.product.name)
}
</script>

<style scoped>
/* 🎨 Staggered Animation */
@keyframes fadeInScale {
  from {
    opacity: 0;
    transform: scale(0.9) translateY(20px);
  }
  to {
    opacity: 1;
    transform: scale(1) translateY(0);
  }
}

.animate-fade-in-scale {
  animation: fadeInScale 0.6s cubic-bezier(0.4, 0, 0.2, 1) forwards;
  opacity: 0;
}

/* Line clamp utility */
.line-clamp-1 {
  display: -webkit-box;
  -webkit-line-clamp: 1;
  -webkit-box-orient: vertical;
  overflow: hidden;
}
</style>