<template>
    <v-container class="pa-0">

        <CardView title="Lista de Usuarios">
            <template #default>
                <v-card-text>
                    <v-sheet class="d-flex flex-wrap bg-surface-variant">
                        <v-sheet class="flex-1-0">
                            <v-row>
                                <v-col cols="4">
                                    <v-text-field
                                        v-model="composable.viewModel.value.search"
                                        label="Filtrar por nombre"
                                        type="text"
                                        variant="outlined"
                                        prepend-inner-icon="mdi-magnify"
                                        class="mb-4"
                                        density="compact"
                                        @keyup.enter="onRefresh"
                                        :clearable="true"
                                        :on-click:clear="onRefresh"
                                    ></v-text-field>
                                </v-col>
                            </v-row>
                        </v-sheet>

                        <v-sheet class="">
                            <v-btn prepend-icon="mdi-plus" @click="router.push({name: enumRoute.ADMIN_CREATE_USER})">
                                Crear un nuevo usuario
                            </v-btn>
                        </v-sheet>
                    </v-sheet>
                </v-card-text>

                <v-card-text>
                    <v-sheet>

                        <Paginator :columns="columns" :rows="composable.viewModel.value.rows!" :pagination="composable.viewModel.value.pagination" @page-changed="args => composable.onUserPaginated(args)">
                            <template #item-roles="slotData">
                                {{slotData.item.row.roles?.map((item : RoleModel) => item.name)?.join(', ') }}
                            </template>
                            <template #item-department="slotData">
                                Área: {{slotData.item.row.department.area.name}}
                                <br>
                                Departamento : {{slotData.item.row.department.name}}
                            </template>
                            <template #item-actions="slotData">

                                <v-menu>
                                    <template v-slot:activator="{ props }">
                                        <v-btn icon="mdi-dots-vertical" height="40" variant="text" width="40" border  v-bind="props"/>
                                    </template>

                                    <v-list>
                                        <v-list-item v-for="(item, index) in slotData.item.row.actions" :key="index">
                                            <v-list-item-title @click="item.action">{{item.label}}</v-list-item-title>
                                        </v-list-item>
                                    </v-list>
                                </v-menu>



                            </template>
                        </Paginator>

                    </v-sheet>

                </v-card-text>
            </template>
        </CardView>

    </v-container>
</template>

<script setup lang="ts">

import Paginator from '@/ui/components/PaginatorView.vue'
import type { TableColumn } from '@/domain/model/helper/TableColumn'
import { ref } from 'vue'
import usePaginatedComposable from '@/ui/composables/app/usePaginatedComposable'
import type { RoleModel } from '@/domain/model/auth/RoleModel'
import { useRouter } from 'vue-router'
import enumRoute from '@/ui/router/enumRoute'
import CardView from '@/ui/components/CardView.vue'

const router = useRouter()
const composable = usePaginatedComposable()

const columns = ref<TableColumn[]>([
    { name : 'name', label : 'Nombre', align : 'center'},
    { name : 'email', label : 'Correo', align : 'center'},
    { name : 'roles', label : 'Role Asignado', align : 'center'},
    { name : 'department', label : 'Área', align : 'center'},
    { name : 'created_at', label : 'Fecha de Registro', align : 'center'},
    { name : 'actions', label : 'Acciones', align : 'center'},
])

const onRefresh = () => {
    composable.onUserPaginated(1)
}

composable.onUserPaginated()

</script>

<style scoped>

</style>
