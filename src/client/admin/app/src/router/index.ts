import { createRouter, createWebHistory } from 'vue-router'
import LoginView from '../views/LoginView.vue'
import RegisterView from '@/views/RegisterView.vue'
import EditContest from '../views/contest/EditContest.vue'
import ContestView from '../views/contest/ContestView.vue'
import ParticipantsView from '../views/participants/ParticipantsView.vue'
import HomeView from '@/views/footer/HomeView.vue'
import AboutView from '../views/footer/AboutView.vue'
import BlogView from '../views/footer/BlogView.vue'
import ContactUsView from '../views/footer/ContactUsView.vue'
import ServicesView from '../views/footer/ServicesView.vue'
import TeamView from '../views/footer/TeamView.vue'
import Settings from '@/views/settings/Settings.vue'
import CandidateView from '@/views/candidate/CandidateView.vue'
const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
        {
          path: '/settings',
          name: 'settings',
          component: Settings,
          meta: { requiresAuth: true }
        },
        {
          path: '/participants',
          name: 'participants',
          component: ParticipantsView,
          meta: { requiresAuth: true }
        },
        {
          path: '/contest',
          name: 'contest',
          component: ContestView,
          meta: { requiresAuth: true }
        },
        {
          path: '/contestedit',
          name: 'contestedit',
          component: EditContest,
          meta: { requiresAuth: true }
        },
        {
          path: '/aboutus',
          name: 'about',
          component: AboutView,
          meta: { requiresAuth: true }
        },
        {
          path: '/blog',
          name: 'blog',
          component: BlogView,
          meta: { requiresAuth: true }
        },
        {
          path: '/contactus',
          name: 'contactus',
          component: ContactUsView,
          meta: { requiresAuth: true }
        },
        {
          path: '/services',
          name: 'services',
          component: ServicesView,
          meta: { requiresAuth: true }
        },
        {
          path: '/team',
          name: 'team',
          component: TeamView,
          meta: { requiresAuth: true }
        },
        {
          path: '/register',
          name: 'register',
          component: RegisterView,
          meta: { requiresAuth: true }
        },
        {
          path: '/candidate',
          name: 'candidate',
          component: CandidateView,
          meta: { requiresAuth: true }
        },        
        {
          path: '/home',
          name: 'home',
          component: HomeView,
          meta: { requiresAuth: true }
        },
        {
          path: '/',
          name: 'home',
          component: HomeView,
          meta: { requiresAuth: true }
        },
        {
          path: '/login',
          name: 'login',
          component: LoginView,
          
        }
      ]
    
   

});

router.beforeEach((to, from, next) => {
  const isTokenAvailable = !!localStorage.getItem('token'); // Convert token presence to a boolean
  const isAuthenticated = isTokenAvailable && to.meta.requiresAuth;

  if ((to.path=="/" && !isAuthenticated)|| (to.meta.requiresAuth && !isAuthenticated)) {
    next('/login');
  } else {
    next();
  }
});
export default router
