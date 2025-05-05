<template>
    <el-table v-infinite-scroll="GetMyReview" :data="myReviewRequests" height="100%" style="width: 100%;overflow: auto;"
        :stripe="true">
        <el-table-column label="状态" width="180">
            <template #default="scope">
                {{ reviewStatusMap[scope.row.status] }}
            </template>
        </el-table-column>
        <el-table-column prop="reviewerId" label="审核人" />
        <el-table-column prop="createdAt" label="请求时间" width="180" />
        <el-table-column prop="reviewedAt" label="审核完成时间" width="180" />
        <el-table-column label="操作">
            <template #default="scope">
                <el-button size="small" v-if="scope.row.status == 'inProgress'" @click="Process(scope.row)">
                    处理请求
                </el-button>
                <el-button size="small" v-if="scope.row.status != 'inProgress'" @click="ReviewDetails(scope.row)">
                    查看审核
                </el-button>
            </template>
        </el-table-column>
    </el-table>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { PageInfo } from '@/utils/WillRequestDtos'
import { GetMyReviewsAsync } from '@/utils/WillRequestHelper'
import { useRouter } from 'vue-router'
import ProcessReview from '../ProcessReview.vue'
import { ElMessage } from 'element-plus'
import { reviewStatusMap } from '@/utils/WillRequestDtos'

const router = useRouter()

const pageInfo = ref(new PageInfo(5, null))

const myReviewRequests = ref([])

const loading = ref(false)

onMounted(async () => {
    await GetMyReview()
})

async function GetMyReview() {
    if (loading.value) {
        return
    }
    loading.value = true

    var { result, response } = await GetMyReviewsAsync(pageInfo.value);
    if (result) {
        console.log(response)
        if (response.length != 0) {
            pageInfo.value.cursorId = response[response.length - 1].id
            myReviewRequests.value = myReviewRequests.value.concat(response)
        }
    }

    loading.value = false
}

async function Process(review) {
    router.push({ name: "process-review", params: { reviewId: review.id } })
}

async function ReviewDetails(review) {
    router.push({ name: "review-details", params: { reviewId: review.id } })
}
</script>

<style></style>