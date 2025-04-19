<template>
    <el-button>新的遗嘱</el-button>
    <el-table v-infinite-scroll="GetMyWill" :data="myWills" height="250" style="width: 100%;overflow: auto;"
        :stripe="true" @row-dblclick="TableItemClick">
        <el-table-column prop="id" label="ID" width="180" />
        <el-table-column prop="name" label="名称" width="180" />
        <el-table-column prop="testaorId" label="所属人" />
        <el-table-column prop="contentId" label="内容ID" />
        <el-table-column label="操作">
            <template #default="scope">
                <el-button size="small" type="danger" @click="DeleteWill(scope.row)">
                    删除
                </el-button>
            </template>
        </el-table-column>
    </el-table>
</template>

<script setup>
import { ref } from 'vue'
import { QueryWillsRequest } from '@/utils/WillRequestDtos'
import { GetMyWillsAsync, DeleteWillAsync } from '@/utils/WillRequestHelper'
import { useRouter } from 'vue-router'

const route = useRouter()

const queryWillsRequest = ref(new QueryWillsRequest(5, null))
// const myWills = ref([{ id: "1", name: "1", testaorId: "1", contentId: "1" }, { id: 2, name: 2, testaorId: 2, contentId: 2 }, { id: 3, name: 3, testaorId: 3, contentId: 3 }])
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
    var { result, response } = await DeleteWillAsync(willResponse.id)
    console.log(result)
    console.log(response)
}
</script>

<style></style>