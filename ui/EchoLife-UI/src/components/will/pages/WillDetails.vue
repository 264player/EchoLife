<template>
    <NewWillVersion v-if="showNewVersionStatus" v-model:status="showNewVersionStatus" :reload="Reload"
        :current-type="currentVersion.willType" :current-content="currentVersion.value" :willId="willId">
    </NewWillVersion>
    <el-row>
        <el-col :span="16">
            <el-row>
                <el-col :span="24">
                    <p><el-text>遗嘱名</el-text></p>
                    <el-input v-model="willResponse.name" />
                </el-col>
            </el-row>
            <el-row>
                <el-col :span="24">
                    <p><el-text>遗嘱类型</el-text></p>
                    <el-select v-model="currentVersion.willType" placeholder="Select" style="width: 240px">
                        <el-option v-for="item in willTypeArray" :key="item.value" :label="item.name"
                            :value="item.value" :disabled="!item.supported" style="border-bottom:0px;"
                            class="custom-option" />
                    </el-select>
                </el-col>
            </el-row>
            <el-row>
                <el-col :span="24">
                    <p><el-text>遗嘱内容</el-text></p>
                    <MdEditor v-model="currentVersion.value" v-if="!needFile"></MdEditor>
                    <div v-else>
                        <el-link target="_blank" :href="currentVersion.value" underline>点击此处下载附件</el-link>
                        <input type="file" @change="HandleFileChange" />
                        <button @click="UploadFile">上传文件</button>
                    </div>
                </el-col>
            </el-row>
            <el-row>
                <el-col :span="24">
                    <el-button @click="UpdateWillAndVersion">保存更改</el-button>
                    <el-button @click="DeleteWillVersion">删除该版本</el-button>
                    <el-button @click="aiReviewStatus = true" :disabled="needFile">查看AI审核</el-button>
                    <el-button @click="RequestHumanReview">请求审核</el-button>
                    <MyFileList></MyFileList>
                </el-col>
            </el-row>
        </el-col>
        <el-col :span="6" :offset="2">
            <el-button @click="showNewVersionStatus = true"><el-text>创建新的版本</el-text></el-button>
            <el-table :data="willVersions" height="800" style="width: 100%;overflow: auto;"
                :show-overflow-tooltip="true" v-infinite-scroll="GetWillVersions" @row-click="SwitchVersion"
                :infinite-scroll-distance="10" infinite-scroll-immediate row-class-name="success-row">
                <el-table-column label="更新时间">
                    <template #default="scope">
                        <el-text class="mx-1" :type="Checked(scope.row.id)">
                            {{ ConvertUTCToBeijingTime(scope.row.updatedAt) }}
                        </el-text>
                    </template>
                </el-table-column>
                <el-table-column prop=" willType" label="遗嘱类型" width="100">
                    <template #default="scope">
                        <el-text class="mx-1" :type="Checked(scope.row.id)">
                            {{ willTypeMap[scope.row.willType] }}
                        </el-text>
                    </template>
                </el-table-column>
            </el-table>
        </el-col>
    </el-row>

    <el-drawer v-model="aiReviewStatus" title="I am the title" :with-header="false">
        <span>{{ aiReviewResult }}</span>
        <br />
        <el-button @click="RequestAIReview">重新生成内容</el-button>
    </el-drawer>
</template>

<script setup>
import { WillResponse, WillVersionRequest, WillVersionResponse, PutWillRequest, PageInfo } from '@/utils/WillRequestDtos';
import { GetWillAsyn, GetWillVersionsAsync, UpdateWillAsync, UpdateWillVersionAsync, DeleteWillVersionAsync, RequestHumanReviewAsync, RequestAIReviewAsync } from '@/utils/WillRequestHelper';
import { useRoute } from 'vue-router';
import { ref, onMounted, computed } from 'vue';
import { ElMessage } from 'element-plus';
import { willTypeArray, willTypeMap } from '@/utils/WillRequestDtos';
import { MdEditor } from 'md-editor-v3';
import 'md-editor-v3/lib/style.css';
import { UploadAsync } from '@/components/common/utils/upload';
import MyFileList from '@/components/common/MyFileList.vue';
import { ConvertUTCToBeijingTime } from '@/components/common/utils/ConvertTime';
import NewWillVersion from '../NewWillVersion.vue';

const route = useRoute()

const willId = ref("")

// status
const loading = ref(false)
const aiReviewStatus = ref(false)
const showNewVersionStatus = ref(false)

// computed
const needFile = computed(() =>
    currentVersion.value.willType.toLowerCase() == "audio" || currentVersion.value.willType.toLowerCase() == "video"
)

// file upload
function HandleFileChange(event) {
    file.value = event.target.files[0];
    console.log(file.value)
    console.log(event)
}

async function UploadFile() {
    if (!file || !file.value) {
        alert("请选择文件");
        return;
    }
    console.log(file.value)
    const uploadUrl = `http://localhost:9000/static/test/${file.value.name}`;
    currentVersion.value.value = uploadUrl
    // 使用 axios 上传文件
    var { result, response } = await UploadAsync(uploadUrl, file.value);
    ElMessage({
        type: result ? "success" : "error",
        message: result ? "上传成功" : "上传失败"
    })
    if (result) {
        UpdateWillAndVersion()
    }
}

const file = ref()

// model
const currentVersion = ref(new WillVersionResponse("", '', '', '', '', ''))
const pageInfo = ref(new PageInfo(30, null))
const willResponse = ref(new WillResponse())
const willVersions = ref([])
const aiReviewResult = ref("")

onMounted(async () => {
    // load current will.
    willId.value = route.params.willId
    // load will versions.
    await Promise.all([GetWill(), GetWillVersions()])

    currentVersion.value = willVersions.value[0]
})


function Checked(id) {
    if (id == willResponse.value.contentId) { return 'success' }
    return 'info'
}

async function SwitchVersion(version) {
    console.debug(version)
    currentVersion.value = version
}

async function UpdateWillAndVersion() {
    console.debug(willResponse.value)
    console.debug(currentVersion.value)
    await UpdateWill()
    await UpdateWillVersion()
}


async function GetWillVersions() {
    if (loading.value) {
        return
    }
    loading.value = true

    var { result, response } = await GetWillVersionsAsync(willId.value, pageInfo.value)
    console.log(result)
    console.log(response)
    if (result && response.length > 0) {
        willVersions.value = willVersions.value.concat(response)
        pageInfo.value.cursorId = response[response.length - 1].id
        console.log(willVersions.value[willVersions.value.length - 1])
    }
    loading.value = false
}

async function GetWill() {
    var { result, response } = await GetWillAsyn(willId.value);
    console.log(result)
    console.log(response)
    if (result) {
        willResponse.value = response
    }
}

async function UpdateWill() {
    var { result, response } = await UpdateWillAsync(willId.value, new PutWillRequest(willResponse.value.name, currentVersion.value.id, currentVersion.value.willType))
    console.debug(result)
    console.debug(response)
}

async function UpdateWillVersion() {
    var { result, response } = await UpdateWillVersionAsync(currentVersion.value.id, new WillVersionRequest(currentVersion.value.willType, currentVersion.value.value))
    console.debug(result)
    console.debug(response)
    ElMessage({
        type: result ? "success" : "error",
        message: result ? "保存成功" : "保存失败"
    })
    if (result) {
        willResponse.value.contentId = currentVersion.value.id
    }
}

async function DeleteWillVersion() {
    var { result, response } = await DeleteWillVersionAsync(currentVersion.value.id)
    console.debug(result)
    console.debug(response)
    ElMessage({
        type: result ? "success" : "error",
        message: result ? "删除成功" : "删除失败"
    })

    if (result) {
        var index = willVersions.value.findIndex(v => v.id == currentVersion.value.id)
        if (index !== -1) {
            willVersions.value.splice(index, 1)
            currentVersion.value = new WillVersionResponse()
        }
    }
}

async function RequestHumanReview() {
    var { result, _ } = await RequestHumanReviewAsync(currentVersion.value.id)
    if (result) {
        ElMessage({
            type: "info",
            message: "请求成功，前往审核中心查看"
        })
    }
}

async function RequestAIReview() {
    var { result, response } = await RequestAIReviewAsync(currentVersion.value.id)
    if (result) {
        aiReviewResult.value = response
    }
}

async function Reload() {
    pageInfo.value.cursorId = null
    willVersions.value = []
    await GetWillVersions()
    console.log('reload')
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

.el-table .success-row {
    color: rgb(209.4, 236.7, 195.9);
}

.custom-option {
    display: flex;
    align-items: center;
    justify-content: center;
    height: 40px;
}
</style>