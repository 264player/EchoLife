<template>
    <NewLifeSubSection v-model:status="newSubSectionStatus" v-model:historyId="historyId" v-model:sections="sections">
    </NewLifeSubSection>
    <el-text size="large">{{ history.title }}</el-text>
    <el-row>
        <el-col :span="16">
            <el-row>
                <el-col :span="24"><el-text>小节标题</el-text>
                    <el-input v-model="currentSection.title" />
                </el-col>
            </el-row>
            <el-row>
                <el-col :span="24"><el-text>小节内容</el-text>
                    <MdEditor v-model="currentSection.content"></MdEditor>
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
                :show-overflow-tooltip="true" @row-click="SwitchSection">
                <el-table-column label="序号">
                    <template #default="scope">
                        {{ indexGenerator.getIndexById(scope.row.id) }}
                    </template>
                </el-table-column>
                <el-table-column prop="title" label="标题" width="180" />
            </el-table>
        </el-col>
    </el-row>

    <el-drawer v-model="aiReviewStatus" title="I am the title" :with-header="false">
        <span>promp is here</span>
    </el-drawer>
</template>

<script setup>
import { PageInfo } from '@/utils/WillRequestDtos';
import { useRoute } from 'vue-router';
import { ref, onMounted } from 'vue';
import { ElMessage } from 'element-plus';
import { DeleteLifeSubSectionAsync, GetMyLifeHistoryAsync, GetMyLifeSubsectionsAsync, UpdataLifeSubSectionAsync } from '../utils/LifeHelpers';
import NewLifeSubSection from '../NewLifeSubSection.vue';
import { LifeSubsectionRequest, LifeSubSectionResponse } from '../utils/LifeDtos';
import { MdEditor } from 'md-editor-v3';
import 'md-editor-v3/lib/style.css';

const route = useRoute()

const historyId = ref("")
const history = ref({})

// status
const loading = ref(false)
const isEnd = ref(false)
const newSubSectionStatus = ref(false)
const aiReviewStatus = ref(false)

// models
const currentSection = ref(new LifeSubSectionResponse("", '', ''))
const sections = ref([])
const indexGenerator = ref()

onMounted(async () => {

    historyId.value = route.params.historyId

    await Promise.all([GetHistory(), GetMyLifeSubsecions()])

    // indexGenerator.value = createIndexGenerator(sections.value)
})

async function SwitchSection(section) {
    console.debug(section)
    currentSection.value = section
}

async function GetMyLifeSubsecions() {
    var { result, response } = await GetMyLifeSubsectionsAsync(historyId.value)
    console.log(result)
    console.log(response)
    if (result) {
        if (response.length == 0) {
            return
        }
        sections.value = response
        indexGenerator.value = createIndexGenerator(sections.value)
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
            currentSection.value = new LifeSubSectionResponse()
        }
    }
}

// index generator
function createIndexGenerator(data) {
    const indexMap = {};
    const childrenMap = {};

    // 初始化：构建父子映射并排序
    data.sort((a, b) => a.id - b.id).forEach(item => {
        const parentId = item.fatherId ?? null;
        if (!childrenMap[parentId]) {
            childrenMap[parentId] = [];
        }
        childrenMap[parentId].push(item);
    });

    // 递归分配层级编号
    function assignIndex(items, prefix = '') {
        items.forEach((item, index) => {
            const currentIndex = prefix ? `${prefix}-${index + 1}` : `${index + 1}`;
            indexMap[item.id] = currentIndex;
            assignIndex(childrenMap[item.id] || [], currentIndex);
        });
    }

    assignIndex(childrenMap[null]);

    return {
        getIndexById: (id) => indexMap[id] || null,
        getAllIndexes: () => ({ ...indexMap })
    };
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