<template>
    <el-table v-infinite-scroll="GetMyReviewRequests" :data="myReviewRequests" height="100%"
        style="width: 100%;overflow: auto;" :stripe="true" @row-dblclick="TableItemClick">
        <el-table-column prop="status" label="状态" width="180">
            <template #default="scope">
                {{ reviewStatusMap[scope.row.status] }}
            </template>
        </el-table-column>
        <el-table-column label="请求时间" width="180">
            <template #default="scope">
                {{ ConvertUTCToBeijingTime(scope.row.createdAt) }}
            </template>
        </el-table-column>
        <el-table-column label="操作">
            <template #default="scope">
                <el-button size="small" @click="ProcessReview(scope.row)">
                    接受处理
                </el-button>
            </template>
        </el-table-column>
    </el-table>
</template>

<script setup>
import { ref } from 'vue'
import { PageInfo } from '@/utils/WillRequestDtos'
import { GetAllReviewRequestAsync, ProcessReviewAsync } from '@/utils/WillRequestHelper'
import { useRouter } from 'vue-router'
import { ElMessage } from 'element-plus'
import { ConvertUTCToBeijingTime } from '@/components/common/utils/ConvertTime'
import { reviewStatusMap } from '@/utils/WillRequestDtos'

const route = useRouter()

const pageInfo = ref(new PageInfo(5, null))

const myReviewRequests = ref([])

const loading = ref(false)

async function GetMyReviewRequests() {
    if (loading.value) {
        return
    }
    loading.value = true

    var { result, response } = await GetAllReviewRequestAsync(pageInfo.value);
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

async function ProcessReview(review) {
    var { result, response } = await ProcessReviewAsync(review.id)
    console.log(result)
    console.log(response)
    ElMessage({
        type: result ? "success" : "error",
        message: result ? "处理成功" : "处理失败"
    })
    if (result) {
        var index = myReviewRequests.value.findIndex(v => v.id == review.id)
        if (index !== -1) {
            myReviewRequests.value.splice(index, 1)
        }
    }
}
</script>

<style></style>