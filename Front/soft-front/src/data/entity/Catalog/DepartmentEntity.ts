import type { AreaEntity } from '@/data/entity/Catalog/AreaEntity'

export interface DepartmentEntity {
    id:         number;
    area_id:    number;
    name:       string;
    created_at: Date;
    area:       AreaEntity;
}
