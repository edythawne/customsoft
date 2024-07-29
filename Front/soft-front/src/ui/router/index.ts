import { createRouter, createWebHistory } from 'vue-router'
import route from '@/ui/router/route'

const router  = createRouter({
  history : createWebHistory(),
  routes : route
})

export default router
