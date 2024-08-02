import { ref } from 'vue'
import { Spinner } from '@/ui/components/Spinner'
import type { BaseModel } from '@/domain/model/BaseModel'
import StorageCase from '@/domain/cases/storage/StorageCase'

export interface StorageViewModel {
    data : string[]
}

export default function useStorageComposable() {

    const viewModel = ref<StorageViewModel>({
        data : []
    })

    const onStore = async (file : File) : Promise<boolean> => {
        Spinner.show()

        try {
            const response : BaseModel<string[]> = await StorageCase.store(file)

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
    }

    const onVisualizeAttachment = async (path: string) => {
        Spinner.show()

        try {
            await StorageCase.showInBrowser(path)
        } catch (e) {
            return e
        } finally {
            Spinner.hide()
        }
    }

    return {
        viewModel,
        onStore,
        onVisualizeAttachment
    }

}
