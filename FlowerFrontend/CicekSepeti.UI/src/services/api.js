import axios from 'axios'
import router from '../router'

// 🔗 Backend Base URL
// Development: Direkt SmartASP backend'e bağlanır
// Production: Vercel proxy (/api) kullanır
const api = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL || '/api',
  headers: {
    'Content-Type': 'application/json'
  }
})

/* ================================
   1️⃣ REQUEST INTERCEPTOR
   Giden her isteğe Access Token ekler
================================ */
api.interceptors.request.use((config) => {
  const token = localStorage.getItem('token')

  if (token) {
    config.headers.Authorization = `Bearer ${token}`
  }

  return config
})

/* ================================
   2️⃣ RESPONSE INTERCEPTOR
   401 → Refresh Token → Retry
================================ */
api.interceptors.response.use(
  (response) => response,

  async (error) => {
    const originalRequest = error.config

    // 🌐 Network / Backend kapalıysa veya hata döndüyse
    if (!error.response) {
      console.error('Sunucuya ulaşılamıyor veya Ağ hatası!')
      return Promise.reject(error)
    }

    // 🚨 401 + daha önce retry edilmediyse
    if (error.response.status === 401 && !originalRequest._retry) {
      originalRequest._retry = true

      try {
        const refreshToken = localStorage.getItem('refreshToken')

        // Refresh token yoksa → direkt logout
        if (!refreshToken) {
          throw new Error('Refresh token bulunamadı')
        }

        // 🔥 KRİTİK DÜZELTME: Localhost yerine '/api' üzerinden istek atıyoruz.
        // Böylece Vercel proxy'si burada da devreye giriyor.
        const response = await axios.post(
          '/api/Auth/refresh-token',
          {
            refreshToken: refreshToken,
          }
        )

        const {
          token: newAccessToken,
          refreshToken: newRefreshToken,
        } = response.data

        // 💾 Tokenları güncelle
        localStorage.setItem('token', newAccessToken)
        localStorage.setItem('refreshToken', newRefreshToken)

        // 🔄 Axios header’ları güncelle
        api.defaults.headers.common['Authorization'] = `Bearer ${newAccessToken}`
        originalRequest.headers['Authorization'] = `Bearer ${newAccessToken}`

        // ▶️ İlk başarısız isteği tekrar dene
        return api(originalRequest)

      } catch (refreshError) {
        console.error('Oturum yenilenemedi, çıkış yapılıyor...', refreshError)

        // 🧹 Temizlik
        localStorage.removeItem('token')
        localStorage.removeItem('refreshToken')
        localStorage.removeItem('rememberedEmail') // opsiyonel

        // 🚪 Login’e yönlendir
        router.push('/login')

        return Promise.reject(refreshError)
      }
    }

    return Promise.reject(error)
  }
)

export default api