<template>
    <el-row>
        <el-col :span="4"></el-col>
        <el-col :span="16">
            <el-text type="=info">遗嘱名</el-text>
            <el-input v-model="willRequest.Name" size="small" /></el-col>
        <el-col :span="4"></el-col>
    </el-row>
    <el-row>
        <el-col :span="4"></el-col>
        <el-col :span="16">
            <el-button @click="CreateWill">创建新的遗嘱</el-button>
        </el-col>
        <el-col :span="4"></el-col>
    </el-row>
</template>

<script setup>
import { WillRequest } from '@/utils/WillRequestDtos';
import { CreateWillAsync } from '@/utils/WillRequestHelper';
import { ref } from 'vue';

const willRequest = ref(new WillRequest(""))

async function CreateWill() {
    var { result, response } = await CreateWillAsync(willRequest.value);
    console.log(result)
    console.log(response)
}

</script>

<style lang="css" scoped>
.el-row {
    margin-bottom: 10px;
}
</style>