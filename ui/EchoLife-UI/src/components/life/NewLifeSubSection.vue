<template>
    <el-dialog v-model="newLifePoint" title="新的小节" width="800">
        <el-text>标题</el-text>
        <el-input v-model="lifePointRequest.content" type="textarea" />
        <el-text>内容</el-text>
        <el-input v-model="lifePointRequest.content" type="textarea" />
        <p>确定新添加的位置</p>
        <el-text>属于哪个小节</el-text>
        <el-input v-model="lifePointRequest.content" type="textarea" />
        <el-text>第几小节</el-text>
        <el-input v-model="lifePointRequest.content" type="textarea" />
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