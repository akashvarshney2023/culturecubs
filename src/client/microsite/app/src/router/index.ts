import { createRouter, createWebHistory } from 'vue-router'
import HomeView from "@/views/main/HomeView.vue"
import ContestView from '../views/contest/ContestView.vue'
import RegistrationView from '@/views/RegisterView.vue'
const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/contest',
      name: 'contest',
      component: ContestView,
      meta: { auth: true }
    },   
    {
      path: '/:contestId/registration',
      name: 'Registration',
      component: RegistrationView,
      meta: { auth: true }
    },
    {
      path: '/',
      name: 'home',
      component: HomeView,
      meta: { auth: true }
    },

  ]
})

export default router
