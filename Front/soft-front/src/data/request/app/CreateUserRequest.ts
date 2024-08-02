
export interface CreateUserRequest {
    name:         string;
    firstName:    string;
    email:        string;
    fkDepartment: number;
    fkRol:        number;
}
