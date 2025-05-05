<template>
    <el-dialog v-model="status" title="家族成员" width="800">
        <el-row>
            <el-col :span="4"></el-col>
            <el-col :span="16">
                <el-descriptions title="家族成员详情" :column="1" :border="true">
                    <el-descriptions-item label="名称">{{ member.model.displayName }}</el-descriptions-item>
                    <el-descriptions-item label="性别">{{ ChineseGenderMap[member.model.gender] }}</el-descriptions-item>
                    <el-descriptions-item label="父亲">{{ father.displayName }}</el-descriptions-item>
                    <el-descriptions-item label="母亲">{{ mother.displayName }}</el-descriptions-item>
                    <el-descriptions-item label="伴侣">{{ spouse.displayName }}</el-descriptions-item>
                    <el-descriptions-item label="出生日期">{{ ConvertUTCToBeijingTime(member.model.birthDate)
                        }}</el-descriptions-item>
                    <el-descriptions-item label="死亡日期">{{ ConvertUTCToBeijingTime(member.model.deathDate)
                    }}</el-descriptions-item>
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
import { onMounted, ref, defineModel, defineProps, watch } from 'vue';
import { useRoute } from 'vue-router';
import { ConvertUTCToBeijingTime } from '../common/utils/ConvertTime';
import { ChineseGenderMap, FamilyMemberResponse } from './utils/familyDtos';
import { GetFamiliyMemberAsync } from './utils/familyHelper';

// status 
const status = defineModel('status', { required: true })

// model
const member = defineProps(['model'])
const father = ref(new FamilyMemberResponse('', '', '', ''))
const mother = ref(new FamilyMemberResponse('', '', '', ''))
const spouse = ref(new FamilyMemberResponse('', '', '', ''))

const route = useRoute()

onMounted(async () => {
    console.log(member)
})

watch(member, async () => {
    await Promise.all([GetFather(), GetMother(), GetSpouse()])
    console.log(father.value)
    console.log(mother.value)
    console.log(spouse.value)
})

async function GetFather() {
    if (member.model.fatherId == null || member.model.fatherId == '') {
        return ''
    }

    var { result, response } = await GetFamiliyMemberAsync(member.model.fatherId);
    console.log(result)
    console.log(response)
    if (result) {
        father.value = response
    }
}
async function GetMother() {
    if (member.model.motherId == null || member.model.motherId == '') {
        return ''
    }

    var { result, response } = await GetFamiliyMemberAsync(member.model.motherId);
    console.log(result)
    console.log(response)
    if (result) {
        mother.value = response
    }
}
async function GetSpouse() {
    if (member.model.spouseId == null || member.model.spouseId == '') {
        return ''
    }

    var { result, response } = await GetFamiliyMemberAsync(member.model.spouseId);
    console.log(result)
    console.log(response)
    if (result) {
        spouse.value = response
    }
}

</script>

<style lang="css" scoped>
.el-row {
    margin-bottom: 10px;
}
</style>