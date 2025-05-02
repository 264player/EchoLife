import { ref, computed } from 'vue'
import { defineStore } from 'pinia'
import { UserInfoResponse } from '@/utils/UserRequestDtos'

export const useUserStore = defineStore('user', () => {
  const userInfo = ref(new UserInfoResponse(null, null))
  const isLoggedIn = ref(false)

  return {
    userInfo,
    isLoggedIn,
  }
})
