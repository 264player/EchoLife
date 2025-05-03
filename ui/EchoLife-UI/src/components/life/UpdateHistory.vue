<template>
    <el-dialog v-model="show" title="修改传记标题" width="800">
        <el-input v-model="history.title" />
        <el-button @click="UpdataLifeHistory">更新</el-button>
    </el-dialog>
</template>

<script setup>
import { ref, defineModel } from 'vue';
import { LifeHistoryRequest, } from './utils/LifeDtos';
import { UpdataLifeHistoryAsync } from './utils/LifeHelpers';
import { ElMessage } from 'element-plus';

const show = defineModel("status", { require: true })

const history = defineModel("model", { require: true })

async function UpdataLifeHistory() {
    var { result, response } = await UpdataLifeHistoryAsync(history.value.id, new LifeHistoryRequest(history.value.title))
    ElMessage({
        type: result ? "success" : "error",
        message: result ? "更新成功" : "更新失败"
    })
}
</script>

<style lang="css" scoped></style>