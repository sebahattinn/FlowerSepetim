<template>
  <div class="min-h-screen bg-[#FDFBF7] font-sans flex flex-col">
    <TheNavbar />

    <main class="flex-grow container mx-auto px-6 py-12 flex items-center justify-center">
      <div class="w-full max-w-4xl text-center">

        <div class="mb-8">
          <div class="text-8xl mb-4">🔐</div>
          <h1 class="text-5xl font-serif text-[#1B4D3E] mb-4 font-bold">Giriş Yapmanız Gerekiyor</h1>
          <p class="text-gray-600 text-xl font-light max-w-2xl mx-auto leading-relaxed">
            Bu sayfaya erişebilmek için öncelikle hesabınıza <strong class="text-[#1B4D3E]">giriş yapmalısınız</strong>.
            <br>
            Hesabınız yoksa hemen kayıt olarak çiçek dünyamıza katılabilirsiniz.
          </p>
        </div>

        <div class="flex gap-4 justify-center flex-wrap">
          <router-link
            to="/login"
            class="btn bg-[#1B4D3E] text-white px-8 py-3 rounded-sm hover:bg-[#143d30] transition shadow-lg"
          >
            GİRİŞ YAP
          </router-link>

          <router-link
            to="/register"
            class="btn bg-white text-[#1B4D3E] border-2 border-[#1B4D3E] px-8 py-3 rounded-sm hover:bg-gray-50 transition shadow-lg"
          >
            KAYIT OL
          </router-link>

          <router-link
            to="/"
            class="btn bg-gray-100 text-gray-700 px-8 py-3 rounded-sm hover:bg-gray-200 transition"
          >
            ANA SAYFAYA DÖN
          </router-link>
        </div>

        <div class="mt-12 bg-white p-8 shadow-xl border-t-4 border-[#D97706] rounded-sm max-w-2xl mx-auto">
          <h3 class="text-xl font-serif text-[#1B4D3E] mb-4">Üyelik Avantajları</h3>
          <ul class="text-left text-gray-600 space-y-2">
            <li class="flex items-start">
              <span class="text-[#1B4D3E] mr-2">✓</span>
              <span>Özel indirimler ve kampanyalardan haberdar olun</span>
            </li>
            <li class="flex items-start">
              <span class="text-[#1B4D3E] mr-2">✓</span>
              <span>Sipariş geçmişinizi takip edin</span>
            </li>
            <li class="flex items-start">
              <span class="text-[#1B4D3E] mr-2">✓</span>
              <span>Hızlı ve güvenli alışveriş yapın</span>
            </li>
          </ul>
        </div>

      </div>
    </main>
  </div>
</template>

<script setup>
import TheNavbar from '../components/TheNavbar.vue';
</script>
