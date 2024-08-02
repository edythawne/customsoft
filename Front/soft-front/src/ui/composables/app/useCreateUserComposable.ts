import type { CreateUserModelRequest } from '@/domain/model/app/request/CreateUserModelRequest'
import { ref } from 'vue'
import { Spinner } from '@/ui/components/Spinner'
import UserCase from '@/domain/cases/app/UserCase'
import { useToast } from 'vue-toastification'
import type { BaseModel } from '@/domain/model/BaseModel'
import type { CreateUserDetailModelRequest } from '@/domain/model/app/request/CreateUserDetailModelRequest'

export interface CreateUserViewModel {
    request : CreateUserModelRequest,
    requestDetail : CreateUserDetailModelRequest
}

export default function useCreateUserComposable() {

    const viewModel =  ref<CreateUserViewModel>({
        request : {
            email : '',
            name : '',
            first_name : '',
            department : null,
            role : null
        },
        requestDetail : {
            user_id : 0,
            birth : '',
            curp  : '',
            birth_document : '',
            gender : '',
            place : ''
        }
    })

    const onCreateUser = async() : Promise<boolean> => {
        Spinner.show()

        try {
            const response  : BaseModel<number> = await UserCase.newUser(viewModel.value.request)

            if(response.data != null) {
                useToast().info(response.message)
                return true
            }

            if (response.data == null) {
                useToast().warning(response.message)
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

    const onSynDetail = async() : Promise<boolean> => {
        Spinner.show()

        try {
            const response : BaseModel<number> = await UserCase.syncUserDetail(viewModel.value.requestDetail)

            if(response.data != null) {
                useToast().info(response.message)
                return true
            }

            if (response.data == null) {
                useToast().warning(response.message)
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
        onCreateUser,
        onSynDetail
    }

}
