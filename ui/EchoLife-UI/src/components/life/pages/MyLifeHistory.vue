<template>
    <el-row>
        <el-col :span="18">
            <p><el-text>{{ currentSection.title }}</el-text></p>
            <p><el-text>{{ currentSection.content }}</el-text></p>
            <p> <el-text>{{ currentSection.createdAt }}</el-text></p>
            <p> <el-text>{{ currentSection.updatedAt }}</el-text></p>
        </el-col>
        <el-col :span="4">
            <LifeSubsection v-for="node in treeData.children" :key="node.id" :treeData="node"
                @node-click="handleNodeClick"></LifeSubsection>
        </el-col>
    </el-row>
</template>

<script setup>
import LifeSubsection from '../LifeSubsection.vue';
import { ref, onMounted } from 'vue';
import { LifeSubSectionResponse } from '../utils/LifeDtos';

const originalData = ref([{ id: 1, title: "hhh", fatherId: null }, { id: 2, fatherId: "1" }, { id: 3, fatherId: "2" }, { id: 4, fatherId: "3" }])
const treeData = ref([])

const currentSection = ref(new LifeSubSectionResponse(1, "title", "content", null, null, 0, "createdAt", "updatedAt"))

onMounted(() => {
    GetTreeData()
    console.debug(treeData.value)
})

function GetTreeData() {
    treeData.value.children = ComputeTreeData(originalData.value, null)
}

function ComputeTreeData(tree, fatherId) {
    var list = []
    tree.forEach(node => {
        if (node.fatherId == fatherId) {
            node.children = ComputeTreeData(tree, node.id)
            list.push(node)
        }
    });
    return list
}

function handleNodeClick(nodeId) {
    console.debug(`nodeId:${nodeId}`)
    const node = originalData.value.find((item) => item.id === nodeId);
    if (node) {
        currentSection.value = new LifeSubSectionResponse(
            node.id,
            node.title,
            node.content,
            node.fatherId,
            null,
            0,
            node.createdAt,
            node.updatedAt
        );
    }
}
</script>

<style lang="css" scoped></style>