<template>

    <v-btn prepend-icon="mdi-eye" variant="text" @click="()=> composable.onVisualizeAttachment($props.path)" >
        {{ $props.label }}
    </v-btn>

    <!--
    <div  class="row items-center cursor-pointer text-primary q-pa-sm"  >
        {{ $props.label }}
        <slot />
    </div>
    -->
</template>

<script setup lang="ts">
import useStorageComposable from '@/ui/composables/storage/useStorageComposable'

export interface Props { path: string, label: string }
withDefaults(defineProps<Props>(), { label: 'Ver archivo' })

const composable = useStorageComposable()
</script>

<style scoped lang="scss">

</style>
