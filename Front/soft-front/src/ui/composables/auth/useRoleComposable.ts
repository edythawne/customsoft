import type { RoleModel } from '@/domain/model/auth/RoleModel'
import { ref } from 'vue'
import { Spinner } from '@/ui/components/Spinner'
import type { BaseModel } from '@/domain/model/BaseModel'
import type { DepartmentModel } from '@/domain/model/catalog/DepartmentModel'
import DepartmentCase from '@/domain/cases/catalog/DepartmentCase'
import RoleCase from '@/domain/cases/app/RoleCase'

export interface RoleViewModel {
    data : RoleModel[]
}

export default function useRoleComposable() {

    const viewModel = ref<RoleViewModel>({
        data : []
    })

    const onGetAll = async () : Promise<boolean> => {
        Spinner.show()

        try {
            const response : BaseModel<RoleModel[]> = await RoleCase.getAll()
            console.log(response)

            if (response.data == null) {
                return false
            }

            viewModel.value.data = response.data
            return true
        } catch (e) {
            console.log(e)
            return false
        } finally {
            Spinner.hide()
        }

        return false
    }

    return {
        viewModel,
        onGetAll
    }

}
