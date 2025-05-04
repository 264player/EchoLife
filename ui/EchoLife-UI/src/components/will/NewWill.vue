<template>
    <el-dialog v-model="viewNewWill">
        <el-row>
            <el-col :span="4"></el-col>
            <el-col :span="16">
                <el-row>
                    <el-col :span="24">
                        <el-text type="=info">遗嘱名称</el-text>
                        <el-input v-model="willRequest.Name" size="large" />
                    </el-col>
                    <el-col :span="24">
                        <el-button @click="CreateWill">创建新的遗嘱</el-button>
                    </el-col>
                </el-row>
            </el-col>
            <el-col :span="4"></el-col>
        </el-row>
    </el-dialog>
</template>


<script setup>
import { WillRequest } from '@/utils/WillRequestDtos';
import { CreateWillAsync } from '@/utils/WillRequestHelper';
import { ref, defineModel } from 'vue';
import { ElMessage } from 'element-plus';

const willRequest = ref(new WillRequest(""))
const viewNewWill = defineModel("status", { required: true })
const myWills = defineModel("list", { required: true })

async function CreateWill() {
    var { result, response } = await CreateWillAsync(willRequest.value);
    console.log(result)
    console.log(response)
    ElMessage({
        type: result ? "success" : "error",
        message: result ? "创建成功" : "创建失败"
    })
    if (result) {
        // myWills.value.unshift(response)
    }
}

</script>

<style lang="css" scoped>
.el-col {
    margin-bottom: 10px;
}
</style>