<template>
  <div class="min-h-screen bg-gray-50 flex flex-col font-sans">
    <TheNavbar />

    <main class="container mx-auto px-6 py-12 flex-grow">
      
      <div class="flex flex-col md:flex-row justify-between items-end mb-8 gap-4">
        <div>
          <h1 class="text-4xl font-serif text-[#1B4D3E] font-bold">Yönetim Paneli</h1>
          <p class="text-sm text-gray-500 mt-2 font-medium">Mağaza envanterini ve site durumunu yönetin.</p>
        </div>
        
        <div class="flex gap-4 items-center">
            
            <div class="form-control bg-white border border-red-100 p-2 rounded-sm shadow-sm hover:shadow-md transition">
                <label class="cursor-pointer label gap-3">
                    <span class="label-text font-bold text-xs uppercase tracking-wider" :class="maintenanceMode ? 'text-red-600' : 'text-gray-400'">
                        {{ maintenanceMode ? 'BAKIMDA 🚧' : 'SİTE AÇIK 🚀' }}
                    </span> 
                    <input 
                        type="checkbox" 
                        class="toggle toggle-error toggle-sm" 
                        :checked="maintenanceMode" 
                        @change="toggleMaintenance"
                    />
                </label>
            </div>

            <button 
            @click="openModal()" 
            class="btn bg-[#1B4D3E] hover:bg-[#143d30] text-white border-none rounded-sm px-6 py-3 font-serif tracking-widest shadow-lg transition-transform hover:scale-105"
            >
            + YENİ ÇİÇEK
            </button>
        </div>
      </div>

      <div class="grid grid-cols-1 md:grid-cols-4 gap-6 mb-8">
        <div class="bg-white p-6 rounded-sm shadow-sm border-l-4 border-[#1B4D3E]">
          <h3 class="text-gray-400 text-xs font-bold uppercase tracking-widest">Toplam Ürün</h3>
          <p class="text-3xl font-serif text-[#1B4D3E] mt-2">{{ flowers.length }} <span class="text-sm text-gray-400">Adet</span></p>
        </div>
        <div class="bg-white p-6 rounded-sm shadow-sm border-l-4 border-yellow-400">
          <h3 class="text-gray-400 text-xs font-bold uppercase tracking-widest">Vitrindekiler</h3>
          <p class="text-3xl font-serif text-yellow-500 mt-2">{{ flowers.filter(f => f.isFeatured).length }} <span class="text-sm text-gray-400">Yıldız</span></p>
        </div>
        <div class="bg-white p-6 rounded-sm shadow-sm border-l-4 border-green-600">
          <h3 class="text-gray-400 text-xs font-bold uppercase tracking-widest">Aktif Ürünler</h3>
          <p class="text-3xl font-serif text-green-600 mt-2">{{ flowers.filter(f => f.isActive).length }} <span class="text-sm text-gray-400">Adet</span></p>
        </div>
        <div class="bg-white p-6 rounded-sm shadow-sm border-l-4 border-orange-400">
          <h3 class="text-gray-400 text-xs font-bold uppercase tracking-widest">Kritik Stok</h3>
          <p class="text-3xl font-serif text-orange-400 mt-2">{{ flowers.filter(f => f.stockQuantity < 10).length }} <span class="text-sm text-gray-400">Adet</span></p>
        </div>
      </div>

      <div class="bg-white p-4 mb-6 shadow-sm rounded-sm border border-gray-100 flex items-center gap-4">
        <span class="text-2xl">🔍</span>
        <input 
          v-model="searchQuery" 
          type="text" 
          placeholder="Ürün adı, kategori veya fiyat ara..." 
          class="input input-ghost w-full focus:bg-transparent focus:outline-none text-gray-700 placeholder-gray-400 font-medium"
        />
        <div v-if="searchQuery" class="text-xs font-bold text-gray-400 uppercase tracking-widest">
            {{ filteredFlowers.length }} Sonuç
        </div>
      </div>

      <div v-if="loading" class="flex justify-center py-20">
        <span class="loading loading-spinner text-[#1B4D3E] loading-lg"></span>
      </div>
      
      <div v-else class="bg-white shadow-lg rounded-sm overflow-hidden border border-gray-100">
        <div class="overflow-x-auto">
          <table class="table w-full">
            <thead class="bg-gray-50 border-b border-gray-200">
              <tr class="text-gray-500 font-bold tracking-widest uppercase text-xs">
                <th class="py-4 pl-6">Görsel</th>
                <th>Ürün Adı</th>
                <th>Kategori</th>
                <th>Fiyat</th>
                <th>Stok</th>
                <th class="text-center">Vitrin</th>
                <th>Durum</th>
                <th class="text-right pr-6">İşlemler</th>
              </tr>
            </thead>
            
            <tbody class="divide-y divide-gray-100">
              <tr v-for="flower in filteredFlowers" :key="flower.id" class="hover:bg-green-50/30 transition duration-200 group">
                
                <td class="pl-6">
                  <div class="avatar">
                    <div class="w-16 h-16 rounded-lg border border-gray-100 shadow-sm overflow-hidden bg-gray-50 flex items-center justify-center">
                      <img 
                        v-if="flower.imageUrl && flower.imageUrl.startsWith('http')" 
                        :src="flower.imageUrl" 
                        @error="$event.target.style.display='none'; $event.target.nextElementSibling.style.display='flex'"
                        class="object-cover w-full h-full" 
                      />
                      <div :style="{ display: (flower.imageUrl && flower.imageUrl.startsWith('http')) ? 'none' : 'flex' }" class="w-full h-full flex flex-col items-center justify-center bg-gray-100 text-gray-400">
                        <span class="text-2xl">🌸</span>
                      </div>
                    </div>
                  </div>
                </td>
                
                <td class="font-serif text-[#1B4D3E] text-lg font-medium">{{ flower.name }}</td>
                <td class="text-gray-500 text-sm font-medium">{{ getCategoryName(flower.categoryId) }}</td>
                <td class="font-bold text-gray-800">{{ flower.price }} ₺</td>
                <td>
                  <span class="px-3 py-1 text-xs font-bold rounded-full border" :class="flower.stockQuantity < 10 ? 'bg-red-50 text-red-600 border-red-200' : 'bg-green-50 text-green-700 border-green-200'">
                    {{ flower.stockQuantity }} Adet
                  </span>
                </td>
                
                <td class="text-center">
                    <button 
                        @click="toggleFeatured(flower)" 
                        class="btn btn-ghost btn-circle btn-sm tooltip tooltip-right"
                        :data-tip="flower.isFeatured ? 'Vitrinden Kaldır' : 'Vitrine Ekle'"
                    >
                        <span v-if="flower.isFeatured" class="text-xl text-yellow-400 drop-shadow-sm transition-transform hover:scale-125">★</span>
                        <span v-else class="text-gray-300 text-xl transition-transform hover:scale-125 hover:text-yellow-400">•</span>
                    </button>
                </td>

                <td>
                  <span v-if="flower.isActive" class="badge badge-success badge-xs gap-1 text-white">Aktif</span>
                  <span v-else class="badge badge-error badge-xs gap-1 text-white">Pasif</span>
                </td>
                
                <td class="text-right pr-6">
                  <div class="flex justify-end gap-2">
                    <button @click="openModal(flower)" class="btn btn-sm btn-square btn-ghost text-gray-500 hover:text-[#1B4D3E] hover:bg-green-50 border border-transparent hover:border-green-200" title="Düzenle">✏️</button>
                    <button @click="deleteFlower(flower.id)" class="btn btn-sm btn-square btn-ghost text-gray-500 hover:text-red-500 hover:bg-red-50 border border-transparent hover:border-red-200" title="Sil">🗑️</button>
                  </div>
                </td>
              </tr>
            </tbody>
          </table>
          
          <div v-if="filteredFlowers.length === 0" class="p-8 text-center text-gray-400 font-medium">
             Aradığınız kriterlere uygun çiçek bulunamadı. 🥀
          </div>
        </div>
      </div>
    </main>

    <dialog id="flower_modal" class="modal backdrop-blur-sm">
      <div class="modal-box bg-white rounded-sm max-w-2xl border-t-8 border-[#1B4D3E] shadow-2xl p-8">
        <h3 class="font-serif text-3xl text-[#1B4D3E] mb-8 border-b pb-4 border-gray-100">
          {{ isEditing ? 'Çiçeği Düzenle' : 'Yeni Çiçek Ekle' }}
        </h3>
        
        <form @submit.prevent="saveFlower" class="grid grid-cols-1 md:grid-cols-2 gap-6">
          <div class="form-control col-span-2">
            <label class="label-text text-xs font-bold uppercase text-gray-500 mb-2">Çiçek Adı</label>
            <input v-model="form.name" required type="text" class="input input-bordered w-full bg-white text-gray-900 focus:border-[#1B4D3E] rounded-sm" />
          </div>

          <div class="form-control col-span-2 md:col-span-1">
            <label class="label-text text-xs font-bold uppercase text-gray-500 mb-2">Kategori</label>
            <div class="flex gap-2">
                <select v-model="form.categoryId" class="select select-bordered w-full bg-white text-gray-900 focus:border-[#1B4D3E] rounded-sm flex-grow">
                  <option disabled value="">Seçin</option>
                  <option v-for="cat in categories" :key="cat.id" :value="cat.id">{{ cat.name }}</option>
                </select>
                <button type="button" @click="openCategoryModal" class="btn btn-square btn-outline border-gray-300 text-[#1B4D3E] hover:bg-[#1B4D3E] hover:text-white rounded-sm" title="Yeni Kategori Ekle">+</button>
            </div>
          </div>

          <div class="form-control">
            <label class="label-text text-xs font-bold uppercase text-gray-500 mb-2">Fiyat (₺)</label>
            <input v-model="form.price" required type="number" step="0.01" class="input input-bordered w-full bg-white text-gray-900 focus:border-[#1B4D3E] rounded-sm" />
          </div>
          <div class="form-control">
            <label class="label-text text-xs font-bold uppercase text-gray-500 mb-2">Stok</label>
            <input v-model="form.stock" required type="number" class="input input-bordered w-full bg-white text-gray-900 focus:border-[#1B4D3E] rounded-sm" />
          </div>
           <div class="form-control">
            <label class="label-text text-xs font-bold uppercase text-gray-500 mb-2">Renk</label>
            <input v-model="form.color" type="text" class="input input-bordered w-full bg-white text-gray-900 focus:border-[#1B4D3E] rounded-sm" />
          </div>
          <div class="form-control col-span-2">
            <label class="label-text text-xs font-bold uppercase text-gray-500 mb-2">Görsel URL</label>
            <input v-model="form.imageUrl" type="text" placeholder="https://..." class="input input-bordered w-full bg-white text-gray-900 focus:border-[#1B4D3E] rounded-sm" />
          </div>
          <div class="form-control col-span-2">
            <label class="label-text text-xs font-bold uppercase text-gray-500 mb-2">Açıklama</label>
            <textarea v-model="form.description" class="textarea textarea-bordered w-full bg-white text-gray-900 focus:border-[#1B4D3E] rounded-sm h-24"></textarea>
          </div>
          
          <div class="col-span-2 grid grid-cols-2 gap-4 border-t border-gray-100 pt-4 mt-2">
            <div class="form-control border border-yellow-200 bg-yellow-50/50 rounded-sm p-3 hover:bg-yellow-50 transition cursor-pointer">
                <label class="cursor-pointer flex items-center justify-between">
                  <div>
                    <span class="block text-xs font-bold text-yellow-800 uppercase tracking-wider">Vitrine Ekle</span>
                    <span class="text-[10px] text-yellow-600">Ana sayfada gösterilsin mi?</span>
                  </div>
                  <input v-model="form.isFeatured" type="checkbox" class="checkbox checkbox-warning checkbox-sm rounded-sm" />
                </label>
            </div>
            <div class="form-control border border-gray-200 rounded-sm p-3 hover:bg-gray-50 transition cursor-pointer">
                <label class="cursor-pointer flex items-center justify-between">
                  <div>
                    <span class="block text-xs font-bold text-gray-600 uppercase tracking-wider">Satışta</span>
                    <span class="text-[10px] text-gray-400">Aktif mi?</span>
                  </div>
                  <input v-model="form.isActive" type="checkbox" class="toggle toggle-success toggle-sm" />
                </label>
            </div>
          </div>

          <div class="modal-action col-span-2 mt-6 flex justify-end gap-4">
            <button type="button" class="btn btn-ghost text-gray-500" onclick="flower_modal.close()">İPTAL</button>
            <button type="submit" class="btn bg-[#1B4D3E] text-white border-none hover:bg-[#143d30] px-8 rounded-sm">
              {{ isEditing ? 'GÜNCELLE' : 'KAYDET' }}
            </button>
          </div>
        </form>
      </div>
      <form method="dialog" class="modal-backdrop"><button>kapat</button></form>
    </dialog>

    <dialog id="category_modal" class="modal backdrop-blur-sm">
        <div class="modal-box bg-white rounded-sm max-w-sm border-t-4 border-[#1B4D3E] shadow-xl p-6">
            <h3 class="font-serif text-xl text-[#1B4D3E] mb-4">Yeni Kategori Ekle</h3>
            <div class="form-control">
                <input v-model="newCategoryName" type="text" placeholder="Kategori Adı" class="input input-bordered w-full bg-white text-gray-900 focus:border-[#1B4D3E] rounded-sm" @keyup.enter="saveCategory" />
            </div>
            <div class="modal-action mt-4 flex justify-end gap-2">
                <button type="button" class="btn btn-sm btn-ghost text-gray-500" onclick="category_modal.close()">İptal</button>
                <button type="button" @click="saveCategory" class="btn btn-sm bg-[#1B4D3E] text-white border-none hover:bg-[#143d30] rounded-sm">Ekle</button>
            </div>
        </div>
        <form method="dialog" class="modal-backdrop"><button>kapat</button></form>
    </dialog>

  </div>
</template>

<script setup>
import { ref, onMounted, reactive, computed } from 'vue'; 
import api from '../services/api';
import TheNavbar from '../components/TheNavbar.vue';
import { useToast } from "vue-toastification"; 

const toast = useToast(); 

// State
const flowers = ref([]);
const categories = ref([]);
const loading = ref(true);
const isEditing = ref(false);
const editingId = ref(null);
const newCategoryName = ref('');
const searchQuery = ref(''); 
const maintenanceMode = ref(false); // 🚧 YENİ: Bakım Modu Durumu

const form = reactive({
  name: '', price: 0, stock: 0, imageUrl: '', description: '', categoryId: '', color: '', isActive: true, isFeatured: false 
});

// FİLTRELEME
const filteredFlowers = computed(() => {
  if (!searchQuery.value) return flowers.value;
  
  const query = searchQuery.value.toLowerCase();
  
  return flowers.value.filter(flower => {
    const catName = getCategoryName(flower.categoryId).toLowerCase();
    return (
      flower.name.toLowerCase().includes(query) ||
      catName.includes(query) ||
      flower.price.toString().includes(query)
    );
  });
});

const loadData = async () => {
  loading.value = true;
  try {
    const [flowersRes, categoriesRes, maintenanceRes] = await Promise.all([
      api.get('/Flowers'),
      api.get('/Flowers/categories'),
      api.get('/Settings/status') // 🚧 YENİ: Durumu Çekiyoruz
    ]);
    flowers.value = flowersRes.data;
    categories.value = categoriesRes.data;
    maintenanceMode.value = maintenanceRes.data.isMaintenance; // 🚧 YENİ
  } catch (error) {
    console.error("Veri hatası:", error);
    toast.error("Veriler yüklenirken hata oluştu!"); 
  } finally {
    loading.value = false;
  }
};

// 🚧 YENİ: BAKIM MODUNU AÇ/KAPA
const toggleMaintenance = async () => {
    try {
        const newState = !maintenanceMode.value; // Tersine çeviriyoruz
        
        // Backend'e gönderiyoruz (Backend [FromBody] bool bekliyorsa content-type json olmalı)
        // Axios otomatik olarak JSON gönderir.
        await api.post('/Settings/toggle', newState, {
            headers: { 'Content-Type': 'application/json' }
        });

        // UI Güncelle
        maintenanceMode.value = newState;

        if(newState) {
            toast.warning("SİTE BAKIMA ALINDI! 🚧 (Sadece Adminler Görebilir)");
        } else {
            toast.success("SİTE YAYINA AÇILDI! 🚀");
        }
    } catch (error) {
        console.error(error);
        toast.error("Bakım modu değiştirilemedi!");
        // Hata olursa UI'ı eski haline getir (Checkbox'ı geri al)
        maintenanceMode.value = !maintenanceMode.value; 
    }
}

// 🌟 TEK TIKLA VİTRİN
const toggleFeatured = async (flower) => {
    const originalState = flower.isFeatured;
    flower.isFeatured = !originalState;

    try {
        const payload = { ...flower };
        delete payload.id; 
        await api.put(`/Flowers/${flower.id}`, payload);
        if (flower.isFeatured) toast.success(`${flower.name} vitrine eklendi! 🌟`);
        else toast.info(`${flower.name} vitrinden kaldırıldı.`);
    } catch (error) {
        flower.isFeatured = originalState;
        toast.error("İşlem başarısız oldu.");
    }
};

const getCategoryName = (id) => {
  const cat = categories.value.find(c => c.id === id);
  return cat ? cat.name : '-';
};

const openModal = (flower = null) => {
  if (flower) {
    isEditing.value = true;
    editingId.value = flower.id;
    Object.assign(form, { ...flower });
  } else {
    isEditing.value = false;
    editingId.value = null;
    Object.assign(form, { name: '', price: 0, stock: 0, imageUrl: '', description: '', categoryId: '', color: '', isActive: true, isFeatured: false });
  }
  document.getElementById('flower_modal').showModal();
};

const openCategoryModal = () => {
    newCategoryName.value = '';
    document.getElementById('category_modal').showModal();
}

const saveCategory = async () => {
    if(!newCategoryName.value.trim()) return;
    try {
        const response = await api.post('/Flowers/categories', JSON.stringify(newCategoryName.value), {
            headers: { 'Content-Type': 'application/json' }
        });
        categories.value.push({ id: response.data.id, name: response.data.name });
        form.categoryId = response.data.id;
        document.getElementById('category_modal').close();
        toast.success("Kategori eklendi! 📂"); 
    } catch (error) {
        toast.error("Kategori eklenemedi."); 
    }
}

const saveFlower = async () => {
  try {
    const payload = { ...form };
    if(!payload.categoryId) { toast.warning("Lütfen bir kategori seçin."); return; } 

    if (isEditing.value) {
      await api.put(`/Flowers/${editingId.value}`, payload);
      toast.success("Çiçek güncellendi! 🌸"); 
    } else {
      await api.post('/Flowers', payload);
      toast.success("Yeni çiçek eklendi! 🌸"); 
    }
    document.getElementById('flower_modal').close();
    await loadData();
  } catch (error) {
    const msg = error.response?.data?.message || error.message;
    toast.error("Hata: " + msg); 
  }
};

const deleteFlower = async (id) => {
  if (!confirm("Silmek istediğinize emin misiniz?")) return;
  try {
    await api.delete(`/Flowers/${id}`);
    toast.info("Çiçek silindi."); 
    await loadData();
  } catch (error) {
    toast.error("Silinemedi.");
  }
};

onMounted(() => {
  loadData();
});
</script>