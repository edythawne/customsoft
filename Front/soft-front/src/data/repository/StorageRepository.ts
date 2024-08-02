import type { BaseResponse } from '@/data/response/BaseResponse'
import api from '@/data/api'
import type { AxiosResponse } from 'axios'

export default {

    store : function(file: File) : Promise<BaseResponse<string[]>> {
        const formData = new FormData();
        formData.append('Files', file);
        formData.append('Folder', 'soft');
        formData.append('IsRename', 'false');

        return new Promise((resolve, reject) => {
            api.post("Storage/store", formData )
                .then((axiosResponse: AxiosResponse) => {
                    const response : BaseResponse<string[]> = axiosResponse.data
                    resolve(response)
                }).catch((error) => reject(error))
        })
    },

    async show(path : string): Promise<any> {
        return new Promise((resolve, reject) => {
            api.get(`Storage/download?path=${path}`, {
                responseType: 'blob',
            }).then((response: AxiosResponse) => {
                resolve(response.data)
            }).catch((error) => reject(error))
        })
    },

}
