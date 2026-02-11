<template>
  <div class="valentine-container">
    <!-- Uçuşan Kalpler -->
    <div
      v-for="heart in hearts"
      :key="heart.id"
      class="floating-heart"
      :style="heart.style"
    >
      {{ heart.symbol }}
    </div>

    <!-- Köşe Dekorasyonları -->
    <div class="corner-decoration top-left">
      <span class="decoration-element">💕</span>
      <span class="decoration-element">🌹</span>
      <span class="decoration-element">✨</span>
    </div>

    <div class="corner-decoration top-right">
      <span class="decoration-element">💖</span>
      <span class="decoration-element">🌸</span>
      <span class="decoration-element">✨</span>
    </div>

    <!-- Sevgililer Günü Özel Banner (isteğe bağlı) -->
    <div class="valentine-banner">
      <div class="banner-content">
        <span class="banner-icon">💝</span>
        <span class="banner-text">Sevgililer Günü Özel Koleksiyonu</span>
        <span class="banner-icon">💝</span>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'

const hearts = ref([])

const createHeart = (id) => {
  const symbols = ['❤️', '💕', '💖', '💗', '💓', '💞', '💝', '🌹', '🌺', '🌸']
  const left = Math.random() * 100
  const animationDuration = 8 + Math.random() * 8 // 8-16 saniye
  const opacity = 0.2 + Math.random() * 0.5
  const fontSize = 15 + Math.random() * 25 // 15-40px
  const delay = Math.random() * 8

  return {
    id,
    symbol: symbols[Math.floor(Math.random() * symbols.length)],
    style: {
      left: `${left}%`,
      opacity: opacity,
      fontSize: `${fontSize}px`,
      animationDuration: `${animationDuration}s`,
      animationDelay: `${delay}s`
    }
  }
}

onMounted(() => {
  // 30 kalp oluştur
  for (let i = 0; i < 30; i++) {
    hearts.value.push(createHeart(i))
  }
})
</script>

<style scoped>
.valentine-container {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  pointer-events: none;
  z-index: 9998;
  overflow: hidden;
}

/* Uçuşan Kalpler */
.floating-heart {
  position: absolute;
  bottom: -50px;
  color: #ff6b9d;
  filter: drop-shadow(0 0 10px rgba(255, 107, 157, 0.5));
  animation: float-up linear infinite;
  user-select: none;
}

@keyframes float-up {
  0% {
    transform: translateY(0) translateX(0) rotate(0deg) scale(0.5);
    opacity: 0;
  }
  10% {
    opacity: var(--opacity, 0.5);
  }
  90% {
    opacity: var(--opacity, 0.5);
  }
  100% {
    transform: translateY(-100vh) translateX(50px) rotate(360deg) scale(1);
    opacity: 0;
  }
}

/* Her 2. kalp sola doğru hareket etsin */
.floating-heart:nth-child(2n) {
  animation: float-up-left linear infinite;
}

@keyframes float-up-left {
  0% {
    transform: translateY(0) translateX(0) rotate(0deg) scale(0.5);
    opacity: 0;
  }
  10% {
    opacity: var(--opacity, 0.5);
  }
  90% {
    opacity: var(--opacity, 0.5);
  }
  100% {
    transform: translateY(-100vh) translateX(-50px) rotate(-360deg) scale(1);
    opacity: 0;
  }
}

/* Köşe Dekorasyonları */
.corner-decoration {
  position: absolute;
  display: flex;
  gap: 8px;
  animation: gentle-pulse 3s ease-in-out infinite;
}

.top-left {
  top: 20px;
  left: 20px;
}

.top-right {
  top: 20px;
  right: 20px;
  flex-direction: row-reverse;
}

.decoration-element {
  font-size: 24px;
  filter: drop-shadow(0 2px 8px rgba(255, 107, 157, 0.3));
  animation: bounce-gentle 2s ease-in-out infinite;
}

.decoration-element:nth-child(2) {
  animation-delay: 0.2s;
}

.decoration-element:nth-child(3) {
  animation-delay: 0.4s;
}

@keyframes gentle-pulse {
  0%, 100% {
    transform: scale(1);
  }
  50% {
    transform: scale(1.05);
  }
}

@keyframes bounce-gentle {
  0%, 100% {
    transform: translateY(0);
  }
  50% {
    transform: translateY(-5px);
  }
}

/* Sevgililer Günü Banner */
.valentine-banner {
  position: absolute;
  top: 80px;
  left: 50%;
  transform: translateX(-50%);
  z-index: 10;
  pointer-events: auto;
}

.banner-content {
  display: inline-flex;
  align-items: center;
  gap: 12px;
  padding: 12px 32px;
  background: linear-gradient(135deg, #ff6b9d 0%, #ffa8c5 100%);
  border-radius: 50px;
  box-shadow: 0 8px 32px rgba(255, 107, 157, 0.4);
  animation: banner-glow 2s ease-in-out infinite;
}

.banner-icon {
  font-size: 24px;
  animation: spin-gentle 4s linear infinite;
}

.banner-text {
  font-size: 14px;
  font-weight: 700;
  color: white;
  text-shadow: 0 2px 4px rgba(0, 0, 0, 0.2);
  letter-spacing: 0.5px;
  text-transform: uppercase;
}

@keyframes banner-glow {
  0%, 100% {
    box-shadow: 0 8px 32px rgba(255, 107, 157, 0.4);
  }
  50% {
    box-shadow: 0 8px 48px rgba(255, 107, 157, 0.6);
  }
}

@keyframes spin-gentle {
  from {
    transform: rotate(0deg);
  }
  to {
    transform: rotate(360deg);
  }
}

/* Mobil Uyumluluk */
@media (max-width: 768px) {
  .corner-decoration {
    gap: 4px;
  }

  .decoration-element {
    font-size: 18px;
  }

  .banner-content {
    padding: 10px 24px;
    gap: 8px;
  }

  .banner-icon {
    font-size: 20px;
  }

  .banner-text {
    font-size: 11px;
  }

  .valentine-banner {
    top: 60px;
  }
}

/* Tablet Uyumluluk */
@media (min-width: 769px) and (max-width: 1024px) {
  .banner-text {
    font-size: 12px;
  }
}
</style>
