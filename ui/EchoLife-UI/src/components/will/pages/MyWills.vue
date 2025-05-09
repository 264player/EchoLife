<template>
    <NewWill v-model:status="viewNewWill" :reload="Reload"></NewWill>
    <el-button @click="viewNewWill = true">新的遗嘱</el-button>
    <el-table v-infinite-scroll="GetMyWill" :data="myWills" height="800px" style="width: 100%;overflow: auto;"
        :stripe="true" @row-dblclick="TableItemClick">
        <el-table-column prop="name" label="名称" width="180" />
        <el-table-column label="遗嘱类型">
            <template #default="scope">
                {{ willTypeMap[scope.row.willType] }}
            </template>
        </el-table-column>
        <el-table-column label="操作">
            <template #default="scope">
                <el-button @click="TableItemClick(scope.row)">
                    查看详情
                </el-button>
                <el-button type="danger" @click="DeleteWill(scope.row)">
                    删除
                </el-button>
            </template>
        </el-table-column>
    </el-table>
</template>

<script setup>
import { computed, ref } from 'vue'
import { QueryWillsRequest } from '@/utils/WillRequestDtos'
import { GetMyWillsAsync, DeleteWillAsync } from '@/utils/WillRequestHelper'
import NewWill from '../NewWill.vue'
import { useRouter } from 'vue-router'
import { ElMessage, ElMessageBox } from 'element-plus'
import { willTypeMap } from '@/utils/WillRequestDtos'

const route = useRouter()
const viewNewWill = ref(false)

//computed


const queryWillsRequest = ref(new QueryWillsRequest(30, null))

const myWills = ref([])

const loading = ref(false)

async function GetMyWill() {
    if (loading.value) {
        return
    }
    loading.value = true

    var { result, response } = await GetMyWillsAsync(queryWillsRequest.value);
    if (result) {
        console.log(response)
        if (response.length != 0) {
            queryWillsRequest.value.CusorId = response[response.length - 1].id
            myWills.value = myWills.value.concat(response)
        }
    }

    loading.value = false
}

function TableItemClick(row) {
    console.debug(row.id)
    route.push({ name: "will-details", params: { willId: row.id } })
}

async function DeleteWill(willResponse) {
    if (!await ConfirmDelete()) {
        return
    }
    var { result, response } = await DeleteWillAsync(willResponse.id)
    console.log(result)
    console.log(response)
    ElMessage({
        type: result ? "success" : "error",
        message: result ? "删除成功" : "删除失败"
    })
    if (result) {
        var index = myWills.value.findIndex(v => v.id == willResponse.id)
        if (index !== -1) {
            myWills.value.splice(index, 1)
        }
    }
}

async function ConfirmDelete() {
    var result = false
    await ElMessageBox.confirm(
        '是否确认删除?',
        {
            confirmButtonText: '确认',
            cancelButtonText: '取消',
            type: 'warning',
        }
    )
        .then(() => {
            result = true
        })
        .catch(() => {
            result = false
        })
    console.log(result)
    return result
}

async function Reload() {
    queryWillsRequest.value.CusorId = null
    myWills.value = []
    await GetMyWill()
}
</script>

<style></style>