<template>
  <div>
    <el-row class="tac">
      <el-col :span="12">
        <h5 class="mb-2">Development Nav</h5>
        <el-menu default-active="2" class="el-menu-vertical-demo" @open="handleOpen" @close="handleClose" height="800">
          <el-sub-menu index="2">
            <template #title>
              <el-icon>
                <location />
              </el-icon>
              <span>遗嘱页面</span>
            </template>
            <el-menu-item-group title="遗嘱">
              <el-menu-item index="2-1">
                <RouterLink :to="{ name: 'my-wills', params: { id: `${userStore.userInfo.userId}` } }">我的遗嘱</RouterLink>
              </el-menu-item>
              <el-menu-item index="2-2">
                <RouterLink :to="{ name: 'my-reviews-requests' }">我的遗嘱请求</RouterLink>
              </el-menu-item>
              <el-menu-item index="2-2">
                <RouterLink :to="{ name: 'all-reviews-requests' }">审核请求中心</RouterLink>
              </el-menu-item>
              <el-menu-item index="2-3">
                <RouterLink :to="{ name: 'my-reviews' }">待处理的请求</RouterLink>
              </el-menu-item>
            </el-menu-item-group>
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
