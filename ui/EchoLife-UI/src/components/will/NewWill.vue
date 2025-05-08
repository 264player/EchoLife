<template>
    <el-dialog v-model="showStatus">
        <el-row>
            <el-col :span="4"></el-col>
            <el-col :span="16">
                <el-row>
                    <el-col :span="24">
                        <el-text type="info">遗嘱名称</el-text>
                        <el-input v-model="willRequest.name" size="large" clearable />
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
import { ref, defineModel, defineProps } from 'vue';
import { ElMessage } from 'element-plus';

const willRequest = ref(new WillRequest(""))
const showStatus = defineModel("status", { required: true })
const reload = defineProps(['reload'])

async function CreateWill() {
    if (!WillRequestValidation(willRequest.value)) {
        return
    }

    var { result, response } = await CreateWillAsync(willRequest.value);
    console.log(result)
    console.log(response)
    ElMessage({
        type: result ? "success" : "error",
        message: result ? "创建成功" : "创建失败"
    })
    if (result) {
        willRequest.value = new WillRequest("")
        showStatus.value = false
        reload.reload()
    }
}

/**
 * 
 * @param {WillRequest} willRequest 
 */
function WillRequestValidation(willRequest) {
    if (willRequest.name == null || willRequest.name.length <= 0) {
        ElMessage({
            type: "warning",
            message: "遗嘱名不可为空"
        })
        return false;
    }

    if (willRequest.name.length > 100) {
        ElMessage({
            type: "warning",
            message: "遗嘱名长度超出上限"
        })
        return false;
    }

    return true;
}

</script>

<style lang="css" scoped>
.el-col {
    margin-bottom: 10px;
}
</style>