<template>
    <el-dialog v-model="status">
        <el-row>
            <el-col :span="4"></el-col>
            <el-col :span="16">
                <el-row>
                    <el-col :span="24">
                        <el-text type="info">成员名称</el-text>
                        <el-input v-model="member.DisplayName" size="small" />
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="24">
                        <el-text type="info">性别</el-text>
                        <el-select v-model="member.gender" placeholder="Select" style="width: 240px">
                            <el-option v-for="item in GenderArray" :key="item.key" :label="item.value"
                                :value="item.key" />
                        </el-select>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="24">
                        <el-text type="info">父亲</el-text>
                        <el-select v-model="member.fatherId" placeholder="Select" style="width: 240px">
                            <el-option v-for="item in members" :key="item.id" :label="item.displayName"
                                :value="item.id" />
                        </el-select>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="24">
                        <el-text type="info">母亲</el-text>
                        <el-select v-model="member.motherId" placeholder="Select" style="width: 240px">
                            <el-option v-for="item in members" :key="item.id" :label="item.displayName"
                                :value="item.id" />
                        </el-select>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="24">
                        <el-text type="info">配偶</el-text>
                        <el-select v-model="member.spouseId" placeholder="Select" style="width: 240px">
                            <el-option v-for="item in members" :key="item.id" :label="item.displayName"
                                :value="item.id" />
                        </el-select>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="24">
                        <el-text type="info">出生日期</el-text>
                        <el-date-picker v-model="member.birthDate" type="date" placeholder="选择一个日期" :size="size" />
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="24">
                        <el-text type="info">死亡日期</el-text>
                        <el-date-picker v-model="member.DeathDate" type="date" placeholder="选择一个日期" :size="size" />
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="24">
                        <el-button @click="CreateMember">创建新成员</el-button>
                    </el-col>
                </el-row>
            </el-col>
            <el-col :span="4"></el-col>
        </el-row>
    </el-dialog>
</template>


<script setup>
import { ref, defineModel, defineProps, onMounted } from 'vue';
import { ElMessage } from 'element-plus';
import { CreateMemberAsync } from './utils/familyHelper';
import { ChineseGenderMap, FamilyMemberRequest, GenderArray } from './utils/familyDtos';

const member = ref(new FamilyMemberRequest("", "", "male", null, null, null, null, null, 0, 0))
const status = defineModel("status", { required: true })
const members = defineModel("list", { required: true })
const familyId = defineProps(["fid"])

onMounted(() => {
    member.value.familyId = familyId.fid
})

async function CreateMember() {
    console.log(familyId)
    member.value.familyId = familyId.fid
    var { result, response } = await CreateMemberAsync(member.value);
    console.log(result)
    console.log(response)
    ElMessage({
        type: result ? "success" : "error",
        message: result ? "创建成功" : "创建失败"
    })
    if (result) {
        members.value.unshift(response)
    }
}

</script>

<style lang="css" scoped>
.el-row {
    margin-bottom: 10px;
}
</style>