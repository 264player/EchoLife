<template>
    <el-dialog v-model="newLifePoint" title="新的节点" width="800">
        <el-text>内容</el-text>
        <el-input v-model="lifePointRequest.content" type="textarea" />
        <el-switch class="mb-2" style="--el-switch-on-color: #13ce66; --el-switch-off-color: #ff4949"
            v-model="lifePointRequest.hidden" inline-prompt active-text="隐藏" inactive-text="公开" />
        <p>
            <el-button @click="CreateWillVersion">确认</el-button>
        </p>
    </el-dialog>
</template>

<script setup>
import { ref, defineModel } from 'vue';
import { LifePointRequest } from './utils/LifeDtos';

const newLifePoint = defineModel({ require: true })

const lifePointRequest = ref(new LifePointRequest(null, false))
</script>

<style lang="css" scoped></style>