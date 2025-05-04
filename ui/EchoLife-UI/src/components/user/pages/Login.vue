<template>
    <div>
        <el-row>
            <el-col :span="10"></el-col>
            <el-col :span="4">
                <el-row>
                    <el-col :span="24">
                        <div class="demo-image__error">
                            <div class="block">
                                <el-image />
                            </div>
                        </div>
                    </el-col>
                    <el-col :span="24">
                        <el-input v-model="requestDto.Username" style="width: 100%" placeholder="用户名" clearable />
                    </el-col>
                    <el-col :span="24">
                        <el-input v-model="requestDto.Password" style="width: 100%" placeholder="密码" clearable
                            show-password />
                    </el-col>
                    <el-col :span="24">
                        <el-text type="=info" size="small">记住密码</el-text>
                        <el-switch v-model="requestDto.RememberMe" size="small" />
                    </el-col>
                    <el-col :span="24">
                        <el-button @click="Login">登录</el-button>
                    </el-col>
                </el-row>
            </el-col>
            <el-col :span="10"></el-col>
        </el-row>
    </div>
</template>

<script setup>
import { ref } from 'vue'
import { LoginRequest } from '@/utils/UserRequestDtos'
import { LoginAsync } from '@/utils/UserRequestHelper'
import { useUserStore } from '@/stores/counter'
import { ElMessage } from 'element-plus'

const userStore = useUserStore()

const requestDto = ref(new LoginRequest("string", "P@ssw0rd", false))

async function Login() {
    var { result, response } = await LoginAsync(requestDto.value)
    console.log(result)
    console.log(response)
    ElMessage({
        type: result ? "success" : "error",
        message: result ? "登录成功" : "账号或密码错误，请重试"
    })
    if (result) {
        userStore.isLoggedIn = true
    }
}
</script>

<style lang="css" scoped>
.el-col {
    margin-bottom: 10px;
}

.demo-image__error .block {
    padding: 30px 0;
    text-align: center;
    display: inline-block;
    width: 100%;
    box-sizing: border-box;
    vertical-align: top;
}

.demo-image__error .demonstration {
    display: block;
    color: var(--el-text-color-secondary);
    font-size: 14px;
    margin-bottom: 20px;
}

.demo-image__error .el-image {
    padding: 0 5px;
    max-width: 300px;
    max-height: 200px;
    width: 100%;
    height: 200px;
}

.demo-image__error .image-slot {
    display: flex;
    justify-content: center;
    align-items: center;
    width: 100%;
    height: 100%;
    background: var(--el-fill-color-light);
    color: var(--el-text-color-secondary);
    font-size: 30px;
}

.demo-image__error .image-slot .el-icon {
    font-size: 30px;
}
</style>