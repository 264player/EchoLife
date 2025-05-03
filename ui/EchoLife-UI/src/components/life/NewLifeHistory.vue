<template>
    <el-dialog v-model="show" title="新的传记" width="800">
        <el-text>内容</el-text>
        <el-input v-model="history.title" />
        <el-button @click="CreateLifePoint">创建</el-button>
    </el-dialog>
</template>

<script setup>
import { ref, defineModel } from 'vue';
import { LifeHistoryRequest, } from './utils/LifeDtos';
import { CreateLifeHistoryAsync } from './utils/LifeHelpers';
import { ElMessage } from 'element-plus';

const show = defineModel("status", { require: true })
const histories = defineModel("list", { require: true })

const history = ref(new LifeHistoryRequest(null))

async function CreateLifePoint() {
    var { result, response } = await CreateLifeHistoryAsync(history.value)
    ElMessage({
        type: result ? "success" : "error",
        message: result ? "创建成功" : "创建失败"
    })
    if (result) {
        histories.value.unshift(response)
    }
}
</script>

<style lang="css" scoped></style>