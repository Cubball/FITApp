export interface IAdministration {
  headOfDepartment: IAdministrationEmployee;
  scientificSecretary: IAdministrationEmployee;
}

export interface IAdministrationEmployee {
  id?: string;
  firstName: string;
  lastName: string;
  patronymic: string;
}
