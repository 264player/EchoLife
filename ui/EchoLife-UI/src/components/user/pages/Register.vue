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
                <el-input v-model="requestDto.EnsurePassword" style="width: 240px" placeholder="确认密码" clearable
                    show-password />
            </el-col>
            <el-col :span="4"></el-col>
        </el-row>
        <el-row>
            <el-col :span="4"></el-col>
            <el-col :span="16">
                <el-button @click="Register">注册</el-button>
            </el-col>
            <el-col :span="4"></el-col>
        </el-row>
    </div>
</template>

<script setup>
import { ref } from 'vue'
import { RegisterUserRequest } from '@/utils/UserRequestDtos'
import { RegisterAsync } from '@/utils/UserRequestHelper'

const requestDto = ref(new RegisterUserRequest("string", "P@ssw0rd", "P@ssw0rd"));

async function Register() {
    var { result, response } = await RegisterAsync(requestDto.value);
    console.log(result)
    console.log(response)
}
</script>

<style lang="css" scoped>
.el-row {
    margin-bottom: 10px;
}
</style>