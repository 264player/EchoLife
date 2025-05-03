import { createRouter, createWebHistory } from 'vue-router'
import Login from '@/components/user/pages/Login.vue'
import Register from '@/components/user/pages/Register.vue'
import UserInfo from '@/components/user/pages/UserInfo.vue'
import MyWills from '@/components/will/pages/MyWills.vue'
import WillDetails from '@/components/will/pages/WillDetails.vue'
import UpdateLifePoint from '@/components/life/UpdateLifePoint.vue'
import MyLifePoints from '@/components/life/pages/MyLifePoints.vue'
import MyLifeHistory from '@/components/life/pages/MyLifeHistory.vue'
import MyReviewRequests from '@/components/will/pages/MyReviewRequests.vue'
import MyWillReviews from '@/components/will/pages/MyWillReviews.vue'
import AllReviewRequests from '@/components/will/pages/AllReviewRequests.vue'
import WillReviewDetails from '@/components/will/pages/WillReviewDetails.vue'
import ProcessReview from '@/components/will/ProcessReview.vue'
import HistoryDetails from '@/components/life/pages/HistoryDetails.vue'

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
      path: '/wills/:willId/will-details',
      name: 'will-details',
      component: WillDetails,
    },
    {
      path: '/reviewer/reviews/requests',
      name: 'my-reviews-requests',
      component: MyReviewRequests,
    },
    {
      path: '/user/reviews',
      name: 'my-reviews',
      component: MyWillReviews,
    },
    {
      path: '/reviewer/reviews/all-requests',
      name: 'all-reviews-requests',
      component: AllReviewRequests,
    },
    {
      path: '/reviews/:reviewId',
      name: 'review-details',
      component: WillReviewDetails,
    },
    {
      path: '/reviews/:reviewId/process',
      name: 'process-review',
      component: ProcessReview,
    },
    {
      path: '/life/history',
      name: 'life-history',
      component: MyLifeHistory,
    },
    {
      path: '/life/history/:historyId',
      name: 'history-details',
      component: HistoryDetails,
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
