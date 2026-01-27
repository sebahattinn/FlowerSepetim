<template>
  <div class="min-h-screen bg-gradient-to-br from-secondary via-white to-secondary font-sans flex flex-col relative overflow-hidden">

    <!-- Arka plan dekoratif elementler -->
    <div class="absolute inset-0 overflow-hidden pointer-events-none">
      <div class="absolute top-20 -right-20 w-96 h-96 bg-accent/10 rounded-full blur-3xl"></div>
      <div class="absolute -bottom-32 -left-32 w-96 h-96 bg-primary/10 rounded-full blur-3xl"></div>
    </div>

    <TheNavbar />

    <main class="flex-grow container mx-auto px-6 py-12 md:py-20 flex items-center justify-center relative z-10">
      <div class="w-full max-w-4xl">

        <!-- Ana içerik kartı -->
        <div class="bg-white/80 backdrop-blur-xl rounded-2xl p-8 md:p-12 shadow-glass border border-white/50 text-center">

          <!-- Icon -->
          <div
            class="inline-flex items-center justify-center w-24 h-24 md:w-32 md:h-32 rounded-full mb-6 animate-float"
            :class="iconBgClass"
          >
            <span class="text-5xl md:text-6xl">{{ icon }}</span>
          </div>

          <!-- Başlık -->
          <h1
            class="text-4xl md:text-5xl lg:text-6xl font-serif font-bold mb-4 animate-fade-in-up"
            :class="titleColorClass"
          >
            {{ title }}
          </h1>

          <!-- Alt başlık -->
          <p class="text-gray-600 text-lg md:text-xl font-light max-w-2xl mx-auto leading-relaxed mb-8 animate-fade-in-up animation-delay-100">
            <slot name="description"></slot>
          </p>

          <!-- Butonlar -->
          <div class="flex gap-4 justify-center flex-wrap mb-8 animate-fade-in-up animation-delay-200">
            <slot name="actions"></slot>
          </div>

          <!-- Ek bilgi kartı -->
          <div
            v-if="$slots.info"
            class="mt-8 bg-gradient-to-br from-gray-50 to-white p-6 md:p-8 rounded-xl border-t-4 animate-fade-in-up animation-delay-300"
            :class="infoBorderClass"
          >
            <slot name="info"></slot>
          </div>

        </div>

      </div>
    </main>
  </div>
</template>

<script setup>
import TheNavbar from './TheNavbar.vue'

defineProps({
  icon: {
    type: String,
    required: true,
  },
  title: {
    type: String,
    required: true,
  },
  titleColorClass: {
    type: String,
    default: 'text-primary',
  },
  iconBgClass: {
    type: String,
    default: 'bg-primary/10',
  },
  infoBorderClass: {
    type: String,
    default: 'border-primary',
  },
})
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
