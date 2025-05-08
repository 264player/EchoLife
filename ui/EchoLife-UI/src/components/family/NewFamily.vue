<template>
    <el-dialog v-model="status">
        <el-row>
            <el-col :span="4"></el-col>
            <el-col :span="16">
                <el-text type="info">家族名</el-text>
                <el-input v-model="family.name" size="small" /></el-col>
            <el-col :span="4"></el-col>
        </el-row>
        <el-row>
            <el-col :span="4"></el-col>
            <el-col :span="16">
                <el-button @click="CreateFamily">创建新的家族</el-button>
            </el-col>
            <el-col :span="4"></el-col>
        </el-row>
    </el-dialog>
</template>


<script setup>
import { ref, defineModel, defineProps } from 'vue';
import { ElMessage } from 'element-plus';
import { CreateFamilyAsync } from './utils/familyHelper';
import { FamilyTreeRequest } from './utils/familyDtos';

const family = ref(new FamilyTreeRequest(""))
const status = defineModel("status", { required: true })
const families = defineModel("list", { required: true })
const reload = defineProps(["reload"])

async function CreateFamily() {
    var { result, response } = await CreateFamilyAsync(family.value);
    console.log(result)
    console.log(response)
    ElMessage({
        type: result ? "success" : "error",
        message: result ? "创建成功" : "创建失败"
    })
    if (result) {
        await reload.reload()
    }
}

</script>

<style lang="css" scoped>
.el-row {
    margin-bottom: 10px;
}
</style>