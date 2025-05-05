<template>
    <el-row>
        <el-col :span="4"></el-col>
        <el-col :span="16">
            <el-descriptions title="遗嘱审核详情" :column="1" :border="true">
                <el-descriptions-item label="状态">{{ reviewStatusMap[review.status]
                }}</el-descriptions-item>
                <el-descriptions-item label="遗嘱ID">{{ willVersion.id }}</el-descriptions-item>
                <el-descriptions-item label="遗嘱类型">{{ willTypeMap[willVersion.willType] }}</el-descriptions-item>
                <el-descriptions-item label="审核人">{{ reviewer.username }}</el-descriptions-item>
                <el-descriptions-item label="请求时间">{{ ConvertUTCToBeijingTime(review.createdAt)
                    }}</el-descriptions-item>
                <el-descriptions-item label="审核时间">{{ ConvertUTCToBeijingTime(review.reviewedAt)
                }}</el-descriptions-item>
            </el-descriptions>
            <el-divider>遗嘱内容</el-divider>
            <MdPreview :modelValue="willVersion.value"></MdPreview>
            <!-- <MdCatalog :editorId="id" :scrollElement="html"></MdCatalog> -->
            <el-divider>评论</el-divider>
            <MdPreview :modelValue="review.comments"></MdPreview>
            <!-- <MdCatalog :editorId="id" :scrollElement="html"></MdCatalog> -->
        </el-col>
        <el-col :span="4"></el-col>
    </el-row>
</template>

<script setup>
import { ReviewResponse, WillVersionResponse } from '@/utils/WillRequestDtos';
import { GetReviewDetailsAsync } from '@/utils/WillRequestHelper';
import { onMounted, ref } from 'vue';
import { useRoute } from 'vue-router';
import { MdPreview } from 'md-editor-v3';
import { willTypeMap, reviewStatusMap } from '@/utils/WillRequestDtos';
import { ConvertUTCToBeijingTime } from '@/components/common/utils/ConvertTime';
import { GetOtherUserInfoAsync } from '@/utils/UserRequestHelper';
import { UserInfoResponse } from '@/utils/UserRequestDtos';


const reviewId = ref(null)
const review = ref(new ReviewResponse('', '', '', '', '', "", ''))
const willVersion = ref(new WillVersionResponse('', '', '', '', '', ''))
const reviewer = ref(new UserInfoResponse('', '', []))

const route = useRoute()

onMounted(async () => {

    reviewId.value = route.params.reviewId

    await Promise.all([GetReview()])
    await GetReviewer(review.value.reviewerId)
})

async function GetReview() {
    var { result, response } = await GetReviewDetailsAsync(reviewId.value);
    console.log(result)
    console.log(response)
    if (result) {
        review.value = response
        willVersion.value = review.value.willVersion
    }
}

async function GetReviewer(userId) {
    if (userId == null || userId == '') {
        return "Unknown"
    }
    var { result, response } = await GetOtherUserInfoAsync(userId)
    if (result) {
        reviewer.value = response
    }

    return "Unknown"
}

</script>

<style lang="css" scoped>
.el-row {
    margin-bottom: 10px;
}
</style>