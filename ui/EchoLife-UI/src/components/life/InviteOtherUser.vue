<template>
    <el-dialog v-model="status" title="邀请用户" width="800">
        <el-checkbox-group v-model="usersId">
            <el-checkbox :label="item.username" :value="item.id" v-for="item in users" :key="item.id" />
        </el-checkbox-group>
        <el-button @click="Invite">邀请</el-button>
    </el-dialog>
    <el-button @click="status = true" size="small">邀请其他用户</el-button>
</template>

<script setup>
import { ref, defineModel, onMounted } from 'vue';
import { ElMessage } from 'element-plus';
import { UserInfoResponse } from '@/utils/UserRequestDtos';
import { GetAllUserInfoAsync } from '@/utils/UserRequestHelper';
import { InviteOtherUserToPointAsync } from './utils/LifeHelpers';

//status
const status = ref(false)
const pointId = defineModel('pointId', { required: true })

// model
const users = ref([])
const usersId = ref([])

onMounted(async () => {
    await GetAllUser()
})


async function GetAllUser() {
    var { result, response } = await GetAllUserInfoAsync()
    if (result) {
        users.value = response
    }
}

async function Invite() {
    var { result, response } = await InviteOtherUserToPointAsync(pointId.value, usersId.value)
    if (result) {
        ElMessage({
            type: result ? "success" : 'error'
            , message: result ? '邀请成功' : "邀请失败"
        })
    }
}
</script>

<style lang="scss" scoped></style>