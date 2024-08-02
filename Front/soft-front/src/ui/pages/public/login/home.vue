<template>

    <v-container>
        <v-row no-gutters>
            <v-col align-self="center">
                <v-sheet class="pa-2 ma-2">

                    <CardView title="Bienvenido">
                        <template #default>
                            <v-card-text>
                                <v-sheet>

                                    <v-form ref="form" class="pa-4">
                                        <v-text-field
                                            v-model="composable.viewModel.value.request.email"
                                            :rules="[v => !!v || 'Name is required']"
                                            label="Email"
                                            required
                                            type="email"
                                            variant="outlined"
                                            prepend-inner-icon="mdi-email"
                                            class="mb-4"
                                        ></v-text-field>

                                        <v-text-field
                                            v-model="composable.viewModel.value.request.password"
                                            :rules="[v => !!v || 'Name is required']"
                                            label="Contraseña"
                                            required
                                            type="password"
                                            variant="outlined"
                                            prepend-inner-icon="mdi-lock"
                                        ></v-text-field>

                                        <div class="d-flex flex-column">
                                            <v-btn class="mt-4" color="success" block @click="onValidationForm">
                                                Inicio de Sesión
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
    </v-container>

</template>

<script setup lang="ts">

import useLoginComposable from '@/ui/composables/auth/useLoginComposable'
import { ref } from 'vue'
import { useToast } from 'vue-toastification'
import enumRoute from '@/ui/router/enumRoute'
import CardView from '@/ui/components/CardView.vue'
import { useRouter } from 'vue-router'
import RoleHelper from '@/ui/helper/RoleHelper'

const form = ref()
const composable = useLoginComposable()

const router = useRouter()

const onValidationForm = async () => {
    const { valid } = await form.value.validate()

    if (!valid) {
        useToast().error('Favor de llenar todos los campos')
    }

    if (valid) {
        const feedback = await composable.onLogin()

        if (feedback) {

            if (RoleHelper.isAdmin()){
                await router.push({name : enumRoute.ADMIN_HOME})
            }

            if (!RoleHelper.isAdmin()) {
                await router.push({name : enumRoute.USER_INDEX})
            }

        }
    }
}


</script>

<style scoped>

</style>
