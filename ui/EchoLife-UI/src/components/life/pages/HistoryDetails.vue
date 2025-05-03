<template>
    <NewLifeSubSection v-model:status="newSubSectionStatus" v-model:historyId="historyId"></NewLifeSubSection>
    <el-input v-model="history.title" />
    <el-row>
        <el-col :span="16">
            <el-row>
                <el-col :span="24"><el-text>遗嘱名</el-text>
                    <el-input v-model="currentSection.id" />
                </el-col>
            </el-row>
            <el-row>
                <el-col :span="24"><el-text>遗嘱类型</el-text>
                    <el-input v-model="currentSection.title" />
                </el-col>
            </el-row>
            <el-row>
                <el-col :span="24"><el-text>遗嘱内容</el-text>
                    <el-input v-model="currentSection.content" type="textarea" :rows="10" />
                </el-col>
            </el-row>
            <el-row>
                <el-col :span="24">
                    <el-button @click="UpdateSubsectino">保存更改</el-button>
                    <el-button @click="DeleteSubSection">删除当前小节</el-button>
                    <el-button @click="aiReviewStatus = true">查看AI润色</el-button>
                    <el-button @click="propmt">查看内容提示</el-button>
                </el-col>
            </el-row>
        </el-col>
        <el-col :span="6" :offset="2">
            <el-button @click="newSubSectionStatus = true">新的小节</el-button>
            <el-table :data="sections" height="400" style="width: 100%;overflow: auto;" :stripe="true"
                :show-overflow-tooltip="true" v-infinite-scroll="GetMyLifeSubsecions" @row-click="SwitchSection">
                <el-table-column prop="id" label="ID" width="180" />
                <el-table-column prop="content" label="内容" width="180" />
            </el-table>
        </el-col>
    </el-row>

    <el-drawer v-model="aiReviewStatus" title="I am the title" :with-header="false">
        <span>promp is here</span>
    </el-drawer>
</template>

<script setup>
import { WillResponse, WillVersionResponse, PageInfo } from '@/utils/WillRequestDtos';
import { useRoute } from 'vue-router';
import { ref, onMounted } from 'vue';
import { ElMessage } from 'element-plus';
import { DeleteLifeSubSectionAsync, GetMyLifeHistoryAsync, GetMyLifeSubsectionsAsync, UpdataLifeSubSectionAsync } from '../utils/LifeHelpers';
import NewLifeSubSection from '../NewLifeSubSection.vue';
import { LifeSubsectionRequest, LifeSubSectionResponse } from '../utils/LifeDtos';

const route = useRoute()

const historyId = ref("")
const history = ref({})

// status
const loading = ref(false)
const isEnd = ref(false)
const newSubSectionStatus = ref(false)
const aiReviewStatus = ref(false)

// models
const currentSection = ref(new LifeSubSectionResponse())
const pageInfo = ref(new PageInfo(30, null))
const willResponse = ref(new WillResponse())
const sections = ref([])

onMounted(async () => {
    // load current will.
    historyId.value = route.params.historyId
    // load will versions.
    await Promise.all([GetHistory(), GetMyLifeSubsecions()])
})

async function SwitchSection(section) {
    console.debug(section)
    currentSection.value = section
}

async function GetMyLifeSubsecions() {
    if (loading.value) {
        return
    }
    loading.value = true

    var { result, response } = await GetMyLifeSubsectionsAsync(historyId.value, pageInfo.value)
    console.log(result)
    console.log(response)
    console.log(pageInfo.value)
    if (result) {
        pageInfo.value.cursorId = response[response.length - 1].id
        sections.value = sections.value.concat(response)
    }

    loading.value = false
}

async function GetHistory() {
    var { result, response } = await GetMyLifeHistoryAsync(historyId.value);
    console.log(result)
    console.log(response)
    if (result) {
        history.value = response
    }
}


async function UpdateSubsectino() {
    var { result, response } = await UpdataLifeSubSectionAsync(currentSection.value.id, new LifeSubsectionRequest(currentSection.value.title, currentSection.value.content, currentSection.value.lifeHistoryId, currentSection.value.fatherId, currentSection.value.index))
    console.debug(result)
    console.debug(response)
    ElMessage({
        type: result ? "success" : "error",
        message: result ? "保存成功" : "保存失败"
    })
}

async function DeleteSubSection() {
    var { result, response } = await DeleteLifeSubSectionAsync(currentSection.value.id)
    console.debug(result)
    console.debug(response)
    ElMessage({
        type: result ? "success" : "error",
        message: result ? "删除成功" : "删除失败"
    })

    if (result) {
        var index = sections.value.findIndex(v => v.id == currentSection.value.id)
        if (index !== -1) {
            sections.value.splice(index, 1)
            currentSection.value = new WillVersionResponse()
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