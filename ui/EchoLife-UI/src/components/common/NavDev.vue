<template>
  <div>
    <el-row class="tac">
      <el-col :span="12">
        <el-menu default-active="1" class="el-menu-vertical-demo" @open="handleOpen" @close="handleClose" height="800">
          <el-sub-menu index="1">
            <template #title>
              <el-icon>
                <location />
              </el-icon>
              <span>遗嘱管理</span>
            </template>
            <el-menu-item index="1-1">
              <RouterLink :to="{ name: 'my-wills', params: { id: `${userStore.userInfo.userId}` } }">
                <el-link>我的遗嘱</el-link>
              </RouterLink>
            </el-menu-item>
            <el-menu-item index="1-2">
              <RouterLink :to="{ name: 'my-reviews-requests' }">我的遗嘱请求</RouterLink>
            </el-menu-item>
            <el-menu-item index="1-3" v-if="userStore.isReviewer">
              <RouterLink :to="{ name: 'all-reviews-requests' }">审核请求中心</RouterLink>
            </el-menu-item>
            <el-menu-item index="1-4" v-if="userStore.isReviewer">
              <RouterLink :to="{ name: 'my-reviews' }">待处理的请求</RouterLink>
            </el-menu-item>
          </el-sub-menu>
          <el-sub-menu index="3">
            <template #title>
              <el-icon>
                <location />
              </el-icon>
              <span>人生界面</span>
            </template>
            <el-menu-item-group title="人生节点">
              <el-menu-item index="2-1">
                <RouterLink :to="{ name: 'my-points', params: { userId: '1234567890' } }">我的人生节点</RouterLink>
              </el-menu-item>
              <el-menu-item index="2-2">
                <RouterLink :to="{ name: 'life-history' }">个人传记</RouterLink>
              </el-menu-item>
              <el-menu-item index="2-3">
                <RouterLink :to="{ name: 'my-families' }">家族树</RouterLink>
              </el-menu-item>
            </el-menu-item-group>
          </el-sub-menu>
        </el-menu>
      </el-col>
    </el-row>
  </div>
</template>

<script setup>
import { RouterLink, RouterView } from 'vue-router'
import { useUserStore } from '@/stores/counter';
const userStore = useUserStore()

</script>
