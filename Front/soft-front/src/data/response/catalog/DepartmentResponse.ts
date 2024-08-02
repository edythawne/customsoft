import type { AreaResponse } from '@/data/response/catalog/AreaResponse'

export interface DepartmentResponse {
    id:             number;
    areaId:         number;
    name:           string;
    createdAt:      string;
    area?:          AreaResponse;
}
