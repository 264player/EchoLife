<template>
    <p @click="handleNodeClick(treeData.id)">{{ treeData.id }}</p>
    <hr />
    <div v-if="treeData.children != null && treeData.children.length != 0">
        <LifeSubsection v-for="node in treeData.children" :key="node.id" :treeData="node"
            @node-click="$emit('node-click', $event)"></LifeSubsection>
    </div>

</template>

<script setup>
import { ref, defineProps, onMounted, defineEmits } from 'vue';

const originalData = defineProps(["treeData"])

const treeData = ref(originalData.treeData)

onMounted(() => {

})
const emit = defineEmits(['node-click']);

function handleNodeClick(nodeId) {
    emit('node-click', nodeId);
}
</script>

<style lang="css" scoped></style>
