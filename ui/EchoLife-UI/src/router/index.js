import { createRouter, createWebHistory } from 'vue-router'
import NavDev from '@/components/common/NavDev.vue'
import Login from '@/components/user/pages/Login.vue'
import Register from '@/components/user/pages/Register.vue'
import UserInfo from '@/components/user/pages/UserInfo.vue'
import MyWills from '@/components/will/pages/MyWills.vue'
import NewWillVersion from '@/components/will/pages/NewWillVersion.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '',
      name: 'home',
      component: Login,
    },
    {
      path: '/users/login',
      name: 'login',
      component: Login,
    },
    {
      path: '/users/login',
      name: 'register',
      component: Register
    },
    {
      path: '/:id/info',
      name: 'user-info',
      component: UserInfo
    },
    {
      path: '/:id/wills',
      name: 'my-wills',
      component: MyWills,
    },
    {
      path: '/:id/wills/versions',
      name: 'new-will-version',
      component:NewWillVersion
    }
  ],
})

export default router
