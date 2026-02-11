<template>
  <div class="snow-container">
    <div
      v-for="snowflake in snowflakes"
      :key="snowflake.id"
      class="snowflake"
      :style="snowflake.style"
    >
      {{ snowflake.symbol }}
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'

const snowflakes = ref([])

const createSnowflake = (id) => {
  const symbols = ['❄', '❅', '❆', '✻', '✼', '❉', '✿']
  const left = Math.random() * 100
  const animationDuration = 5 + Math.random() * 10 // 5-15 saniye
  const opacity = 0.3 + Math.random() * 0.7
  const fontSize = 10 + Math.random() * 20 // 10-30px
  const delay = Math.random() * 5 // 0-5 saniye gecikme

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
  // 50 kar tanesi oluştur
  for (let i = 0; i < 50; i++) {
    snowflakes.value.push(createSnowflake(i))
  }
})
</script>

<style scoped>
.snow-container {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  pointer-events: none;
  z-index: 9999;
  overflow: hidden;
}

.snowflake {
  position: absolute;
  top: -50px;
  color: #fff;
  text-shadow: 0 0 5px rgba(255, 255, 255, 0.5);
  animation: fall linear infinite;
  user-select: none;
}

@keyframes fall {
  0% {
    transform: translateY(-50px) rotate(0deg);
  }
  100% {
    transform: translateY(100vh) rotate(360deg);
  }
}

/* Hafif rüzgar efekti için yatay hareket */
.snowflake:nth-child(3n) {
  animation: fall-left linear infinite;
}

.snowflake:nth-child(3n+1) {
  animation: fall-right linear infinite;
}

@keyframes fall-left {
  0% {
    transform: translateY(-50px) translateX(0) rotate(0deg);
  }
  50% {
    transform: translateY(50vh) translateX(-30px) rotate(180deg);
  }
  100% {
    transform: translateY(100vh) translateX(0) rotate(360deg);
  }
}

@keyframes fall-right {
  0% {
    transform: translateY(-50px) translateX(0) rotate(0deg);
  }
  50% {
    transform: translateY(50vh) translateX(30px) rotate(180deg);
  }
  100% {
    transform: translateY(100vh) translateX(0) rotate(360deg);
  }
}
</style>
