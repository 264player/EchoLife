<template>
    <NewLifePoint v-model="newLifePoint"></NewLifePoint>
    <UpdateLifePoint v-model:status="updatePoint" v-model:point="currentPoint"></UpdateLifePoint>
    <el-button @click="newLifePoint = true">新的节点</el-button>
    <el-table v-infinite-scroll="GetMyWill" :data="lifePoints" height="250" style="width: 100%;overflow: auto;"
        :stripe="true" @row-dblclick="GetPointDetails">
        <el-table-column prop="id" label="ID" width="100" />
        <el-table-column prop="content" label="内容" width="100" />
        <el-table-column prop="hidden" label="可见度" width="100" />
        <el-table-column prop="createdAt" label="创建时间" />
        <el-table-column prop="updatedAt" label="更新时间" />
        <el-table-column label="操作">
            <template #default="scope">
                <el-button size="small" @click="UpdatePoint(scope.row)">
                    修改
                </el-button>
                <el-button size="small" type="danger" @click="DeleteWill(scope.row)">
                    删除
                </el-button>
            </template>
        </el-table-column>
    </el-table>
</template>

<script setup>
import { ref } from 'vue';
import NewLifePoint from '../NewLifePoint.vue';
import UpdateLifePoint from '../UpdateLifePoint.vue';

// status
const newLifePoint = ref(false)
const updatePoint = ref(false)

const currentPoint = ref({})

const lifePoints = ref([{ id: 1, content: 1, userId: 1, hidden: false, createdAt: "createdTime", updatedAt: "updatedTime" },
{ id: 2, content: 2, userId: 2, hidden: false, createdAt: "createdTime", updatedAt: "updatedTime" },
{ id: 3, content: 3, userId: 3, hidden: true, createdAt: "createdTime", updatedAt: "updatedTime" }])

function GetMyWill() {

}

function GetPointDetails() {

}

function UpdatePoint(point) {
    updatePoint.value = true
    console.debug(point)
    currentPoint.value = point
}
</script>

<style lang="css" scoped></style>