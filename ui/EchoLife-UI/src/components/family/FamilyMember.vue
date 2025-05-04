<template>
    <el-dialog v-model="status" title="家族成员" width="800">
        <el-row>
            <el-col :span="4"></el-col>
            <el-col :span="16">
                <el-descriptions title="家族成员详情" :column="1" :border="true">
                    <el-descriptions-item label="名称">{{ member.model.displayName }}</el-descriptions-item>
                    <el-descriptions-item label="性别">{{ member.model.gender }}</el-descriptions-item>
                    <el-descriptions-item label="父亲">{{ member.model.fatherId }}</el-descriptions-item>
                    <el-descriptions-item label="母亲">{{ member.model.motherId }}</el-descriptions-item>
                    <el-descriptions-item label="伴侣">{{ member.model.spouseId }}</el-descriptions-item>
                    <el-descriptions-item label="出生日期">{{ member.model.birthDate }}</el-descriptions-item>
                    <el-descriptions-item label="死亡日期">{{ member.model.deathDate }}</el-descriptions-item>
                    <el-descriptions-item label="世代">{{ member.model.generation }}</el-descriptions-item>
                    <el-descriptions-item label="权限等级">{{ member.model.powerLevel }}</el-descriptions-item>
                </el-descriptions>
            </el-col>
            <el-col :span="4"></el-col>
        </el-row>
    </el-dialog>
</template>

<script setup>
import { ReviewResponse, WillVersionResponse } from '@/utils/WillRequestDtos';
import { GetReviewDetailsAsync } from '@/utils/WillRequestHelper';
import { onMounted, ref, defineModel, defineProps } from 'vue';
import { useRoute } from 'vue-router';

// status 
const status = defineModel('status', { required: true })

// model
const member = defineProps(['model'])

const reviewId = ref(null)
const review = ref(new ReviewResponse(null, null, null, null, null, "", null))

const route = useRoute()

onMounted(() => {
    console.log(member)
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

</script>

<style lang="css" scoped>
.el-row {
    margin-bottom: 10px;
}
</style>