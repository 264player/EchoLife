<template>
  <div class="common-layout">
    <el-container>
      <el-header>ECHO LIFE</el-header>
      <el-container>
        <el-aside width="400px">
          <NavDev></NavDev>
        </el-aside>
        <el-container>
          <el-main width="100%">
            <RouterView></RouterView>
          </el-main>
          <el-footer>Footer</el-footer>
        </el-container>
      </el-container>
    </el-container>
  </div>
</template>

<script setup>
import { RouterLink, RouterView } from 'vue-router'
import NavDev from './NavDev.vue';

</script>