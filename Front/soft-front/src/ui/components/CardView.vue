<template>

    <v-card class="mx-auto rounded-md pa-4" border="sm" rounded="xl" variant="outlined" >
        <v-card-item class="mb-4">
            <v-card-title>
                {{props.title}}
            </v-card-title>
        </v-card-item>

        <slot></slot>
    </v-card>

</template>

<script setup lang="ts">

export interface Props {
    title: string
}

const props = defineProps<Props>()

</script>

<style scoped>

</style>
