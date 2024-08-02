import type { AreaModel } from '@/domain/model/catalog/AreaModel'

export interface DepartmentModel {
    id:             number;
    areaId:         number;
    name:           string;
    createdAt:      string;
    area?:          AreaModel;
}
