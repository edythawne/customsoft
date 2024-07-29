import type { DepartmentEntity } from '@/data/entity/Catalog/DepartmentEntity'
import type { RoleEntity } from '@/data/entity/Auth/RoleEntity'

export interface UserEntity {
    id:         number;
    name:       string;
    last_name:  string;
    email:      string;
    token:      null;
    created_at: Date;
    roles:      RoleEntity[];
    department: DepartmentEntity;
}
