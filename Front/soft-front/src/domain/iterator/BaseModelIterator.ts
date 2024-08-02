import type { BaseModel } from '@/domain/model/BaseModel'

export default {

    toModel<T>(message : string, model?: T): BaseModel<T> {
        return {
            message : message,
            data : model
        }
    }

}
