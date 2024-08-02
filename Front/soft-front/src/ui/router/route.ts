import enumRoute from '@/ui/router/enumRoute'
import enumRole from '@/ui/helper/enumRole'

const routePublic = [
    {
        path: '/',
        name: '',
        component: () => import('@/ui/pages/public/index.vue'),
        children: [
            {
                path: '/',
                name:  enumRoute.LOGIN,
                component: () => import('@/ui/pages/public/login/home.vue')
            },
            {
                path: '/admin',
                name:  enumRoute.ADMIN_HOME,
                component: () => import('@/ui/pages/admin/index.vue'),
                meta: {
                    requiredAuth: true,
                    pageTitle: '',
                    roles : [enumRole.ADMIN]
                }
            },
            {
                path: '/admin/create-user',
                name:  enumRoute.ADMIN_CREATE_USER,
                component: () => import('@/ui/pages/admin/user/create.vue'),
                meta: {
                    requiredAuth: true,
                    pageTitle: '',
                    roles : [enumRole.ADMIN]
                }
            },
            {
                path: '/user/detail-user/:id',
                name:  enumRoute.USER_DETAIL,
                component: () => import('@/ui/pages/admin/user/detail.vue'),
                meta: {
                    requiredAuth: true,
                    pageTitle: '',
                    roles : [enumRole.ADMIN, enumRole.USER]
                }
            },
            {
                path: '/user/index',
                name:  enumRoute.USER_INDEX,
                component: () => import('@/ui/pages/admin/user/detail.vue'),
                meta: {
                    requiredAuth: true,
                    pageTitle: '',
                    roles : [enumRole.USER]
                }
            },
            {
                path: '/user/detail/sync',
                name:  enumRoute.USER_SYNC_DETAIL,
                component: () => import('@/ui/pages/user/sync_detail.vue'),
                meta: {
                    requiredAuth: true,
                    pageTitle: '',
                    roles : [enumRole.USER]
                }
            },
            {
                path: 'access-denied',
                component: () => import('@/ui/pages/deny.vue'),
                name: enumRoute.ACCESS_DENIED,
                meta: {
                    requiredAuth: false,
                    pageTitle: 'Acceso restringido',
                },
            },
            {
                path: '/:catchAll(.*)*',
                component: () => import('@/ui/pages/lose.vue'),
            },
        ]
    }
]

export default routePublic
