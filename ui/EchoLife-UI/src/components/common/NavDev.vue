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
              <span>用户页面</span>
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
          </el-sub-menu>
          <el-sub-menu index="2">
            <template #title>
              <el-icon>
                <location />
              </el-icon>
              <span>遗嘱页面</span>
            </template>
            <el-menu-item-group title="遗嘱">
              <el-menu-item index="2-1">
                <RouterLink :to="{ name: 'my-wills', params: { id: '1234567890' } }">我的遗嘱</RouterLink>
              </el-menu-item>
              <el-menu-item index="2-2">
                <RouterLink :to="{ name: 'new-will-version', params: { id: '1234567890' } }">新的遗嘱版本</RouterLink>
              </el-menu-item>
              <el-menu-item index="2-3">
                <RouterLink :to="{ name: 'user-info', params: { id: '1234567890' } }">遗嘱详情</RouterLink>
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
</script>
