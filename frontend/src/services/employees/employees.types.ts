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
  total: number;
  employees: IEmployeeShortInfo[];
}

export interface IAddEmployee {
  email: string;
  roleName: string;
}

export interface IUpdateEmployeeRole {
  roleName: string;
}
