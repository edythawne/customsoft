import type { RoleModel } from '@/domain/model/auth/RoleModel'
import type { DepartmentModel } from '@/domain/model/catalog/DepartmentModel'
import type { UserDetailModel } from '@/domain/model/app/UserDetailModel'

export interface UserModel {
    id:             number;
    name:           string;
    last_name:       string;
    email:          string;
    token?:         string;
    created_at:     string;
    roles?:         RoleModel[];
    department:     DepartmentModel;
    detail?:        UserDetailModel;
}
