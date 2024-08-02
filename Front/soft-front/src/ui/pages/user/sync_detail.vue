<template>
    <v-container>
        <v-row no-gutters>
            <v-col align-self="center">
                <v-sheet class="pa-2 ma-2">

                    <CardView title="Crear un nuevo usuario">
                        <template #default>
                            <v-card-text>
                                <v-sheet>

                                    <v-form ref="form" class="pa-4">

                                        <v-row>
                                            <v-text-field
                                                v-model="composable.viewModel.value.requestDetail.curp"
                                                :rules="[v => !!v || 'Campo es requerido', v => v.length <= 18 || 'Debe tener 18 caracteres']"
                                                label="Curp"
                                                required
                                                type="text"
                                                variant="outlined"
                                                prepend-inner-icon="mdi-email"
                                                class="mb-4 v-col-6"
                                            ></v-text-field>

                                            <v-text-field
                                                v-model="composable.viewModel.value.requestDetail.place"
                                                :rules="[v => !!v || 'Campo es requerido']"
                                                label="Lugar de nacimiento"
                                                required
                                                type="text"
                                                variant="outlined"
                                                prepend-inner-icon="mdi-email"
                                                class="mb-4 v-col-6"
                                            ></v-text-field>

                                            <v-select
                                                label="Seleccione un genero"
                                                :items="['F', 'M']"
                                                item-title="name"
                                                variant="outlined"
                                                :rules="[v => !!v || 'Campo requerido']"
                                                class="mb-4 v-col-6"
                                                v-model="composable.viewModel.value.requestDetail.gender"
                                            />

                                            <v-file-input
                                                v-model="file"
                                                clearable
                                                label="Subir acta de nacimiento"
                                                variant="outlined"
                                                :rules="[v => !!v || 'Campo es requerido']"
                                                required
                                                @change="onUploadFile"
                                                accept=".jpg,.jpeg,.png,.pdf"
                                                class="mb-4 v-col-6">

                                            </v-file-input>

                                            <v-text-field
                                                v-model="composable.viewModel.value.requestDetail.birth"
                                                :rules="[v => !!v || 'Campo es requerido']"
                                                label="Fecha de nacimiento"
                                                required
                                                type="date"
                                                variant="outlined"
                                                prepend-inner-icon="mdi-email"
                                                class="mb-4 v-col-6"
                                            ></v-text-field>

                                        </v-row>


                                        <div class="d-flex flex-column">
                                            <v-btn class="mt-4" color="success" block @click="onValidationForm">
                                                Guardar
                                            </v-btn>
                                        </div>

                                    </v-form>

                                </v-sheet>
                            </v-card-text>
                        </template>
                    </CardView>

                </v-sheet>
            </v-col>
        </v-row>

        <dialog-view title="Alerta" description="¿Estas seguro de crear el usuario?" v-model="showModal" @accept="onSave" @close="showModal = false" />

    </v-container>
</template>

<script setup lang="ts">

import CardView from '@/ui/components/CardView.vue'
import DialogView from '@/ui/components/DialogView.vue'
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import useCreateUserComposable from '@/ui/composables/app/useCreateUserComposable'
import { useToast } from 'vue-toastification'
import enumRoute from '@/ui/router/enumRoute'
import { useAuthStore } from '@/ui/composables/localstorage/useAuthStore'
import useStorageComposable from '@/ui/composables/storage/useStorageComposable'

const form = ref()
const router = useRouter()
const userStore = useAuthStore()
const file = ref<File | null>(null);
const showModal = ref<boolean>(false)

const composable = useCreateUserComposable()
const composableStorage = useStorageComposable()

const onValidationForm = async () => {
    const { valid } = await form.value.validate()

    if (!valid) {
        useToast().error('Favor de llenar todos los campos')
    }

    if (valid) {
        showModal.value = true
    }
}

const onSave = async () => {
    showModal.value = false
    composable.viewModel.value.requestDetail.user_id = userStore.session!.id!
    const feedback = await composable.onSynDetail()

    if (feedback) {
        await router.push({name : enumRoute.USER_INDEX})
    }
}

const onUploadFile = async () => {
    console.log(file.value)
    if (file.value != null){
        const response = await composableStorage.onStore(file.value)

        if (response) {
            composable.viewModel.value.requestDetail.birth_document = composableStorage.viewModel.value.data[0]
        }
    }

}
</script>

<style scoped>

</style>
