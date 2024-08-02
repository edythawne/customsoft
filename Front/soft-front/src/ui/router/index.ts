import { createRouter, createWebHistory } from 'vue-router'
import route from '@/ui/router/route'
import { useAuthStore } from '@/ui/composables/localstorage/useAuthStore'
import api from '@/data/api'
import enumRoute from '@/ui/router/enumRoute'
import RoleHelper from '@/ui/helper/RoleHelper'

const router  = createRouter({
  history : createWebHistory(),
  routes : route
})

router.beforeEach(async (to) => {
    const authStore = useAuthStore()
    const userRoles = authStore.session?.roles || [];
    const token = localStorage.getItem('token') ?? ''

    if (token){
        api.defaults.headers.common.Authorization = `Bearer ${token}`
    }

    if (!authStore.session && to.name !== enumRoute.LOGIN){
        await router.push({name : enumRoute.LOGIN})
    }

    if (authStore.session && to.name === enumRoute.LOGIN){

        if (RoleHelper.isAdmin()){
            await router.push({name : enumRoute.ADMIN_HOME})
        }

        if (!RoleHelper.isAdmin()) {
            await router.push({name : enumRoute.USER_INDEX})
        }
    }

    if (to.meta.roles && !RoleHelper.someOneRole(Array.isArray(to.meta.roles) ? to.meta.roles : [to.meta.roles])) {
        // Redirige si el usuario no tiene el rol necesario
        await router.push({ name: enumRoute.ACCESS_DENIED });
    }

    return
})

export default router
