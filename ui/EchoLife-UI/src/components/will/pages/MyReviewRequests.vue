<template>
    <el-table v-infinite-scroll="GetMyReviewRequests" :data="myReviewRequests" height="100%"
        style="width: 100%;overflow: auto;" :stripe="true" @row-dblclick="TableItemClick">
        <el-table-column prop="status" label="名称" width="180" />
        <el-table-column prop="reviewerId" label="审核人" />
        <el-table-column prop="createdAt" label="请求时间" width="180" />
        <el-table-column prop="reviewedAt" label="审核完成时间" width="180" />
        <el-table-column label="操作">
            <template #default="scope">
                <el-button size="small" type="danger" @click="CancelRequest(scope.row)"
                    :disabled="scope.row.status != 'pending'">
                    取消请求
                </el-button>
            </template>
        </el-table-column>
    </el-table>
</template>

<script setup>
import { ref } from 'vue'
import { PageInfo } from '@/utils/WillRequestDtos'
import { GetMyReviewRequestsAsync, CancelReviewAsync } from '@/utils/WillRequestHelper'
import { useRouter } from 'vue-router'
import { ElMessage } from 'element-plus'

const route = useRouter()

const pageInfo = ref(new PageInfo(5, null))

const myReviewRequests = ref([])

const loading = ref(false)

async function GetMyReviewRequests() {
    if (loading.value) {
        return
    }
    loading.value = true

    var { result, response } = await GetMyReviewRequestsAsync(pageInfo.value);
    if (result) {
        console.log(response)
        if (response.length != 0) {
            pageInfo.value.cursorId = response[response.length - 1].id
            myReviewRequests.value = myReviewRequests.value.concat(response)
        }
    }

    loading.value = false
}

function TableItemClick(row) {
    console.debug(row.id)
    route.push({ name: "review-details", params: { reviewId: row.id } })
}

async function CancelRequest(review) {
    var { result, response } = await CancelReviewAsync(review.id)
    console.log(result)
    console.log(response)
    ElMessage({
        type: result ? "success" : "error",
        message: result ? "取消成功" : "取消失败"
    })
}
</script>

<style></style>