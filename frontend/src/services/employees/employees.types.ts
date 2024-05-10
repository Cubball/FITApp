export interface IEmployeeShortInfo {
  id: string;
  firstName: string;
  lastName: string;
  patronymic: string;
  email: string;
  role: string;
}

export interface IEmployeesPagedList {
  page: number;
  pageSize: number;
  totalCount: number;
  employees: IEmployeeShortInfo[];
}

export interface IAddEmployee {
  email: string;
  roleId: string;
}

export interface IUpdateEmployeeRole {
  roleName: string;
}
