<template>
    <el-dialog v-model="newLifePoint" title="新的节点" width="800">
        <el-text>内容</el-text>
        <MdEditor v-model="lifePointRequest.content"></MdEditor>
        <el-switch class="mb-2" style="--el-switch-on-color: #ff4949; --el-switch-off-color: #13ce66"
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
import { MdEditor } from 'md-editor-v3';
import 'md-editor-v3/lib/preview.css';

const newLifePoint = defineModel("status", { require: true })
const mylifePoints = defineModel("list", { require: true })

const lifePointRequest = ref(new LifePointRequest("", false))

async function CreateLifePoint() {
    var { result, response } = await CreateLifePointAsync(lifePointRequest.value)
    ElMessage({
        type: result ? "success" : "error",
        message: result ? "创建成功" : "创建失败"
    })
    if (result) {
        // mylifePoints.value = []
    }
}
</script>

<style lang="css" scoped></style>