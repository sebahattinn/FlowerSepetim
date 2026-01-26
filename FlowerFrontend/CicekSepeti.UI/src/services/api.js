import axios from 'axios'
import router from '../router'

// 🔗 Backend Base URL
const api = axios.create({
 //aseURL: 'https://localhost:7123/api',
    baseURL: 'https//sebahattin16-001-site1.jtempurl.com/api',
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

    // 🌐 Network / Backend kapalıysa
    if (!error.response) {
      console.error('Sunucuya ulaşılamıyor!')
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

        // 🔥 Sessizce yeni token iste
        // ⚠️ interceptor loop olmaması için axios (api değil)
        const response = await axios.post(
          'https://localhost:7123/api/Auth/refresh-token',
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
        api.defaults.headers.common[
          'Authorization'
        ] = `Bearer ${newAccessToken}`

        originalRequest.headers[
          'Authorization'
        ] = `Bearer ${newAccessToken}`

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