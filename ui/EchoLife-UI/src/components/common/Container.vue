<template>
  <div class="common-layout">
    <el-container>
      <el-header>
        <Header></Header>
      </el-header>
      <el-container>
        <el-aside width="400px">
          <NavDev></NavDev>
        </el-aside>
        <el-container>
          <el-main width="100%">
            <RouterView></RouterView>
          </el-main>
          <el-footer><el-empty description="footer"></el-empty></el-footer>
        </el-container>
      </el-container>
    </el-container>
  </div>
</template>

<script setup>
import Header from './Header.vue';
import NavDev from './NavDev.vue';
import { useRouter, useRoute } from 'vue-router';
import { useUserStore } from '@/stores/counter';
import { watch, onMounted } from 'vue';
import { GetUserInfoAsync } from '@/utils/UserRequestHelper';

const router = useRouter()
const routeInfo = useRoute()
const userStore = useUserStore()

onMounted(() => {
  GetUserInfo()
})

watch(() => routeInfo.name, (newName) => {
  GotoLogin(newName)
})

watch(() => userStore.isLoggedIn, async (status) => {
  if (!status) {
    router.push({ name: "login" })
  } else {
    await GetUserInfo()
    router.push({ name: "user-info", params: { id: `${userStore.userInfo.userId}` } })
  }
})

async function GetUserInfo() {
  var { result, response } = await GetUserInfoAsync()
  if (result) {
    userStore.userInfo.username = response.username;
    userStore.userInfo.userId = response.id;
    userStore.isLoggedIn = true
  } else {
    console.log(response)
  }
}

function GotoLogin(newName) {
  if (newName == "register" || newName == "login") {
    if (userStore.isLoggedIn) {
      router.push({ name: "user-info", params: { id: `${userStore.userInfo.userId}` } })
    }
    return
  }

  if (userStore.isLoggedIn) {
    return
  }

  router.push({ name: "login" })
}
</script>