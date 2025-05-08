<template>
    <el-row>
        <el-col :span="4"></el-col>
        <el-col :span="16">
            <el-descriptions title="个人信息" :column="1" :border="true">
                <el-descriptions-item label="用户名">{{ userInfoResponse.username }}</el-descriptions-item>
                <el-descriptions-item label="角色">
                    <el-tag v-for="item in userInfoResponse.roles" :key="item">
                        {{ item }}</el-tag>
                </el-descriptions-item>
            </el-descriptions>
        </el-col>
        <el-col :span="4"></el-col>
    </el-row>
    <el-row>
        <el-col :span="4"></el-col>
        <el-col :span="16">
            <el-button @click="LogOut">退出登录</el-button>
            <el-button @click="BecomeAReviewer">成为审核员</el-button>
        </el-col>
        <el-col :span="4"></el-col>
    </el-row>
</template>

<script setup>
import { UserInfoResponse } from '@/utils/UserRequestDtos';
import { BecomeAReviewerAsync, GetUserInfoAsync, LogOutAsync } from '@/utils/UserRequestHelper';
import { onMounted, ref } from 'vue';
import { useUserStore } from '@/stores/counter';
import { ElMessage } from 'element-plus';

const userStore = useUserStore()
const userInfoResponse = ref(new UserInfoResponse("", ""))

onMounted(async () => {
    var { result, response } = await GetUserInfoAsync()
    if (result) {
        userInfoResponse.value = response
        userInfoResponse.value.userId = response.id;
        userStore.isReviewer = IsReviewer(userInfoResponse.value.roles)
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

async function BecomeAReviewer() {
    var { result, response } = await BecomeAReviewerAsync()
    console.log(result)
    console.log(response)
    ElMessage({
        type: result ? "success" : "error",
        message: result ? "已成为审核员" : "未成为审核员"
    })
}
function IsReviewer(roles) {
    return roles.includes("Reviewer")
}

</script>

<style lang="css" scoped>
.el-row {
    margin-bottom: 10px;
}
</style>