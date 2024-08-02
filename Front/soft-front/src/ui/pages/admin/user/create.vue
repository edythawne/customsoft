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

                                        <v-text-field
                                            v-model="composable.viewModel.value.request.name"
                                            :rules="[v => !!v || 'Nombre es requerido']"
                                            label="Nombre"
                                            required
                                            type="text"
                                            variant="outlined"
                                            prepend-inner-icon="mdi-email"
                                            class="mb-4"
                                        ></v-text-field>

                                        <v-text-field
                                            v-model="composable.viewModel.value.request.first_name"
                                            :rules="[v => !!v || 'Apellidos son requeridos']"
                                            label="Apellidos"
                                            required
                                            type="text"
                                            variant="outlined"
                                            prepend-inner-icon="mdi-email"
                                            class="mb-4"
                                        ></v-text-field>

                                        <v-text-field
                                            v-model="composable.viewModel.value.request.email"
                                            :rules="[v => !!v || 'Correo requerido']"
                                            label="Correo Electrónico"
                                            required
                                            type="email"
                                            variant="outlined"
                                            prepend-inner-icon="mdi-email"
                                            class="mb-4"
                                        ></v-text-field>


                                        <v-select
                                            label="Seleccione un rol a asignar"
                                            :items="composableRole.viewModel.value.data"
                                            item-title="name"
                                            variant="outlined"
                                            :rules="[v => !!v || 'Campo requerido']"
                                            class="mb-4"
                                            return-object
                                            v-model="composable.viewModel.value.request.role"
                                        />

                                        <v-select
                                            label="Seleccione un departamento a asignar"
                                            :items="composableDepartment.viewModel.value.data"
                                            item-title="name"
                                            variant="outlined"
                                            :rules="[v => !!v || 'Campo requerido']"
                                            class="mb-4"
                                            return-object
                                            v-model="composable.viewModel.value.request.department" />

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
import useCreateUserComposable from '@/ui/composables/app/useCreateUserComposable'
import useRoleComposable from '@/ui/composables/auth/useRoleComposable'
import useDepartmentComposable from '@/ui/composables/catalog/useDepartmentComposable'
import DialogView from '@/ui/components/DialogView.vue'
import { ref } from 'vue'
import { useToast } from 'vue-toastification'
import enumRoute from '@/ui/router/enumRoute'
import { useRouter } from 'vue-router'


const form = ref()
const router = useRouter()
const showModal = ref<boolean>(false)

const composableRole = useRoleComposable()
const composable = useCreateUserComposable()
const composableDepartment = useDepartmentComposable()


await composableRole.onGetAll()
await composableDepartment.onGetAll()


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
    const feedback = await composable.onCreateUser()

    if (feedback) {
        await router.push({name : enumRoute.ADMIN_HOME})
    }
}

</script>

<style scoped>

</style>
