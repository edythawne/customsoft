import { ref } from 'vue'
import type { LoginRequest } from '@/data/request/Auth/LoginRequest'
import AuthRepository from '@/data/repository/AuthRepository'

export interface LoginViewModel {
    email : string;
    password : string;
}

export default function useLoginComposable() {

    const viewModel = ref<LoginViewModel>({
        email : '',
        password : ''
    })


    const onLogin = async () => {
        const x : LoginRequest = { email : viewModel.value.email, password : viewModel.value.password } as LoginRequest
        const response = await AuthRepository.login(x);
        console.log(response)
    }

    return {
        viewModel,
        onLogin
    }

}
