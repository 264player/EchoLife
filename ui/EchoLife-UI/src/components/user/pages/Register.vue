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
                        <el-input v-model="requestDto.EnsurePassword" style="width: 100%" placeholder="确认密码" clearable
                            show-password />
                    </el-col>
                    <el-col :span="24">
                        <el-button @click="Register">注册</el-button>
                    </el-col>
                </el-row>
            </el-col>
            <el-col :span="10"></el-col>
        </el-row>
    </div>
</template>

<script setup>
import { ref } from 'vue'
import { RegisterUserRequest } from '@/utils/UserRequestDtos'
import { RegisterAsync } from '@/utils/UserRequestHelper'
import { ElMessage } from 'element-plus';
import { useRouter } from 'vue-router';
import { FormatErrorInfo } from '@/components/common/utils/errorHelper';

const router = useRouter()
const requestDto = ref(new RegisterUserRequest("string", "P@ssw0rd", "P@ssw0rd"));

async function Register() {
    var validationResult = validateModel()
    if (!validationResult) {
        return
    }

    var { result, response } = await RegisterAsync(requestDto.value);
    console.log(result)
    console.log(response)
    ElMessage({
        type: result ? "success" : "error",
        message: result ? "注册成功" : FormatErrorInfo(response)
    })
    if (result) {
        router.push({ name: "login" })
    }
}

function validateModel() {
    if (requestDto.value.Password != requestDto.value.EnsurePassword) {
        ElMessage({
            type: "error",
            message: "两次密码输入不匹配"
        })
        return false
    }

    if (requestDto.value.Password.length < 6) {
        ElMessage({
            type: "error",
            message: "密码长度最小六位"
        })
        return false
    }
    return true;
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