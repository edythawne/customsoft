<template>
    <v-dialog
        v-model="show"
        @update:model-value="onCloseModal"
        max-width="400"
        :persistent="props.persist">

        <v-card :text="props.description">

            <template v-slot:actions>
                <v-spacer></v-spacer>

                <v-btn @click="onCloseModal">
                    Cancelar
                </v-btn>

                <v-btn @click="emit('accept')">
                    Aceptar
                </v-btn>
            </template>
        </v-card>

    </v-dialog>
</template>

<script setup lang="ts">

import { ref, watch } from 'vue'

export interface Props {
    title: string;
    description : string;
    persist?: boolean;
    modelValue: boolean;
}

const props = withDefaults(defineProps<Props>(), {
    persist : true
})

const emit = defineEmits(['close', 'accept'])
const show = ref<boolean>(false)

const onCloseModal = () => {
    show.value = false
    emit('close')
}

watch(props, () => (show.value = props.modelValue))

</script>

<style scoped>

</style>
