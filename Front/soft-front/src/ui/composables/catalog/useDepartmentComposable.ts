import type { DepartmentModel } from '@/domain/model/catalog/DepartmentModel'
import { ref } from 'vue'
import { Spinner } from '@/ui/components/Spinner'
import type { BaseModel } from '@/domain/model/BaseModel'
import DepartmentCase from '@/domain/cases/catalog/DepartmentCase'

export interface DepartmentViewModel {
    data : DepartmentModel[]
}

export default function useDepartmentComposable() {

    const viewModel = ref<DepartmentViewModel>({
        data : []
    })

    const onGetAll = async () : Promise<boolean> => {
        Spinner.show()

        try {
            const response : BaseModel<DepartmentModel[]> = await DepartmentCase.getAll()

            if (response.data == null) {
                return false
            }

            viewModel.value.data = response.data!
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
