<template>
    <div>
        <el-input v-model="Username" style="width: 240px" placeholder="Please input" clearable />
        <el-input v-model="Password" style="width: 240px" placeholder="Please input" clearable show-password />
        <el-input v-model="EnsurePassword" style="width: 240px" placeholder="Please input agin" clearable
            show-password />
        <el-button @click="LoginAsync">注册</el-button>
    </div>
</template>

<script setup>
import { ref } from 'vue'
const Username = ref('')
const Password = ref('')
const EnsurePassword = ref('')

function LoginAsync() {
    console.log(Username.value)
    console.log(Password.value)
    console.log(EnsurePassword.value)
}
</script>

<style lang="css" scoped></style>