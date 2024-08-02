<template>

    <v-container>
        <v-row no-gutters>
            <v-col align-self="center">
                <v-sheet class="pa-2 ma-2">

                    <v-sheet class="d-flex flex-wrap mb-4">
                        <v-sheet class="flex-1-0">

                        </v-sheet>

                        <v-sheet class="">
                            <v-btn prepend-icon="mdi-plus" @click="router.push({name: enumRoute.USER_SYNC_DETAIL})" v-if="!RoleHelper.isAdmin()">
                                Actualizar detalle
                            </v-btn>
                        </v-sheet>
                    </v-sheet>

                    <div v-if="composable.viewModel.value.data != null">

                        <CardView title="Información del Usuario" class="mb-4">
                            <template #default>
                                <v-card-text>
                                    <v-sheet>

                                        <v-row>

                                            <v-col cols="4">
                                                <p><b>Nombre</b></p>
                                                <p>{{composable.viewModel.value.data?.name}}</p>
                                            </v-col>

                                            <v-col cols="4">
                                                <p><b>Apellidos</b></p>
                                                <p>{{composable.viewModel.value.data?.last_name}}</p>
                                            </v-col>

                                            <v-col cols="4">
                                                <p><b>Emnail</b></p>
                                                <p>{{composable.viewModel.value.data?.email}}</p>
                                            </v-col>

                                            <v-col cols="4">
                                                <p><b>Curp</b></p>
                                                <p>{{composable.viewModel.value.data?.detail?.curp ?? '-'}}</p>
                                            </v-col>

                                            <v-col cols="4">
                                                <p><b>Genero</b></p>
                                                <p>{{ composable.viewModel.value.data?.detail?.gender == 'F' ? 'Femenino' : 'Masculino'   }}</p>
                                            </v-col>

                                            <v-col cols="4">
                                                <p><b>Fecha de nacimiento</b></p>
                                                <p>{{ composable.viewModel.value.data?.detail?.date_of_birth ?? '-' }}</p>
                                            </v-col>

                                            <v-col cols="4" v-if="composable.viewModel.value.data?.detail">
                                                <p><b>Acta de Nacimiento</b></p>
                                                <ShowFile :path="composable.viewModel.value.data?.detail?.birth_certificate" />
                                            </v-col>

                                        </v-row>

                                    </v-sheet>
                                </v-card-text>
                            </template>
                        </CardView>


                        <CardView title="Información empresarial">
                            <template #default>
                                <v-card-text>
                                    <v-sheet>

                                        <v-row>

                                            <v-col cols="4">
                                                <p><b>Departamento</b></p>
                                                <p>{{composable.viewModel.value.data?.department.name}}</p>
                                            </v-col>

                                            <v-col cols="4">
                                                <p><b>ÁREA</b></p>
                                                <p>{{ composable.viewModel.value.data?.department.area?.name ?? '-' }}</p>
                                            </v-col>

                                            <v-col cols="4">
                                                <p><b>Role</b></p>
                                                <p>{{ composable.viewModel.value.data?.roles?.map((item : RoleModel) => item.name)?.join(', ') }}</p>
                                            </v-col>

                                        </v-row>


                                    </v-sheet>
                                </v-card-text>
                            </template>
                        </CardView>

                    </div>

                    <div v-else>
                        <CardView title="Información del Usuario" class="mb-4">
                            <template #default>
                                <v-card-text>
                                    <v-sheet>
                                        El usuario no existe
                                    </v-sheet>
                                </v-card-text>
                            </template>
                        </CardView>
                    </div>




                </v-sheet>
            </v-col>
        </v-row>

    </v-container>

</template>

<script setup lang="ts">

import CardView from '@/ui/components/CardView.vue'
import useGetUserComposable from '@/ui/composables/app/useGetUserComposable'
import { useRoute, useRouter } from 'vue-router'
import type { RoleModel } from '@/domain/model/auth/RoleModel'
import enumRoute from '@/ui/router/enumRoute'
import { useAuthStore } from '@/ui/composables/localstorage/useAuthStore'
import ShowFile from '@/ui/components/ShowFile.vue'
import RoleHelper from '@/ui/helper/RoleHelper'

const route =  useRoute()
const router =  useRouter()
const userStore = useAuthStore()
const composable = useGetUserComposable()


const init = async () => {
    const routeName = route.name

    if (routeName == enumRoute.USER_INDEX) {
        const id = userStore.session!.id!
        await composable.onGetUserById(id)
    } else {
        await composable.onGetUserById(+route.params.id)
    }
}

init()

</script>

<style scoped>

</style>
