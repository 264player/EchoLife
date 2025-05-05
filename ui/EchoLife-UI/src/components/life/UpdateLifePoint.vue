<template>
    <el-dialog v-model="updateLifePoint" title="修改内容" width="800">
        <el-text>内容</el-text>
        <MdEditor v-model="lifePointRequest.content"></MdEditor>
        <el-switch class="mb-2" style="--el-switch-on-color: #ff4949; --el-switch-off-color: #13ce66"
            v-model="lifePointRequest.hidden" inline-prompt active-text="隐藏" inactive-text="公开" />
        <p>
            <el-button @click="UpdateLifePoint">确认修改</el-button>
        </p>
    </el-dialog>
</template>

<script setup>
import { ref, defineModel } from 'vue';
import { LifePointRequest, LifePointResponse } from './utils/LifeDtos';
import { UpdataLifePointAsync } from './utils/LifeHelpers';
import { ElMessage } from 'element-plus';
import { MdEditor } from 'md-editor-v3';
import 'md-editor-v3/lib/preview.css';

const updateLifePoint = defineModel("status", { required: true })

const lifePointRequest = defineModel("point", { required: true })

async function UpdateLifePoint() {
    var { result, response } = await UpdataLifePointAsync(lifePointRequest.value.id, new LifePointRequest(lifePointRequest.value.content, lifePointRequest.value.hidden))
    console.debug(result)
    console.debug(response)
    ElMessage({
        type: result ? "success" : "error",
        message: result ? "修改成功" : "修改失败"
    })
}
</script>

<style lang="css" scoped></style>