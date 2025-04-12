<template>
    <el-form :label-position="labelPosition" label-width="auto" :model="formLabelAlign" style="max-width: 600px">
        <el-form-item label="Name" label-position="right">
            <el-input />
        </el-form-item>
        <el-form-item label="Activity zone" label-position="right">
            <el-input />
        </el-form-item>
        <el-form-item label="Activity form" label-position="right">
            <el-input />
        </el-form-item>
    </el-form>
</template>

<script setup>
import { ref } from 'vue'

class WillVersion {
    constructor(WillType, Value) {
        this.WillType = WillType;
        this.Value = Value;
    }
}

const count = ref(0)

</script>
