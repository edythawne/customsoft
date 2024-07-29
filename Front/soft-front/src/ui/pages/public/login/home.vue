<template>

    <v-card title="Bienvenido" subtitle="Favor de ingresar sus credenciales" width="400">
        <form @submit.prevent="composable.onLogin">
            <v-text-field
                v-model="composable.viewModel.value.email"
                label="Correo electronico"
                type="email"
                :rules="[(value: string) => value != null && value  != ''  || 'Campo obligatorio']"
            ></v-text-field>

            <v-text-field
                v-model="composable.viewModel.value.password"
                :rules="[ (value: string) => value != null && value  != '' || 'Campo obligatorio']"
                type="password"
                label="Contraseña"
            ></v-text-field>

            <v-btn type="submit">
                Iniciar Sesion
            </v-btn>
        </form>
    </v-card>

</template>

<script setup lang="ts">

import useLoginComposable from '@/ui/pages/public/login/useLoginComposable'

const composable = useLoginComposable()

</script>

<style scoped>

</style>
