<template>
    <el-dialog v-model="showStatus" title="新的版本" width="800">
        <p><el-text>遗嘱类型</el-text></p>
        <p>
            <el-select v-model="willVersionRequest.willType" placeholder="Select" style="width: 240px">
                <el-option v-for="item in willTypeArray" :key="item.value" :label="item.name" :value="item.value"
                    :disabled="!item.supported" style="border-bottom:0px;" class="custom-option" />
            </el-select>
        </p>
        <el-text>内容</el-text>
        <MdEditor v-model="willVersionRequest.value" v-if="!needFile"></MdEditor>
        <el-button @click="CreateWillVersion">确认</el-button>
    </el-dialog>
</template>

<script setup>
import { CreateWillVersionAsync } from '@/utils/WillRequestHelper'
import { ElMessage } from 'element-plus'
import { defineModel, defineProps, ref, onMounted, computed } from 'vue'
import { WillVersionRequest } from '@/utils/WillRequestDtos'
import { willTypeArray } from '@/utils/WillRequestDtos';
import { MdEditor } from 'md-editor-v3';

const showStatus = defineModel('status', { required: true })
const props = defineProps(['reload', 'currentType', 'currentContent', 'willId'])

//model
const willVersionRequest = ref(new WillVersionRequest(props.currentType, props.currentContent))

const needFile = computed(() =>
    willVersionRequest.value.willType.toLowerCase() == "audio" || willVersionRequest.value.willType.toLowerCase() == "video"
)

onMounted(() => {
    console.log(props)
    willVersionRequest.value = new WillVersionRequest(props.currentType, props.currentContent)
})

async function CreateWillVersion() {
    var { result, response } = await CreateWillVersionAsync(props.willId, willVersionRequest.value, false)
    if (result) {
        ElMessage({
            type: "success",
            message: "创建成功"
        })
        await props.reload()
    }
}

</script>

<style lang="css" scoped>
.custom-option {
    display: flex;
    align-items: center;
    justify-content: center;
    height: 40px;
}
</style>