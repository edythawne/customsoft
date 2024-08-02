import type { UserDetailResponse } from '@/data/response/app/UserDetailResponse'
import type { UserDetailModel } from '@/domain/model/app/UserDetailModel'
import DateHelper from '@/domain/helper/DateHelper'
import type { CreateUserDetailRequest } from '@/data/request/app/CreateUserDetailRequest'
import type { CreateUserDetailModelRequest } from '@/domain/model/app/request/CreateUserDetailModelRequest'

export default {

    responseToModel(response: UserDetailResponse): UserDetailModel {
        return {
            id: response.id,
            curp: response.curp,
            gender: response.gender,
            user_id: response.userId,
            created_at: DateHelper.fullDateString(response.createdAt),
            date_of_birth: DateHelper.fullDateString(response.dateOfBirth),
            place_of_birth: response.placeOfBirth,
            birth_certificate: response.birthCertificate
        };
    },

    requestModelToRequest(request : CreateUserDetailModelRequest): CreateUserDetailRequest {
        return {
            curp : request.curp,
            gender : request.gender,
            userId : request.user_id,
            birthCertificate : request.birth_document,
            dateOfBirth : request.birth,
            placeOfBirth : request.place
        } as CreateUserDetailRequest
    }

}
