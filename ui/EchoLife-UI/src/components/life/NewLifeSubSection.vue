<template>
    <el-dialog v-model="status" title="新的小节" width="800">
        <el-text>标题</el-text>
        <el-input v-model="subsection.title" />
        <p>确定新添加的位置</p>
        <el-text>属于哪个小节</el-text>
        <el-select v-model="subsection.fatherId" placeholder="Select" style="width: 240px">
            <el-option v-for="item in sections" :key="item.id" :label="item.title" :value="item.id" />
        </el-select>
        <p>
            <el-button @click="CreateLifeSubSection">确认</el-button>
        </p>
    </el-dialog>
</template>

<script setup>
import { ref, defineModel } from 'vue';
import { LifeSubsectionRequest } from './utils/LifeDtos';
import { CreateLifeSubSectionAsync } from './utils/LifeHelpers';
import { ElMessage } from 'element-plus';

const status = defineModel("status", { require: true })
const sections = defineModel("sections", { require: true })
const historyId = defineModel("historyId", { require: true })
const subsection = ref(new LifeSubsectionRequest('', ' ', '', null))

async function CreateLifeSubSection() {
    subsection.value.lifeHistoryId = historyId.value
    var { result, response } = await CreateLifeSubSectionAsync(subsection.value)
    console.log(result)
    console.log(response)
    console.log(subsection.value)
    ElMessage({
        type: result ? "success" : "error",
        message: result ? "创建成功" : "创建失败"
    })
}
</script>

<style lang="css" scoped></style>