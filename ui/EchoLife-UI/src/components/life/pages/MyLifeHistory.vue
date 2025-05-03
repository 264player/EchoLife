<template>
    <NewLifeHistory v-model:status="newLifeHistoryStatus" v-model:list="histories"></NewLifeHistory>
    <UpdateHistory v-model:status="updateHistoryStatus" v-model:model="currentHistory"></UpdateHistory>
    <el-button @click="newLifeHistoryStatus = true">新的传记</el-button>
    <el-table v-infinite-scroll="GetMyHistories" :data="histories" height="800" style="width: 100%;overflow: auto;"
        :stripe="true" @row-dblclick="GetPointDetails">
        <el-table-column prop="id" label="ID" width="100" />
        <el-table-column prop="title" label="传记标题" width="100" />
        <el-table-column label="操作">
            <template #default="scope">
                <el-button size="small" @click="ViewDetails(scope.row)">
                    查看详情
                </el-button>
                <el-button size="small" @click="UpdatePoint(scope.row)">
                    修改
                </el-button>
                <el-button size="small" type="danger" @click="DeleteHistory(scope.row)">
                    删除
                </el-button>
            </template>
        </el-table-column>
    </el-table>
</template>

<script setup>
import { ref } from 'vue';
import { DeleteLifeHistoryAsync, GetMyLifeHistoriesAsync } from '../utils/LifeHelpers';
import { PageInfo } from '@/utils/WillRequestDtos';
import { ElMessage } from 'element-plus';
import NewLifeHistory from '../NewLifeHistory.vue';
import UpdateHistory from '../UpdateHistory.vue';
import { useRouter } from 'vue-router';

const router = useRouter()

// status
const newLifeHistoryStatus = ref(false)
const updateHistoryStatus = ref(false)
const loading = ref(false)

const histories = ref([])
const currentHistory = ref({})

const pageInfo = ref(new PageInfo(30, null))

async function GetMyHistories() {
    if (loading.value) {
        return
    }
    loading.value = true

    var { result, response } = await GetMyLifeHistoriesAsync(pageInfo.value)
    if (result) {
        console.log(response)
        if (response.length != 0) {
            pageInfo.value.cursorId = response[response.length - 1].id
            histories.value = histories.value.concat(response)
        }
    }
    loading.value = false
}

function ViewDetails(history) {
    router.push({ name: "history-details", params: { historyId: history.id } })
}

function UpdatePoint(history) {
    updateHistoryStatus.value = true
    console.debug(history)
    currentHistory.value = history
}

async function DeleteHistory(history) {
    var { result, response } = await DeleteLifeHistoryAsync(history.id)
    console.log(result)
    console.log(response)
    ElMessage({
        type: result ? "success" : "error",
        message: result ? "删除成功" : "删除失败"
    })
    if (result) {
        var index = histories.value.findIndex(v => v.id == history.id)
        if (index !== -1) {
            histories.value.splice(index, 1)
        }
    }
}
</script>

<style lang="css" scoped></style>