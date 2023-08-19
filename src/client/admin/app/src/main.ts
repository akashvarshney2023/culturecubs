import { createApp } from 'vue'
import App from '@/App.vue'
import router from './router'
import vuetify from './plugins/vuetify'
import { loadFonts } from './plugins/webfontloader'
import authPlugin from './plugins/auth';
loadFonts()

const app = createApp(App);
app.use(authPlugin);
app.use(vuetify);
app.use(router);
router.beforeEach((to, from, next) => {
  if (to.meta.requiresAuth && !app.config.globalProperties.auth.isAuthenticated()) {
    next('/login'); // Redirect to login if not authenticated
  } else {
    next();
  }
});
app.mount('#app');
