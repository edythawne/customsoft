import type { RoleModel } from '@/domain/model/auth/RoleModel'
import { useAuthStore } from '@/ui/composables/localstorage/useAuthStore'
import enumRole from '@/ui/helper/enumRole'

export default {

    hasRole(roles: RoleModel[], roleName: string): boolean {
        return roles.some(role => role.name === roleName);
    },


    isAdmin(): boolean {
        const auth = useAuthStore()
        const roles: RoleModel[] = auth.session?.roles!
        return this.hasRole(roles, enumRole.ADMIN)
    },

    someOneRole(roles: string[]) : boolean {
        const auth = useAuthStore()
        const userRoles: string[] = auth.session!.roles!.map((x) => x.name)

        return roles.some(item => userRoles.includes(item))
    }

}
