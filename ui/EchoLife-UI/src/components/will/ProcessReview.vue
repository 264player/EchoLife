<template>
    <div>
        <el-row>
            <el-col :span="4"></el-col>
            <el-col :span="16">
                <el-text type="=info">遗嘱内容</el-text>
                <el-input v-model="willVersion.value" size="large" /></el-col>
            <el-col :span="4"></el-col>
        </el-row>
        <el-row>
            <el-col :span="4"></el-col>
            <el-col :span="16">
                <el-text type="=info">评论</el-text>
                <el-input v-model="review.comments" type="textarea" /></el-col>
            <el-col :span="4"></el-col>
        </el-row>
        <el-row>
            <el-col :span="4"></el-col>
            <el-col :span="16">
                <el-button @click="CompleteReview">确认审核</el-button>
            </el-col>
            <el-col :span="4"></el-col>
        </el-row>
    </div>
</template>


<script setup>
import { ReviewResponse, WillVersionResponse } from '@/utils/WillRequestDtos';
import { CompleteReviewAsync, GetReviewDetailsAsync } from '@/utils/WillRequestHelper';
import { ref, defineModel, onMounted, watch } from 'vue';
import { ElMessage } from 'element-plus';
import { useRoute } from 'vue-router';

const review = ref(new ReviewResponse(""))
const willVersion = ref(new WillVersionResponse())
const reviewId = ref("")

const route = useRoute()


onMounted(async () => {
    var Id = route.params.reviewId
    reviewId.value = Id
    await GetReviewDetails()
    console.debug(review.value)
})


async function CompleteReview() {
    var { result, response } = await CompleteReviewAsync(reviewId.value, { comment: review.value.comments, status: "approved" });
    console.log(result)
    console.log(response)
    ElMessage({
        type: result ? "success" : "error",
        message: result ? "审核成功" : "审核失败"
    })
}

async function GetReviewDetails() {
    var { result, response } = await GetReviewDetailsAsync(reviewId.value);
    console.log(result)
    console.log(response)
    if (result) {
        review.value = response
        willVersion.value = review.value.willVersion
    }
}
</script>

<style lang="css" scoped>
.el-row {
    margin-bottom: 10px;
}
</style>