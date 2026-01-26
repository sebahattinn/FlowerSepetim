<template>
  <footer class="bg-[#1B4D3E] text-white pt-10 pb-6 font-sans text-sm">
    <div class="container mx-auto px-6 grid grid-cols-1 md:grid-cols-4 gap-8 mb-8">
      
      <div class="col-span-1 md:col-span-2">
        <h2 class="text-2xl font-serif font-bold tracking-widest mb-3 text-accent">
          Nehir Çiçekçilik
        </h2>
        <p class="text-gray-300 text-xs leading-relaxed mb-4 max-w-sm font-light">
          Doğanın en taze mucizelerini, sevdiklerinize ulaştırıyoruz. 
          Her bukette bir hikaye saklı.
        </p>
        <div class="flex gap-4 text-gray-400 text-xs">
          <a 
            href="https://www.instagram.com/nehir.cicekcilikk?igsh=bjZtc2UxYXM0Zmlz" 
            target="_blank" 
            rel="noopener noreferrer" 
            class="hover:text-accent transition"
          >
            INSTAGRAM
          </a>
        </div>
      </div>

      <div>
        <h3 class="text-xs font-bold uppercase tracking-[0.2em] mb-4 text-accent">Kurumsal</h3>
        <ul class="space-y-2 text-xs text-gray-300 font-medium tracking-wide">
          <li><router-link to="/story" class="hover:text-accent transition">Hikayemiz</router-link></li>
          <li><a href="#" class="hover:text-accent transition">Sıkça Sorulan Sorular</a></li>
          <li><a href="#" class="hover:text-accent transition">Kargo & Teslimat</a></li>
        </ul>
      </div>

      <div>
        <h3 class="text-xs font-bold uppercase tracking-[0.2em] mb-4 text-accent">İletişim</h3>
        <ul class="space-y-3 text-xs text-gray-300 font-medium tracking-wide">
          <li>
            <span class="block text-[10px] text-gray-500 uppercase mb-0.5">Adres</span>
             Osmangazi, Bursa
          </li>
          <li>
            <span class="block text-[10px] text-gray-500 uppercase mb-0.5">Telefon</span>
            <a href="tel:+905343058284" class="hover:text-accent transition">+90 (534) 305 82 84</a>
          </li>
        </ul>
      </div>

    </div>

    <div class="container mx-auto px-6 pt-4 border-t border-[#2C5F50] flex flex-col md:flex-row justify-between items-center text-[10px] text-gray-400 tracking-wider uppercase font-medium">
      <p>© 2026 Nehir Çiçekçilik. Tüm hakları saklıdır.</p>
      <div class="flex gap-4 mt-2 md:mt-0">
        <a href="#" class="hover:text-accent transition">Gizlilik</a>
        <a href="#" class="hover:text-accent transition">Kullanım</a>
      </div>
    </div>
  </footer>
</template>