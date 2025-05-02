<template>
    <el-row>
        <el-col :span="4"></el-col>
        <el-col :span="16">
            <el-descriptions title="个人信息" :column="1" :border="true">
                <el-descriptions-item label="ID">{{ userInfoResponse.userId }}</el-descriptions-item>
                <el-descriptions-item label="用户名">{{ userInfoResponse.username }}</el-descriptions-item>
            </el-descriptions>
        </el-col>
        <el-col :span="4"></el-col>
    </el-row>
    <el-row>
        <el-col :span="4"></el-col>
        <el-col :span="16">
            <el-button @click="LogOut">退出登录</el-button>
            <el-button @click="Refresh">刷新登录</el-button>
            <el-button @click="BecomeAReviewer">成为审核员</el-button>
        </el-col>
        <el-col :span="4"></el-col>
    </el-row>
</template>

<script setup>
import { UserInfoResponse } from '@/utils/UserRequestDtos';
import { BecomeAReviewerAsync, GetUserInfoAsync, LogOutAsync, RefreshAsync } from '@/utils/UserRequestHelper';
import { onMounted, ref } from 'vue';
import { useUserStore } from '@/stores/counter';
import { ElMessage } from 'element-plus';

const userStore = useUserStore()
const userInfoResponse = ref(new UserInfoResponse("", ""))

onMounted(async () => {
    var { result, response } = await GetUserInfoAsync()
    if (result) {
        userInfoResponse.value.username = response.username;
        userInfoResponse.value.userId = response.id;
    } else {
        console.log(response)
    }
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

async function Refresh() {
    var { result, response } = await RefreshAsync()
    if (result) {
        console.log("refresh success!")
    } else {
        console.log(response)
    }
}

async function BecomeAReviewer() {
    var { result, response } = await BecomeAReviewerAsync()
    console.log(result)
    console.log(response)
    ElMessage({
        type: result ? "success" : "error",
        message: result ? "已成为审核员" : "未成为审核员"
    })
}

</script>

<style lang="css" scoped>
.el-row {
    margin-bottom: 10px;
}
</style>