<template>
    <div>
        <el-row>
            <el-col :span="4"></el-col>
            <el-col :span="16">
                <el-input v-model="requestDto.Username" style="width: 240px" placeholder="用户名" clearable />
            </el-col>
            <el-col :span="4"></el-col>
        </el-row>
        <el-row>
            <el-col :span="4"></el-col>
            <el-col :span="16">
                <el-input v-model="requestDto.Password" style="width: 240px" placeholder="密码" clearable show-password />
            </el-col>
            <el-col :span="4"></el-col>
        </el-row>

        <el-row>
            <el-col :span="4"></el-col>
            <el-col :span="16">
                <el-text type="=info" size="small">记住密码</el-text>
                <el-switch v-model="requestDto.RememberMe" size="small" />
            </el-col>
            <el-col :span="4"></el-col>
        </el-row>

        <el-row>
            <el-col :span="4"></el-col>
            <el-col :span="16">
                <el-button @click="Login">登录</el-button>
            </el-col>
            <el-col :span="4"></el-col>
        </el-row>
    </div>
</template>

<script setup>
import { ref } from 'vue'
import { LoginRequest } from '@/utils/UserRequestDtos'
import { LoginAsync } from '@/utils/UserRequestHelper'

const requestDto = ref(new LoginRequest("string", "P@ssw0rd", false))

async function Login() {
    var { result, response } = await LoginAsync(requestDto.value)
    console.log(result)
    console.log(response)
}
</script>

<style lang="css" scoped>
.el-row {
    margin-bottom: 10px;
}
</style>