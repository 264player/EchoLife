<template>

    <el-row>
        <el-col :span="4">
            ECOH LIFE
        </el-col>
        <el-col :span="4" :offset="16">
            <div v-if="!logged">
                <RouterLink :to="{ name: 'login' }">
                    <el-link>
                        登录
                    </el-link>
                </RouterLink>
                <el-divider direction="vertical" />
                <RouterLink :to="{ name: 'register' }">
                    <el-link>
                        注册
                    </el-link>
                </RouterLink>
            </div>
            <div v-else>
                <RouterLink :to="{ name: 'user-info', params: { id: `${userStore.userInfo.userId}` } }">
                    <el-link>
                        {{ userStore.userInfo.username }}
                    </el-link>
                </RouterLink>
                <el-divider direction="vertical" />
                <el-link @click="LogOut">
                    退出登录
                </el-link>
            </div>
        </el-col>
    </el-row>
    <el-divider />
</template>

<script setup>
import { RouterLink } from 'vue-router';
import { watch, ref } from 'vue';
import { useUserStore } from '@/stores/counter';
import { LogOutAsync } from '@/utils/UserRequestHelper';

const userStore = useUserStore()
const logged = ref(false)

watch(userStore, () => {
    logged.value = userStore.isLoggedIn
})

async function LogOut() {
    var { result, response } = await LogOutAsync()
    if (result) {
        userStore.isLoggedIn = false
        console.log("logout success!")
    } else {
        console.log(response)
    }
}
</script>

<style lang="scss" scoped></style>