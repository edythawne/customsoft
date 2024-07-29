const routePublic = [
    {
        path: '/',
        name: 'index',
        component: () => import('@/ui/pages/public/index.vue'),
        children: [
            {
                path: '/',
                name: 'Home',
                component: () => import('@/ui/pages/public/login/home.vue')
            }
        ]
    }
]

export default routePublic
