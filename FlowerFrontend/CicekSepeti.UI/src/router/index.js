import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import LoginView from '../views/LoginView.vue'
import RegisterView from '../views/RegisterView.vue'
import FlowerDetailView from '../views/FlowerDetailView.vue'
import CollectionView from '../views/CollectionView.vue'
import AdminDashboardView from '../views/AdminDashboardView.vue'
import NotFoundView from '../views/NotFoundView.vue' // 👈 404 IMPORT
import { useAuthStore } from '../stores/auth'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    // 🏠 HOME
    {
      path: '/',
      name: 'home',
      component: HomeView
    },

    // 🔐 AUTH (Misafir + Footer Gizli)
    {
      path: '/login',
      name: 'login',
      component: LoginView,
      meta: {
        requiresGuest: true,
        hideFooter: true
      }
    },
    {
      path: '/register',
      name: 'register',
      component: RegisterView,
      meta: {
        requiresGuest: true,
        hideFooter: true
      }
    },

    // 🌸 ÇİÇEK DETAY
    {
      path: '/flower/:id',
      name: 'flower-detail',
      component: FlowerDetailView
    },

    // 📦 KOLEKSİYON
    {
      path: '/collection',
      name: 'collection',
      component: CollectionView
    },

    // 📖 HİKAYEMİZ
    {
      path: '/story',
      name: 'story',
      component: () => import('../views/StoryView.vue')
    },

    // 📞 İLETİŞİM
    {
      path: '/contact',
      name: 'contact',
      component: () => import('../views/ContactView.vue')
    },

    // 🔥 SADECE ADMİN (Auth + Rol + Footer Gizli)
    {
      path: '/admin',
      name: 'admin',
      component: AdminDashboardView,
      meta: {
        requiresAuth: true,
        role: 'Admin',
        hideFooter: true
      }
    },

    // ❌ 404 – EN SONA!
    {
      path: '/:pathMatch(.*)*',
      name: 'not-found',
      component: NotFoundView,
      meta: {
        hideFooter: true
      }
    }
  ]
})

// 🛡️ GLOBAL ROUTE GUARD
router.beforeEach((to, from, next) => {
  const authStore = useAuthStore()

  // 🧠 Token var ama user yoksa → decode et
  if (authStore.isAuthenticated && !authStore.user) {
    authStore.decodeAndSetUser()
  }

  // 1️⃣ Auth gerekli sayfalar
  if (to.meta.requiresAuth && !authStore.isAuthenticated) {
    return next('/login')
  }

  // 2️⃣ Rol kontrolü
  if (to.meta.role && authStore.userRole !== to.meta.role) {
    alert('Bu sayfaya erişim yetkiniz yok!')
    return next('/')
  }

  // 3️⃣ Misafir sayfalar (login/register)
  if (to.meta.requiresGuest && authStore.isAuthenticated) {
    return next('/')
  }

  next()
})

export default router