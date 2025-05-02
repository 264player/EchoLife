<template>
    <el-row>
        <el-col :span="4"></el-col>
        <el-col :span="16">
            <el-descriptions title="遗嘱审核" :column="1" :border="true">
                <el-descriptions-item label="ID">{{ review.id }}</el-descriptions-item>
                <el-descriptions-item label="状态">{{ review.status }}</el-descriptions-item>
                <el-descriptions-item label="遗嘱ID">{{ review.willVersion.id }}</el-descriptions-item>
                <el-descriptions-item label="遗嘱类型">{{ review.willVersion.willType }}</el-descriptions-item>
                <el-descriptions-item label="遗嘱内容">{{ review.willVersion.value }}</el-descriptions-item>
                <el-descriptions-item label="评论">{{ review.comments }}</el-descriptions-item>
            </el-descriptions>
        </el-col>
        <el-col :span="4"></el-col>
    </el-row>
</template>

<script setup>
import { ReviewResponse } from '@/utils/WillRequestDtos';
import { GetReviewDetailsAsync } from '@/utils/WillRequestHelper';
import { onMounted, ref } from 'vue';
import { useRoute } from 'vue-router';


const reviewId = ref(null)
const review = ref(new ReviewResponse(null, null, null, null, null, null, ""))

const route = useRoute()

onMounted(async () => {

    reviewId.value = route.params.reviewId

    await Promise.all([GetReview()])
})

async function GetReview() {
    var { result, response } = await GetReviewDetailsAsync(reviewId.value);
    console.log(result)
    console.log(response)
    if (result) {
        review.value = response
    }
}

</script>

<style lang="css" scoped>
.el-row {
    margin-bottom: 10px;
}
</style>