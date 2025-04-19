<template>
    <el-row>
        <el-co :span="18">
            <el-row>
                <el-col :span="24"><el-text>遗嘱名</el-text>
                    <el-input v-model="willResponse.name" />
                </el-col>
            </el-row>
            <el-row>
                <el-col :span="24"><el-text>遗嘱类型</el-text>
                    <el-input v-model="currentVersion.willType" />
                </el-col>
            </el-row>
            <el-row>
                <el-col :span="24"><el-text>遗嘱内容</el-text>
                    <el-input v-model="currentVersion.value" type="textarea" :rows="10" />
                </el-col>
            </el-row>
            <el-row>
                <el-col :span="24">
                    <el-button @click="UpdateWillAndVersion">保存更改</el-button>
                    <el-button @click="DeleteWillVersion">删除该版本</el-button>
                </el-col>
            </el-row>
        </el-co>
        <el-co :span="6">
            <el-button @click="newWillVersion = true"><el-text>创建新的版本</el-text></el-button>
            <el-table :data="willVersions" height="150" style="width: 100%;overflow: auto;" :stripe="true"
                :show-overflow-tooltip="true" v-infinite-scroll="GetWillVersions" @row-dblclick="SwitchVersion">
                <el-table-column prop="id" label="ID" width="180" />
                <el-table-column prop="value" label="内容" width="180" />
            </el-table>
        </el-co>
    </el-row>

    <el-dialog v-model="newWillVersion" title="新的版本" width="800">
        <el-text>遗嘱类型</el-text>
        <el-input v-model="willVersionRequest.WillType" />
        <el-text>内容</el-text>
        <el-input v-model="willVersionRequest.Value" />
        <el-button @click="CreateWillVersion">确认</el-button>
    </el-dialog>
</template>

<script setup>
import { WillResponse, QueryWillVersionsRequest, WillVersionRequest, WillVersionResponse, WillRequest, PutWillRequest } from '@/utils/WillRequestDtos';
import { GetWillAsyn, GetWillVersionsAsync, CreateWillVersionAsync, UpdateWillAsync, UpdateWillVersionAsync, DeleteWillVersionAsync } from '@/utils/WillRequestHelper';
import { useRoute } from 'vue-router';
import { ref, onMounted } from 'vue';

const route = useRoute()

const willId = ref("")

const loading = ref(false)
const isEnd = ref(false)
const newWillVersion = ref(false)

const currentVersion = ref(new WillVersionResponse())

const willVersionRequest = ref(new WillVersionRequest(null, null))

const queryWillVersionsRequest = ref(new QueryWillVersionsRequest(10, null))

const willResponse = ref(new WillResponse())

const willVersions = ref([])

onMounted(async () => {
    // load current will.
    willId.value = route.params.willId
    // load will versions.
    await Promise.all([GetWill(), GetWillVersions()])

    currentVersion.value = willVersions.value[0]
})

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

async function CreateWillVersion() {
    var { result, response } = await CreateWillVersionAsync(willId.value, willVersionRequest.value, false)
    console.log(result)
    console.log(response)
}

async function GetWillVersions() {
    if (loading.value || isEnd.value) {
        return
    }
    loading.value = true

    var { result, response } = await GetWillVersionsAsync(willId.value, queryWillVersionsRequest.value)
    console.log(result)
    console.log(response)
    if (result) {
        if (response.length == 0) {
            isEnd.value = true
        }
        willVersions.value = willVersions.value.concat(response)
        queryWillVersionsRequest.value.CusorId = willVersions.value[willVersions.value.length - 1].id
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
    var { result, response } = await UpdateWillAsync(willId.value, new PutWillRequest(willResponse.value.name, willResponse.value.contentId))
    console.debug(result)
    console.debug(response)
}

async function UpdateWillVersion() {
    var { result, response } = await UpdateWillVersionAsync(currentVersion.value.id, new WillVersionRequest(currentVersion.value.willType, currentVersion.value.value))
    console.debug(result)
    console.debug(response)
}

async function DeleteWill() {

}

async function DeleteWillVersion() {
    var { result, response } = await DeleteWillVersionAsync(currentVersion.value.id)
    console.debug(result)
    console.debug(response)
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
</style>