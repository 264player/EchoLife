import { createRouter, createWebHistory } from 'vue-router'
import NavDev from '@/components/common/NavDev.vue'
import Login from '@/components/user/pages/Login.vue'
import Register from '@/components/user/pages/Register.vue'
import UserInfo from '@/components/user/pages/UserInfo.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/users/login',
      name: 'login',
      component: Login,
    }, {
      path: '/users/login',
      name: 'register',
      component: Register
    }, {
      path: '/users/:id/info',
      name: 'user-info',
      component: UserInfo
    },
    {
      path: '/about',
      name: 'about',
      // route level code-splitting
      // this generates a separate chunk (About.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import('../views/AboutView.vue'),
    },
  ],
})

export default router
