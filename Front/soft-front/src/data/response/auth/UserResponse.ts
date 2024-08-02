import type { DepartmentResponse } from '@/data/response/catalog/DepartmentResponse'
import type { RoleResponse } from '@/data/response/auth/RoleResponse'
import type { UserDetailResponse } from '@/data/response/app/UserDetailResponse'

export interface UserResponse {
    id:                 number;
    name:               string;
    lastName:          string;
    email:              string;
    token:              string;
    createdAt:         string;
    roles?:             RoleResponse[];
    department:         DepartmentResponse;
    detail?:            UserDetailResponse;
}
