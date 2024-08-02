import type { BaseModel } from '@/domain/model/BaseModel'
import type { BaseResponse } from '@/data/response/BaseResponse'
import StorageRepository from '@/data/repository/StorageRepository'
import BaseModelIterator from '@/domain/iterator/BaseModelIterator'
import enumFileTypes from '@/ui/helper/enumFileTypes'

export default  {

    async store(file: File) : Promise<BaseModel<string[]>>{
        const response : BaseResponse<string[]> = await StorageRepository.store(file)

        if (response.data != null) {
            return BaseModelIterator.toModel<string[]>(response.message, response.data)
        }

        return BaseModelIterator.toModel("Error existe")
    },

    async showInBrowser(path: string ) {
        const response = await StorageRepository.show(path)
        const fileType: string = this.getFileType(path)

        const blob = new Blob([response], {
            type: enumFileTypes[fileType]
        })

        const link = document.createElement('a')
        link.target = '_blank'
        link.href = URL.createObjectURL(blob)
        link.click()
    },

    getFileType(fileName: string) {
        const splitName = fileName.toLowerCase().split('.')
        return splitName[splitName.length - 1] as string
    }

}
