<template>
    <el-button type="primary" style="margin-left: 16px" @click="drawerStatus = true">
        查看资源文件
    </el-button>
    <el-drawer v-model="drawerStatus" title="I am the title" :with-header="false">
        <input type="file" @change="HandleFileChange" />
        <button @click="UploadFile">上传文件</button>
        <el-table :data="files" height="800" style="width: 100%;overflow: auto;" :stripe="true"
            :show-overflow-tooltip="true" @row-click="CopyToBorad">
            <el-table-column label="文件路径" width="180">
                <template #default="scope">
                    {{ `http://localhost:9000/static/${scope.row}` }}
                </template>
            </el-table-column>
            <el-table-column label="操作">
                <template #default="scope">
                    <el-button size="small" type="danger" @click="DeleteObject(scope.row)">
                        删除
                    </el-button>
                </template>
            </el-table-column>
        </el-table>
    </el-drawer>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { useUserStore } from '@/stores/counter';
import { DeleteObjectAsync, ListObjectsAsync, UploadAsync } from './utils/upload';
import { ElMessage } from 'element-plus';

const userStore = useUserStore()

const drawerStatus = ref(false)

const files = ref([])

onMounted(async () => {
    await ListObjects()
})

async function ListObjects() {
    var { result, response } = await ListObjectsAsync()
    console.log(result)
    console.log(response)
    if (result) {
        files.value = response
    }
}

async function DeleteObject(fileUri) {
    await DeleteObjectAsync(fileUri)
}

// upload
function HandleFileChange(event) {
    file.value = event.target.files[0];
    console.log(file.value)
    console.log(event)
}
async function UploadFile() {
    if (!file || !file.value) {
        alert("请选择文件");
        return;
    }
    console.log(file.value)
    const uploadUrl = `http://localhost:9000/static/${userStore.userInfo.userId}/${file.value.name}`;
    // 使用 axios 上传文件
    var { result, response } = await UploadAsync(uploadUrl, file.value);
    ElMessage({
        type: result ? "success" : "error",
        message: result ? "上传成功" : "上传失败"
    })
}

async function CopyToBorad(uri) {
    await navigator.clipboard.writeText(`http://localhost:9000/static/${uri}`);
    ElMessage({
        type: 'success',
        message: "成功复制到剪贴板"
    })
}

const file = ref()

</script>