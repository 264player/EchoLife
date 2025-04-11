<template>
  <div>
    <el-row class="tac">
      <el-col :span="12">
        <h5 class="mb-2">Default colors</h5>
        <el-menu default-active="2" class="el-menu-vertical-demo" @open="handleOpen" @close="handleClose">
          <el-sub-menu index="1">
            <template #title>
              <el-icon>
                <location />
              </el-icon>
              <span>Navigator One</span>
            </template>
            <el-menu-item-group title="用户">
              <el-menu-item index="1-1">
                <RouterLink :to="{ name: 'login' }">登录</RouterLink>
              </el-menu-item>
              <el-menu-item index="1-2">
                <RouterLink :to="{ name: 'register' }">注册</RouterLink>
              </el-menu-item>
              <el-menu-item index="1-3">
                <RouterLink :to="{ name: 'user-info', params: { id: '1234567890' } }">用户信息</RouterLink>
              </el-menu-item>
            </el-menu-item-group>
            <el-sub-menu index="1-4">
              <template #title>item four</template>
              <el-menu-item index="1-4-1">item one</el-menu-item>
            </el-sub-menu>
          </el-sub-menu>
          <el-menu-item index="2">
            <el-icon><icon-menu /></el-icon>
            <span>Navigator Two</span>
          </el-menu-item>
          <el-menu-item index="3" disabled>
            <el-icon>
              <document />
            </el-icon>
            <span>Navigator Three</span>
          </el-menu-item>
          <el-menu-item index="4">
            <el-icon>
              <setting />
            </el-icon>
            <span>Navigator Four</span>
          </el-menu-item>
        </el-menu>
      </el-col>
    </el-row>
  </div>
</template>

<script setup>
import { RouterLink, RouterView } from 'vue-router'
</script>
