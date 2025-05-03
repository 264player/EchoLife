<template>
    <el-dialog v-model="status">
        <el-row>
            <el-col :span="4"></el-col>
            <el-col :span="16">
                <el-text type="info">成员名称</el-text>
                <el-input v-model="member.DisplayName" size="small" /></el-col>
            <el-col :span="4"></el-col>
        </el-row>
        <el-row>
            <el-col :span="4"></el-col>
            <el-col :span="16">
                <el-button @click="CreateMember">创建新成员</el-button>
            </el-col>
            <el-col :span="4"></el-col>
        </el-row>
    </el-dialog>
</template>


<script setup>
import { ref, defineModel } from 'vue';
import { ElMessage } from 'element-plus';
import { CreateMemberAsync } from './utils/familyHelper';
import { FamilyMemberRequest } from './utils/familyDtos';

const member = ref(new FamilyMemberRequest(""))
const status = defineModel("status", { required: true })
const members = defineModel("list", { required: true })

async function CreateMember() {
    var { result, response } = await CreateMemberAsync(member.value);
    console.log(result)
    console.log(response)
    ElMessage({
        type: result ? "success" : "error",
        message: result ? "创建成功" : "创建失败"
    })
    if (result) {
        members.value.unshift(response)
    }
}

</script>

<style lang="css" scoped>
.el-row {
    margin-bottom: 10px;
}
</style>