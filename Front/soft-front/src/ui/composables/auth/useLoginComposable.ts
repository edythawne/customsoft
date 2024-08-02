import type { LoginModelRequest } from '@/domain/model/auth/request/LoginModelRequest'
import AuthCase from '@/domain/cases/auth/AuthCase'
import { ref } from 'vue'
import type { BaseModel } from '@/domain/model/BaseModel'
import type { UserModel } from '@/domain/model/auth/UserModel'
import { useToast } from 'vue-toastification'
import { Spinner } from '@/ui/components/Spinner'
import { useAuthStore } from '@/ui/composables/localstorage/useAuthStore'

export interface LoginViewModel {
    request : LoginModelRequest
}

export default function useLoginComposable() {

    const viewModel = ref<LoginViewModel>({
        request : {
            email : '',
            password : ''
        }
    })

    const onLogin = async () : Promise<boolean> => {
        Spinner.show()

        try {
            const response : BaseModel<UserModel> = await AuthCase.login(viewModel.value.request)

            if(response.data != null) {
                useToast().info(response.message)
                useAuthStore().onLogin(response.data)
                return true
            }

            if (response.data == null) {
                useToast().warning(response.message)
                viewModel.value.request.password = ''
                return false
            }

        } catch (ex) {
            console.log(ex)
            return false
        } finally {
            Spinner.hide()
        }

        return false
    }

    return {
        viewModel,
        onLogin
    }

}
