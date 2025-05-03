<template>
    <el-dialog v-model="newLifePoint" title="新的节点" width="800">
        <el-text>内容</el-text>
        <el-input v-model="lifePointRequest.content" type="textarea" />
        <el-switch class="mb-2" style="--el-switch-on-color: #13ce66; --el-switch-off-color: #ff4949"
            v-model="lifePointRequest.hidden" inline-prompt active-text="隐藏" inactive-text="公开" />
        <p>
            <el-button @click="CreateLifePoint">创建</el-button>
        </p>
    </el-dialog>
</template>

<script setup>
import { ref, defineModel } from 'vue';
import { LifePointRequest } from './utils/LifeDtos';
import { CreateLifePointAsync } from './utils/LifeHelpers';
import { ElMessage } from 'element-plus';

const newLifePoint = defineModel("status", { require: true })
const mylifePoints = defineModel("list", { require: true })

const lifePointRequest = ref(new LifePointRequest(null, false))

async function CreateLifePoint() {
    var { result, response } = await CreateLifePointAsync(lifePointRequest.value)
    ElMessage({
        type: result ? "success" : "error",
        message: result ? "创建成功" : "创建失败"
    })
    if (result) {
        mylifePoints.value.unshift(response)
    }
}
</script>

<style lang="css" scoped></style>