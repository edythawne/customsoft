<script setup lang="ts">

import { type Ref, ref } from 'vue'
import { useRoute, useRouter } from 'vue-router'

const _route = useRoute()
const _router = useRouter()
const _drawer: Ref<boolean | undefined> = ref<boolean | undefined>()

const navigation = (name: string) => {
    _router.push({ name: name })
}

</script>

<template>

    <v-layout class="rounded rounded-md background">

        <v-navigation-drawer v-model="_drawer" temporary class="elevation-2">

            <v-list density="compact" nav>
                <v-list-item prepend-icon="mdi-home" title="Home" @click="navigation('Home')" />
                <v-list-item prepend-icon="mdi-scale-balance" title="Descargo de Responsabilidad"
                             @click="navigation('Descargo de Responsabilidad')" />
            </v-list>

        </v-navigation-drawer>

        <v-app-bar scroll-behavior="elevate" extension-height="100%">
            <template v-slot:prepend>
                <v-app-bar-nav-icon @click.stop="_drawer = !_drawer"></v-app-bar-nav-icon>
            </template>

            <v-toolbar-title>{{ _route.name }}</v-toolbar-title>
        </v-app-bar>

        <v-main>
            <div class="container mt-4">
                <router-view />
            </div>
        </v-main>
    </v-layout>
</template>
<style>

</style>
