<template>
  <div class="min-h-screen bg-[#FDFBF7] font-sans flex flex-col">
    <TheNavbar />

    <main class="flex-grow container mx-auto px-6 py-12 flex items-center justify-center">
      <div class="bg-white shadow-2xl rounded-sm overflow-hidden w-full max-w-5xl flex flex-col md:flex-row min-h-[600px]">
        
        <div class="bg-[#1B4D3E] text-white p-12 md:w-5/12 flex flex-col justify-center relative">
          <div class="absolute top-0 left-0 w-32 h-32 bg-white/5 rounded-br-full"></div>
          
          <h2 class="text-4xl font-serif font-bold mb-6 tracking-wide">Bizimle İletişime Geçin</h2>
          <p class="text-gray-300 mb-12 font-light leading-relaxed">
            Özel tasarım siparişleriniz, kurumsal işbirlikleri veya sadece bir merhaba demek için buradayız.
          </p>

          <div class="space-y-8">
            <div class="flex items-start gap-4">
              <span class="text-2xl mt-1">📍</span>
              <div>
                <h3 class="text-xs font-bold uppercase tracking-widest text-[#D4AF37] mb-1">Adres</h3>
                <p class="text-sm text-gray-300">Osmangazi Bursa</p>
              </div>
            </div>

            <div class="flex items-start gap-4">
              <span class="text-2xl mt-1">📞</span>
              <div>
                <h3 class="text-xs font-bold uppercase tracking-widest text-[#D4AF37] mb-1">Telefon</h3>
                <p class="text-sm text-gray-300">+90 534 305 82 84</p>
              </div>
            </div>

            <div class="flex items-start gap-4">
              <span class="text-2xl mt-1">✉️</span>
              <div>
                <h3 class="text-xs font-bold uppercase tracking-widest text-[#D4AF37] mb-1">Email</h3>
                <p class="text-sm text-gray-300">info@nehircicekcilik.com</p>
              </div>
            </div>
          </div>

          <p class="mt-auto text-xs text-gray-400">© 2026 Nehir Çiçekçilik.</p>
        </div>

        <div class="p-12 md:w-7/12 flex flex-col justify-center bg-white">
          <form @submit.prevent="sendToWhatsapp" class="space-y-6">
            
            <div>
              <label class="block text-xs font-bold uppercase text-gray-500 tracking-widest mb-2">Adınız Soyadınız</label>
              <input 
                v-model="form.name"
                required
                type="text" 
                class="w-full bg-gray-50 text-gray-900 border border-gray-200 p-4 rounded-sm focus:outline-none focus:border-[#1B4D3E] focus:bg-white transition-colors font-medium placeholder-gray-400"
                placeholder="Adınız Soyadınız"
              >
            </div>

            <div>
              <label class="block text-xs font-bold uppercase text-gray-500 tracking-widest mb-2">Telefon Numaranız</label>
              <input 
                v-model="form.phone"
                required
                type="tel" 
                class="w-full bg-gray-50 text-gray-900 border border-gray-200 p-4 rounded-sm focus:outline-none focus:border-[#1B4D3E] focus:bg-white transition-colors font-medium placeholder-gray-400"
                placeholder="05XX XXX XX XX"
              >
            </div>

            <div>
              <label class="block text-xs font-bold uppercase text-gray-500 tracking-widest mb-2">Mesajınız</label>
              <textarea 
                v-model="form.message"
                required
                rows="4" 
                class="w-full bg-gray-50 text-gray-900 border border-gray-200 p-4 rounded-sm focus:outline-none focus:border-[#1B4D3E] focus:bg-white transition-colors font-medium placeholder-gray-400"
                placeholder="Sipariş vermek istiyorum..."
              ></textarea>
            </div>

            <button type="submit" class="btn bg-[#25D366] hover:bg-[#128C7E] text-white w-full py-4 rounded-sm font-serif tracking-widest shadow-lg transform hover:-translate-y-1 transition-all flex items-center justify-center gap-2">
              <span class="text-xl">💬</span> WHATSAPP İLE GÖNDER
            </button>
          </form>
        </div>

      </div>
    </main>
  </div>
</template>

<script setup>
import { reactive } from 'vue';
import TheNavbar from '../components/TheNavbar.vue';

// Form verilerini tutuyoruz
const form = reactive({
  name: '',
  phone: '',
  message: ''
});

const sendToWhatsapp = () => {
  // 1. Senin numaran (Başında 90 olacak, + olmayacak)
  const myNumber = "905343058284";

  // 2. Müşterinin yazdığı mesajı formatlıyoruz
  // "\n" işareti alt satıra geçmek içindir.
  const text = `Merhaba Nehir Çiçekçilik, ben ${form.name}.%0A%0ATelefon Numaram: ${form.phone}%0A%0AMesajım:%0A${form.message}`;

  // 3. WhatsApp linkini oluşturuyoruz
  // encodeURIComponent kullanmıyoruz çünkü yukarıda %0A ile manuel kodladık, daha temiz dursun diye.
  const url = `https://wa.me/${myNumber}?text=${text}`;

  // 4. Yeni sekmede açıyoruz
  window.open(url, '_blank');
};
</script>