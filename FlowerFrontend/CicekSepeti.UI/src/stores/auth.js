import { defineStore } from 'pinia';
import api from '../services/api';
import router from '../router';
import { jwtDecode } from 'jwt-decode'; // 👈 JWT çözümleme

export const useAuthStore = defineStore('auth', {
    state: () => ({
        token: localStorage.getItem('token') || null,
        user: null, // { id, email, role }
    }),
    
    getters: {
        isAuthenticated: (state) => !!state.token,
        userRole: (state) => state.user?.role || null,
    },

    actions: {
        // 🧠 TOKEN'I ÇÖZ → USER STATE'E YAZ
        decodeAndSetUser() {
            if (!this.token) return;

            try {
                const decoded = jwtDecode(this.token);

                // .NET Core role claim (uzun URL veya role)
                const roleClaim =
                    decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']
                    || decoded['role'];

                this.user = {
                    id: decoded.nameid || decoded.sub,
                    email: decoded.email,
                    role: roleClaim
                };
            } catch (error) {
                console.error('Token çözülemedi:', error);
                this.logout(); // bozuk token → güvenli çıkış
            }
        },

        // LOGIN
        async login(email, password) {
            try {
                const response = await api.post('/Auth/login', { email, password });

                this.token = response.data.token;
                localStorage.setItem('token', this.token);

                if (response.data.refreshToken) {
                    localStorage.setItem('refreshToken', response.data.refreshToken);
                }

                // 🔥 Token gelir gelmez decode
                this.decodeAndSetUser();

                router.push('/');
                return true;
            } catch (error) {
                console.error('Giriş hatası:', error);
                throw error;
            }
        },

        // REGISTER
        async register(userData) {
            try {
                await api.post('/Auth/register', userData);
                router.push('/login');
                return true;
            } catch (error) {
                console.error('Kayıt hatası:', error);
                throw error;
            }
        },

        // LOGOUT (MULTI-TAB UYUMLU)
        logout() {
            this.token = null;
            this.user = null;

            // 🔥 diğer sekmeleri tetikler
            localStorage.removeItem('token');
            localStorage.removeItem('refreshToken');
            localStorage.removeItem('rememberedEmail');

            router.push('/login');
        }
    }
});