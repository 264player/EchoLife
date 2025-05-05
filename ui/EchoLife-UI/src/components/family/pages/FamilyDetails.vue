<template>
    <NewFamilyMember :fid="family.id" v-model:status="newMemberStatus" v-model:list="members">
    </NewFamilyMember>
    <FamilyMember :model="currentMember" v-model:status="viewMemberDetailsStatus"></FamilyMember>
    <el-row>
        <el-col :span="16">
            <el-row>
                <el-col :span="24"><el-text>家族名称</el-text>
                    <el-input v-model="family.name" />
                </el-col>
            </el-row>
            <el-row>
                <el-col :span="24">
                    <el-button @click="newMemberStatus = true"><el-text>创建新的家族成员</el-text></el-button>
                    <el-table :data="members" height="600" style="width: 100%;overflow: auto;" :stripe="true"
                        @row-click="ViewMemberDetails">
                        <el-table-column prop="displayName" label="名称" width="180" />
                        <el-table-column label="性别" width="180">
                            <template #default="scope">
                                {{ ChineseGenderMap[scope.row.gender] }}
                            </template>
                        </el-table-column>
                        <el-table-column label="操作">
                            <template #default="scope">
                                <el-button size="small" type="danger" @click="DeleteMember(scope.row)">
                                    删除
                                </el-button>
                            </template>
                        </el-table-column>
                    </el-table>
                </el-col>
            </el-row>
        </el-col>
        <el-col :span="6" :offset="2">
        </el-col>
    </el-row>

    <el-drawer v-model="aiReviewStatus" title="I am the title" :with-header="false">
        <span>Hi there!</span>
    </el-drawer>
</template>

<script setup>
import { WillVersionRequest } from '@/utils/WillRequestDtos';
import { GetWillAsyn, UpdateWillVersionAsync } from '@/utils/WillRequestHelper';
import { useRoute } from 'vue-router';
import { ref, onMounted } from 'vue';
import { ElMessage } from 'element-plus';
import { ChineseGenderMap, FamilyTreeResponse } from '../utils/familyDtos';
import { DeleteMemberAsync, GetFamiliyMembersAsync, GetFamilyAsyn } from '../utils/familyHelper';
import NewFamilyMember from '../NewFamilyMember.vue';
import FamilyMember from '../FamilyMember.vue';

const route = useRoute()



//status
const newMemberStatus = ref(false)
const viewMemberDetailsStatus = ref(false)
const aiReviewStatus = ref(false)

// model
const familyId = ref("")
const family = ref(new FamilyTreeResponse())
const members = ref([])

const currentMember = ref()

onMounted(async () => {

    familyId.value = route.params.familyId
    console.log(familyId.value)
    await Promise.all([GetFamily(), GetFamilyMembers()])

    currentVersion.value = willVersions.value[0]
})

function ViewMemberDetails(item) {
    console.debug(item)
    currentMember.value = item
    viewMemberDetailsStatus.value = true
}

async function UpdateWillAndVersion() {
    console.debug(willResponse.value)
    console.debug(currentVersion.value)
    await UpdateWill()
    await UpdateWillVersion()
}

async function GetFamilyMembers() {
    var { result, response } = await GetFamiliyMembersAsync(familyId.value)
    console.log(result)
    console.log(response)
    if (result) {
        members.value = response
    }
}

async function GetFamily() {
    var { result, response } = await GetFamilyAsyn(familyId.value);
    console.log(result)
    console.log(response)
    if (result) {
        family.value = response
    }
}

async function UpdateWillVersion() {
    var { result, response } = await UpdateWillVersionAsync(currentVersion.value.id, new WillVersionRequest(currentVersion.value.willType, currentVersion.value.value))
    console.debug(result)
    console.debug(response)
    ElMessage({
        type: result ? "success" : "error",
        message: result ? "保存成功" : "保存失败"
    })
}

async function DeleteMember(target) {
    var { result, response } = await DeleteMemberAsync(target.id)
    console.debug(result)
    console.debug(response)
    ElMessage({
        type: result ? "success" : "error",
        message: result ? "删除成功" : "删除失败"
    })

    if (result) {
        var index = members.value.findIndex(v => v.id == target.id)
        if (index !== -1) {
            members.value.splice(index, 1)
        }
    }
}
</script>

<style lang="css" scoped>
.will-version-list {
    height: 100px;
    padding: 0;
    margin: 0;
    list-style: none;
}

li {
    padding: 10px 15px;
    cursor: pointer;
    border-bottom: 1px solid #e0e0e0;
    box-sizing: border-box;
}

li:hover {
    background-color: #f0f0f0;
}

li:last-child {
    border-bottom: none;
}

.el-input,
.el-textarea {
    margin-bottom: 16px;
}
</style>