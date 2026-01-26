<template>
  <div class="min-h-screen bg-[#FDFBF7] font-sans flex flex-col">
    <TheNavbar />

    <main class="flex-grow container mx-auto px-6 py-12 flex items-center justify-center">
      <div class="w-full max-w-6xl flex flex-col md:flex-row gap-12 items-center">
        
        <div class="w-full md:w-1/2 text-center md:text-left">
          <h1 class="text-[150px] leading-none font-serif font-bold text-[#1B4D3E] opacity-90">404</h1>
          <h2 class="text-4xl font-serif text-[#1B4D3E] mb-6">Sayfa Bulunamadı</h2>
          <p class="text-gray-500 text-lg mb-8 font-light">
            Aradığınız sayfa taşınmış, silinmiş veya hiç var olmamış olabilir. 
            Ama merak etmeyin, biz buradayız!
          </p>
          <router-link to="/" class="btn bg-[#1B4D3E] text-white px-8 py-3 rounded-sm hover:bg-[#143d30] transition shadow-lg">
            ANA SAYFAYA DÖN
          </router-link>
        </div>

        <div class="w-full md:w-1/2 bg-white p-8 md:p-12 shadow-xl border-t-4 border-[#1B4D3E] rounded-sm">
          <h3 class="text-2xl font-serif text-[#1B4D3E] mb-6">Bize Ulaşın</h3>
          
          <form @submit.prevent="sendToWhatsapp" class="space-y-4">
            <div>
              <input v-model="form.name" required type="text" placeholder="İsim Soyisim" class="w-full bg-gray-50 border border-gray-200 p-4 rounded-sm focus:outline-none focus:border-[#1B4D3E]">
            </div>
            <div>
              <input v-model="form.phone" required type="tel" placeholder="Telefon" class="w-full bg-gray-50 border border-gray-200 p-4 rounded-sm focus:outline-none focus:border-[#1B4D3E]">
            </div>
            <div>
              <textarea v-model="form.message" required rows="4" placeholder="Mesajınız..." class="w-full bg-gray-50 border border-gray-200 p-4 rounded-sm focus:outline-none focus:border-[#1B4D3E]"></textarea>
            </div>
            <button type="submit" class="w-full bg-[#1B4D3E] text-white py-4 rounded-sm font-bold tracking-widest hover:bg-[#143d30] transition">
              GÖNDER
            </button>
          </form>

          <div class="mt-6 flex justify-end">
             <div class="bg-[#25D366] text-white p-3 rounded-full shadow-lg animate-bounce cursor-pointer" @click="sendToWhatsapp">
                <span class="text-2xl">💬</span>
             </div>
          </div>
        </div>

      </div>
    </main>
  </div>
</template>

<script setup>
import { reactive } from 'vue';
import TheNavbar from '../components/TheNavbar.vue';

const form = reactive({ name: '', phone: '', message: '' });

const sendToWhatsapp = () => {
  const myNumber = "905343058284";
  const text = `Merhaba (404 Sayfasından), ben ${form.name}.%0A%0ATelefon: ${form.phone}%0A%0AMesaj: ${form.message}`;
  window.open(`https://wa.me/${myNumber}?text=${text}`, '_blank');
};
</script>