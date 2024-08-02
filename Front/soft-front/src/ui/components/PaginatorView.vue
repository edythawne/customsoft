<template>
    <v-table class="table">
        <thead>
        <tr>
            <th v-for="(column, index) in columns" :key="index" :title="column?.label">
                <slot :name="`header-${column.name}`">

                    <div :class="index == 0 ? 'flex' :''">
                        <div class="col" :style="`text-align: ${column.align}`">
                            <b>{{ column?.label }}</b>
                        </div>
                    </div>

                </slot>
            </th>
        </tr>
        </thead>
        <tbody v-if="localRows && localRows?.length">
            <tr v-for="(row, index) in localRows" :key="index" :class="index % 2 === 1 ? 'white' : 'grey-lighten-4'">

                <slot :name="`row-item-${row.name}`" :item="row">

                    <td v-for="(value, key) in columns" :key="key" class="text-center truncate" :style="`text-align: ${value.align}`">
                        <slot :name="`item-${value.name}`" :item="{ value, key, row, index }">
                            {{ row[value.name] }}
                        </slot>
                    </td>
                </slot>

            </tr>
        </tbody>
        <tbody v-else>
            <tr>
                <td :colspan="columns?.length" class="no-results">
                    Ningún resultado encontrado
                </td>
            </tr>
        </tbody>
    </v-table>

    <div v-if="pagination">
        <v-pagination :length="pagination.lastPage" v-model="current"></v-pagination>
    </div>

</template>

<script setup lang="ts">

import { ref, watch, watchEffect } from 'vue'
import type { TableColumn } from '@/domain/model/helper/TableColumn'
import type { PaginationModel } from '@/domain/model/PaginationModel'

export interface Props {
    columns: TableColumn[];
    rows: Record<string | number, any>[]|[];
    pagination?: PaginationModel
}

const props = defineProps<Props>()
const emits = defineEmits(['page-changed'])

const current = ref(1)
const localRows = ref<Record<string | number, any>[]>([])

watchEffect(() => {
    localRows.value = props.rows
})

watch(current, (current) => emits('page-changed', current))

</script>


<style scoped>
.truncate {
    min-width: 120px;
    max-width: 250px;
    white-space: break-spaces;
}

.table-main-container {
    width: 100%;
    padding: 12px 0;
    overflow-x: auto;

    table {
        border-collapse: collapse;
        width: 100%;
        background-color: white;
        overflow: hidden;

        .table-header-row {
            height: 48px;
        }

        thead tr th {
            height: 48px;
            border-bottom: solid 1px #D2D6DA;
        }

        tbody tr td {
            height: 48px;
        }
    }
}

.paginator-show {
    margin-top: 30px;
    position: absolute;
    z-index: 10;
}

.paginator-slot {
    margin-top: 24px;
    display: flex;
    justify-content: flex-end;
    align-self: flex-end;
}

:deep(.q-btn--outline:before) {
    border: 1px solid #D2D6DA;
}
</style>
