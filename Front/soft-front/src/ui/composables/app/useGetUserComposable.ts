import { ref } from 'vue'
import UserCase from '@/domain/cases/app/UserCase'
import type { UserModel } from '@/domain/model/auth/UserModel'
import { Spinner } from '@/ui/components/Spinner'
import type { BaseModel } from '@/domain/model/BaseModel'
import { useToast } from 'vue-toastification'

export interface GetUserViewModel {
    data : UserModel|null;
}


export default function useGetUserComposable() {
    const viewModel = ref<GetUserViewModel>({
        data : null
    })

    const onGetUserById = async (id: number) => {
        Spinner.show()

        try {
            const response : BaseModel<UserModel> = await UserCase.getUserById(id)

            if (response.data != null) {
                viewModel.value.data = response.data
                return true
            }

            useToast().warning(response.message)
            return false
        } catch (e) {
            console.log(e)
        } finally {
            Spinner.hide()
        }

        return false
    }

    return {
        viewModel,
        onGetUserById
    }

}
