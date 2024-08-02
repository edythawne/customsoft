import type { BaseResponse } from '@/data/response/BaseResponse'
import api from '@/data/api'
import type { AxiosResponse } from 'axios'
import type { DepartmentResponse } from '@/data/response/catalog/DepartmentResponse'

export default {

    getAllDepartment : function() : Promise<BaseResponse<DepartmentResponse[]>> {
        return new Promise((resolve, reject) => {
            api.get("Department/get/all")
                .then((axiosResponse: AxiosResponse) => {
                    const response : BaseResponse<DepartmentResponse[]> = axiosResponse.data
                    resolve(response)
                }).catch((error) => reject(error))
        })
    }

}
