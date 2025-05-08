<template>
    <NewFamily v-model:status="newFamilyStatus" v-model:list="myFamilies" :reload="ReloadFamily"></NewFamily>
    <el-button @click="newFamilyStatus = true">新的家族</el-button>
    <el-table v-infinite-scroll="GetMyFamily" :data="myFamilies" height="800px" style="width: 100%;overflow: auto;"
        :stripe="true" @row-dblclick="TableItemClick">
        <el-table-column prop="name" label="名称" width="180" />
        <el-table-column prop="createdAt" label="创建日期">
            <template #default="scope">
                {{ ConvertUTCToBeijingTime(scope.row.createdAt) }}
            </template>
        </el-table-column>
        <el-table-column label="操作">
            <template #default="scope">
                <el-button size="small" @click="TableItemClick(scope.row)">
                    查看详情
                </el-button>
                <el-button size="small" @click="GotoFamilyHistory(scope.row.id)">
                    查看家族传记
                </el-button>
                <el-button size="small" type="danger" @click="DeleteFamily(scope.row)">
                    删除
                </el-button>
            </template>
        </el-table-column>
    </el-table>
</template>

<script setup>
import { ref } from 'vue'
import { PageInfo } from '@/utils/WillRequestDtos'
import { useRouter } from 'vue-router'
import { ElMessage } from 'element-plus'
import NewFamily from '../NewFamily.vue'
import { DeleteFamilyAsync, GetMyFamiliesAsync } from '../utils/familyHelper'
import { ConvertUTCToBeijingTime } from '@/components/common/utils/ConvertTime'

const route = useRouter()

// status
const newFamilyStatus = ref(false)
const loading = ref(false)



//model
const myFamilies = ref([])
const pageInfo = ref(new PageInfo(30, null))


async function GetMyFamily() {
    if (loading.value) {
        return
    }
    loading.value = true

    var { result, response } = await GetMyFamiliesAsync(pageInfo.value);
    if (result) {
        console.log(response)
        if (response.length != 0) {
            pageInfo.value.cursorId = response[response.length - 1].id
            myFamilies.value = myFamilies.value.concat(response)
        }
    }

    loading.value = false
}

function TableItemClick(row) {
    console.debug(row.id)
    route.push({ name: "family-details", params: { familyId: row.id } })
}

async function DeleteFamily(family) {
    var { result, response } = await DeleteFamilyAsync(family.id)
    console.log(result)
    console.log(response)
    ElMessage({
        type: result ? "success" : "error",
        message: result ? "删除成功" : "删除失败"
    })
    if (result) {
        var index = myFamilies.value.findIndex(v => v.id == family.id)
        if (index !== -1) {
            myFamilies.value.splice(index, 1)
        }
    }
}

function GotoFamilyHistory(familyId) {
    route.push({ name: 'family-history', params: { familyId: familyId } })
}

async function ReloadFamily() {
    myFamilies.value = [];
    pageInfo.value.cursorId = null;
    await GetMyFamily()
    console.log("重新加载数据")
}


</script>

<style></style>