import type { App } from 'vue';
export default {
    install: (app:App) => {
        app.config.globalProperties.$auth = {
            getToken() {
                return localStorage.getItem('token');
            },
            isAuthenticated() {
                const token = this.getToken();
                return !!token;
            },
            logout() {
                localStorage.removeItem('token');
            }
        };
    }
};