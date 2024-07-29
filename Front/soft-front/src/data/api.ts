import axios from 'axios'

const token = JSON.parse(localStorage.getItem('token') as string) ?? ''

const api = axios.create({
  baseURL: import.meta.env.VITE_BACKEND_URL
})

api.defaults.headers.common.Authorization = `Bearer ${token}`

export default api
