<template>
    <div>
        <el-input v-model="username" style="width: 240px" placeholder="Please input" clearable />
        <el-input v-model="password" style="width: 240px" placeholder="Please input" clearable show-password />
        <el-button @click="LoginAsync">登录</el-button>
    </div>
</template>

<script setup>
import { ref } from 'vue'
const username = ref('')
const password = ref('')

function LoginAsync() {
    console.log(username.value)
    console.log(password.value)
}
</script>

<style lang="css" scoped></style>