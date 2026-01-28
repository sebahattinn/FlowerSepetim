import { createApp } from 'vue'
import { createPinia } from 'pinia'
import App from './App.vue'
import router from './router'

//  Global CSS
import './style.css'

//  TOAST IMPORTLARI
import Toast from 'vue-toastification'
import 'vue-toastification/dist/index.css'

const app = createApp(App)

//  STATE & ROUTER
app.use(createPinia())
app.use(router)

//  TOAST AYARLARI
const toastOptions = {
  position: 'top-right',
  timeout: 3000,
  closeOnClick: true,
  pauseOnFocusLoss: true,
  pauseOnHover: true,
  draggable: true,
  draggablePercent: 0.6,
  showCloseButtonOnHover: false,
  hideProgressBar: false,
  closeButton: 'button',
  icon: true,
  rtl: false
}

//  Toast aktif
app.use(Toast, toastOptions)

//  APP MOUNT
app.mount('#app')