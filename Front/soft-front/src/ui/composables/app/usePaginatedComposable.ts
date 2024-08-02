import { ref } from 'vue'
import UserCase from '@/domain/cases/app/UserCase'
import type { BasePaginationModel } from '@/domain/model/BasePaginationModel'
import type { UserModel } from '@/domain/model/auth/UserModel'
import type { PaginationModel } from '@/domain/model/PaginationModel'
import { Spinner } from '@/ui/components/Spinner'
import { useRouter } from 'vue-router'
import enumRoute from '@/ui/router/enumRoute'

export interface IndexViewModel {
    search :            string
    rows? :             UserModel[],
    pagination? :       PaginationModel
}


export default function usePaginatedComposable() {
    const viewModel = ref<IndexViewModel>({
        search :    '',
        rows :      []
    })

    const router = useRouter()

    const onUserPaginated = async (page: number = 1) => {
        Spinner.show()

        try {
            const response : BasePaginationModel<UserModel[]> = await UserCase.getUserPaginated(page, viewModel.value.search)

            if (response.data != null) {
                viewModel.value.rows = response.data
                onSetAction(response.data)
                viewModel.value.pagination = response.pagination
            }
        } catch (e) {
            console.log(e)
        } finally {
            Spinner.hide()
        }
    }

    const onSetAction = (data : UserModel[]) : void => {
        data.map((item: any) => {

            item.actions = [
                {
                    label: 'Ver detalle',
                    action: () => {
                        router.push({name : enumRoute.USER_DETAIL, params : { id : item.id }})
                    },
                },
            ]

        })
    }

    return {
        viewModel,
        onUserPaginated
    }

}
