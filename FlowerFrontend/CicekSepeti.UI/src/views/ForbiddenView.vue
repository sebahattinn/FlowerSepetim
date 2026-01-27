<template>
  <div class="min-h-screen bg-[#FDFBF7] font-sans flex flex-col">
    <TheNavbar />

    <main class="flex-grow container mx-auto px-6 py-12 flex items-center justify-center">
      <div class="w-full max-w-4xl text-center">

        <div class="mb-8">
          <div class="text-8xl mb-4">⛔</div>
          <h1 class="text-5xl font-serif text-[#C41E3A] mb-4 font-bold">Yetkiniz Bulunmuyor</h1>
          <p class="text-gray-600 text-xl font-light max-w-2xl mx-auto leading-relaxed">
            Bu sayfaya erişim <strong class="text-[#C41E3A]">sadece yönetici yetkisine</strong> sahip kullanıcılar tarafından yapılabilir.
            <br>
            Hesabınız bu işlemi gerçekleştirmek için gerekli yetkilere sahip değil.
          </p>
        </div>

        <div class="flex gap-4 justify-center flex-wrap">
          <router-link
            to="/"
            class="btn bg-[#1B4D3E] text-white px-8 py-3 rounded-sm hover:bg-[#143d30] transition shadow-lg"
          >
            ANA SAYFAYA DÖN
          </router-link>

          <router-link
            to="/contact"
            class="btn bg-white text-[#1B4D3E] border-2 border-[#1B4D3E] px-8 py-3 rounded-sm hover:bg-gray-50 transition shadow-lg"
          >
            İLETİŞİME GEÇ
          </router-link>
        </div>

        <div class="mt-12 bg-white p-8 shadow-xl border-t-4 border-[#C41E3A] rounded-sm max-w-2xl mx-auto">
          <h3 class="text-xl font-serif text-[#1B4D3E] mb-4">Neden bu mesajı görüyorum?</h3>
          <ul class="text-left text-gray-600 space-y-2">
            <li class="flex items-start">
              <span class="text-[#C41E3A] mr-2">•</span>
              <span>Bu sayfa sadece yönetici yetkisine sahip kullanıcılar tarafından görüntülenebilir</span>
            </li>
            <li class="flex items-start">
              <span class="text-[#C41E3A] mr-2">•</span>
              <span>Hesabınız bu işlemi yapmak için gerekli izinlere sahip değil</span>
            </li>
            <li class="flex items-start">
              <span class="text-[#C41E3A] mr-2">•</span>
              <span>Erişim gerekiyorsa, lütfen yöneticinizden yetki talebinde bulunun</span>
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
