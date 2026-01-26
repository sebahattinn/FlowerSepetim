<template>
  <div class="min-h-screen bg-[#FDFBF7] font-sans flex flex-col">
    <TheNavbar />

    <main class="container mx-auto px-6 py-12 flex-grow flex justify-center">
      <div class="bg-white p-8 md:p-16 shadow-xl border-t-8 border-[#1B4D3E] max-w-4xl w-full rounded-sm">
        
        <h1 class="text-4xl md:text-5xl font-serif text-[#1B4D3E] mb-10 text-center tracking-wide">
          Doğanın Kalbine Yolculuk
        </h1>
        
        <div class="space-y-6 text-gray-600 font-light leading-loose text-lg text-justify">
          <p>
            <span class="text-6xl float-left mr-3 mt-[-15px] font-serif text-[#D4AF37]">H</span>
            er şey tek bir tohumla başladı. Çiçeklerin sadece birer hediye değil, duyguların en saf tercümanı olduğuna inandık. "Boutique" anlayışımız, fabrikasyon üretimden uzak, her biri sanat eseri niteliğinde aranjmanlar sunma tutkumuzdan doğdu.
          </p>
          <p>
            Tasarımcılarımız, mevsimin en taze renklerini ve kokularını bir araya getirirken sadece estetiği değil, o çiçeğin anlatacağı hikayeyi de önemsiyor. Bizim için her sipariş, iletilmesi gereken bir aşk mektubu, bir özür veya kocaman bir "İyi ki varsın" demektir.
          </p>
          
          <div class="py-8">
            <div class="overflow-hidden rounded-sm shadow-sm h-80 relative group">
              <img 
                :src="imgAtolye" 
                class="w-full h-full object-cover transition duration-700 group-hover:scale-105" 
                alt="Atölye"
              >
              <div class="absolute bottom-4 right-4 bg-white/80 px-4 py-1 text-xs text-[#1B4D3E] italic backdrop-blur-sm rounded-sm">
                Atölyemizden bir kare
              </div>
            </div>
          </div>

          <h2 class="text-3xl font-serif text-[#1B4D3E] mt-8 mb-6 text-center border-b border-gray-100 pb-4">
            Vizyonumuz
          </h2>
          <p>
            Doğallıktan ödün vermeden, modern ve zamansız tasarımlarla evlerinize ve kalplerinize misafir olmak. Sürdürülebilir tarımı destekliyor, yerel üreticilerle el ele çalışıyoruz.
          </p>

          <div class="grid grid-cols-1 md:grid-cols-2 gap-4 mt-8">
            <div class="h-48 overflow-hidden rounded-sm">
               <img :src="imgDetay1" class="w-full h-full object-cover hover:opacity-90 transition" alt="Detay 1">
            </div>
            <div class="h-48 overflow-hidden rounded-sm">
               <img :src="imgDetay2" class="w-full h-full object-cover hover:opacity-90 transition" alt="Detay 2">
            </div>
          </div>

        </div>

        <div class="mt-16 text-center">
            <p class="font-serif text-4xl text-[#D4AF37] opacity-80" style="font-family: 'Playfair Display', serif; font-style: italic;">
              Nehir Çiçekçilik
            </p>
            <p class="text-[10px] font-bold uppercase tracking-[0.3em] text-[#1B4D3E] mt-2">Kurucu Tasarımcı</p>
        </div>
      </div>
    </main>
  </div>
</template>

<script setup>
// 👇 Noktalı import kullanıyoruz (Relative Path)
import TheNavbar from '../components/TheNavbar.vue';

// 📂 Local Görselleri Çekiyoruz
// '../' demek "bulunduğum klasörden (views) bir yukarı çık (src)" demektir.
import imgAtolye from '../assets/images/indir.jpg';
import imgDetay1 from '../assets/images/indir2.jpg';
import imgDetay2 from '../assets/images/indir3.jpg';
</script>