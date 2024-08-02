import { defineStore } from 'pinia'
import type { Ref } from 'vue'
import type { UserModel } from '@/domain/model/auth/UserModel'
import { computed, ref } from 'vue'

export const useAuthStore = defineStore('auth', () => {

    const session : Ref<UserModel|null> = ref<UserModel>(localStorage.getItem('session') ? JSON.parse(localStorage.getItem('session') || '') : null)
    const isSessionActive = computed(() => session.value !== null)

    function onLogin(data : UserModel): void {
        try {
            session.value = data
            localStorage.setItem('session', JSON.stringify(data))
            localStorage.setItem('token', data.token ?? '')
        } catch (ex) {
            console.log(ex)
        }
    }

    function onLogout(): void {

    }

    function clearLocalStorage() {
        localStorage.removeItem('token')
        localStorage.removeItem('session')
    }

    return {
        session,
        clearLocalStorage,
        onLogin,
        onLogout,
        isSessionActive
    }

})
