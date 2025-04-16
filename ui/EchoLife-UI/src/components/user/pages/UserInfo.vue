<template>
    <el-row>
        <el-col :span="4"></el-col>
        <el-col :span="16">
            <el-descriptions title="个人信息" :column="1" :border="true">
                <el-descriptions-item label="用户名">{{ userInfoResponse.Username }}</el-descriptions-item>
                <el-descriptions-item label="邮箱">18100000000</el-descriptions-item>
            </el-descriptions>
        </el-col>
        <el-col :span="4"></el-col>
    </el-row>
    <el-row>
        <el-col :span="4"></el-col>
        <el-col :span="16">
            <el-button @click="LogOut">退出登录</el-button>
            <el-button @click="Refresh">刷新登录</el-button>
        </el-col>
        <el-col :span="4"></el-col>
    </el-row>
</template>

<script setup>
import { UserInfoResponse } from '@/utils/RequestDtos';
import { GetUserInfoAsync, LogOutAsync, RefreshAsync } from '@/utils/RequestHelper';
import { onMounted, ref } from 'vue';

const userInfoResponse = ref(new UserInfoResponse(""))

onMounted(async () => {
    var { result, response } = await GetUserInfoAsync()
    if (result) {
        userInfoResponse.value.Username = response.username;
    } else {
        console.log(response)
    }
})

async function LogOut() {
    var { result, response } = await LogOutAsync()
    if (result) {
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

</script>

<style lang="css" scoped>
.el-row {
    margin-bottom: 10px;
}
</style>