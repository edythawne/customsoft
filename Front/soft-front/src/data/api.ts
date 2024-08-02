import axios from 'axios'
import { useAuthStore } from '@/ui/composables/localstorage/useAuthStore'
import { useToast } from 'vue-toastification'

const token = localStorage.getItem('token') ?? ''

const api = axios.create({
    baseURL: import.meta.env.VITE_BACKEND_URL
})

api.defaults.headers.common.Authorization = `Bearer ${token}`

// Interceptores de errores
api.interceptors.response.use((response) => {
    return response
}, async (error) => {
    const unauthorizedCode = 401
    const validationCode = 400

    if (error.response.status === unauthorizedCode) {
        useToast().warning("Su sesión ha caducado")

        const userStore = useAuthStore()
        userStore.clearLocalStorage()

        //const router = useRouter();
        //await router.push({name : enumRoute.LOGIN})
        window.location.reload()
        return
    }

    if (error.response.status === validationCode){
        useToast().error("La información proporcionada es invalida")
    }

    return Promise.reject(error)
})

export default api
