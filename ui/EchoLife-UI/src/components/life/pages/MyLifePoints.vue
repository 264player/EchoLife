<template>
    <NewLifePoint v-model:status="newLifePoint" v-model:list="lifePoints" :reload="Reload"></NewLifePoint>
    <UpdateLifePoint v-model:status="updatePoint" v-model:point="currentPoint"></UpdateLifePoint>
    <el-button @click="newLifePoint = true">新的节点</el-button>
    <el-divider></el-divider>
    <!-- <el-table v-infinite-scroll="GetMyLifePoints" :data="lifePoints" height="800" style="width: 100%;overflow: auto;"
        :stripe="true" @row-dblclick="GetPointDetails">
        <el-table-column prop="content" label="内容" width="100" />
        <el-table-column prop="hidden" label="可见度" width="100">
            <template #default="scope">
                {{ scope.row.hidden == true ? "隐藏" : "公开" }}
            </template>
</el-table-column>
<el-table-column prop="createdAt" label="创建时间">
    <template #default="scope">
                {{ ConvertUTCToBeijingTime(scope.row.createdAt) }}
            </template>
</el-table-column>
<el-table-column prop="updatedAt" label="更新时间">
    <template #default="scope">
                {{ ConvertUTCToBeijingTime(scope.row.updatedAt) }}
            </template>
</el-table-column>
<el-table-column label="操作">
    <template #default="scope">
                <el-button size="small" @click="UpdatePoint(scope.row)">
                    修改
                </el-button>
                <InviteOtherUser v-model:pointId="scope.row.id"></InviteOtherUser>
                <el-button size="small" type="danger" @click="DeleteLifePoint(scope.row)">
                    删除
                </el-button>
            </template>
</el-table-column>
</el-table> -->
    <el-scrollbar height="600px">
        <el-timeline style="max-width: 600px" v-infinite-scroll="GetMyLifePoints" :infinite-scroll-immediate="true">
            <el-timeline-item :timestamp="ConvertUTCToBeijingTime(point.createdAt)" placement="top"
                v-for="point in lifePoints" :key="point.id" height="700">
                <el-card>
                    <el-button @click="UpdatePoint(point)">
                        修改
                    </el-button>
                    <InviteOtherUser v-model:pointId="point.id"> </InviteOtherUser>
                    <el-button type="danger" @click="DeleteLifePoint(point)">
                        删除
                    </el-button>
                    <el-divider></el-divider>
                    <MdPreview v-model="point.content"></MdPreview>
                    <el-divider></el-divider>
                    <el-text>最后更新于{{ ConvertUTCToBeijingTime(point.updatedAt) }}</el-text>
                </el-card>
            </el-timeline-item>
        </el-timeline>
    </el-scrollbar>
</template>

<script setup>
import { ref } from 'vue';
import NewLifePoint from '../NewLifePoint.vue';
import UpdateLifePoint from '../UpdateLifePoint.vue';
import { GetMyLifePointsAsync, DeleteLifePointAsync, GetLifePointsAsync } from '../utils/LifeHelpers';
import { useUserStore } from '@/stores/counter';
import { PageInfo } from '@/utils/WillRequestDtos';
import { ElMessage } from 'element-plus';
import { ConvertUTCToBeijingTime } from '@/components/common/utils/ConvertTime';
import { MdPreview, MdCatalog, MdEditor } from 'md-editor-v3';
import 'md-editor-v3/lib/preview.css';
import InviteOtherUser from '../InviteOtherUser.vue';

// status
const newLifePoint = ref(false)
const updatePoint = ref(false)
const loading = ref(false)


const currentPoint = ref({})

const lifePoints = ref([])

const userStore = useUserStore()

const pageInfo = ref(new PageInfo(3, null))

async function GetMyLifePoints() {
    if (loading.value) {
        return
    }
    loading.value = true

    var { result, response } = await GetLifePointsAsync(pageInfo.value)
    console.log(response)
    if (result) {
        console.log(response)
        if (response.length != 0) {
            pageInfo.value.cursorId = response[response.length - 1].id
            lifePoints.value = lifePoints.value.concat(response)
        }
    }

    loading.value = false
}

function GetPointDetails() {

}

function UpdatePoint(point) {
    updatePoint.value = true
    console.debug(point)
    currentPoint.value = point
}

async function DeleteLifePoint(lifePoint) {
    var { result, response } = await DeleteLifePointAsync(lifePoint.id)
    console.log(result)
    console.log(response)
    ElMessage({
        type: result ? "success" : "error",
        message: result ? "删除成功" : "删除失败"
    })
    if (result) {
        var index = lifePoints.value.findIndex(v => v.id == lifePoint.id)
        if (index !== -1) {
            lifePoints.value.splice(index, 1)
        }
    }
}

async function Reload() {
    pageInfo.value.cursorId = null;
    lifePoints.value = [];
    await GetMyLifePoints()
}

</script>

<style lang="css" scoped>
.el-button {
    margin-right: 5px;
}
</style>