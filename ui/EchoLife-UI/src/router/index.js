import { createRouter, createWebHistory } from 'vue-router'
import NavDev from '@/components/common/NavDev.vue'
import Login from '@/components/user/pages/Login.vue'
import Register from '@/components/user/pages/Register.vue'
import UserInfo from '@/components/user/pages/UserInfo.vue'
import MyWills from '@/components/will/pages/MyWills.vue'
import NewWill from '@/components/will/pages/NewWill.vue'
import WillDetails from '@/components/will/pages/WillDetails.vue'
import NewLifePoint from '@/components/life/NewLifePoint.vue'
import UpdateLifePoint from '@/components/life/UpdateLifePoint.vue'
import MyLifePoints from '@/components/life/pages/MyLifePoints.vue'
import MyLifeHistory from '@/components/life/pages/MyLifeHistory.vue'

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
      component: Register,
    },
    {
      path: '/:id/info',
      name: 'user-info',
      component: UserInfo,
    },
    {
      path: '/:id/wills',
      name: 'my-wills',
      component: MyWills,
    },
    {
      path: '/:id/wills/new-will',
      name: 'new-will',
      component: NewWill,
    },
    {
      path: '/wills/:willId/will-details',
      name: 'will-details',
      component: WillDetails,
    },
    {
      path: '/life/history',
      name: 'life-history',
      component: MyLifeHistory,
    },
    {
      path: '/life/points/:pointId',
      name: 'update-point',
      component: UpdateLifePoint,
    },
    {
      path: '/:userId/life/points',
      name: 'my-points',
      component: MyLifePoints,
    },
  ],
})

export default router
