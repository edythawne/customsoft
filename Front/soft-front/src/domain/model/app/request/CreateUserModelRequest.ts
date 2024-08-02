import type { DepartmentModel } from '@/domain/model/catalog/DepartmentModel'
import type { RoleModel } from '@/domain/model/auth/RoleModel'

export interface CreateUserModelRequest {
    name :          string;
    first_name :    string;
    email :         string;
    department :    DepartmentModel | null;
    role :          RoleModel | null;
}
