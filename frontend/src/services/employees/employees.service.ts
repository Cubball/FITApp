import { EnhancedWithAuthHttpService } from '../../shared/services/http-auth.service';
import { HttpFactoryService } from '../../shared/services/http-factory.service';
import {
  IAddAcademicDegreesBody,
  IAddAcademicRank,
  IAddEducationBody,
  IAddPositionBody,
  IEmployee,
  IUpdateEmployeeBody
} from '../profile/profile.types';
import { IAddEmployee, IEmployeesPagedList, IUpdateEmployeeRole } from './employees.types';

class EmployeesService {
  httpService: EnhancedWithAuthHttpService;

  constructor(httpService: EnhancedWithAuthHttpService) {
    this.httpService = httpService;
  }

  private readonly employeesEndpoint = 'api/employees';
  private readonly usersEndpoint = 'api/users';

  public getEmployees(page: number, pageSize: number): Promise<IEmployeesPagedList> {
    return this.httpService.get(this.employeesEndpoint, {
      params: {
        page,
        pageSize
      }
    });
  }

  public addEmployee(employee: IAddEmployee): Promise<null> {
    return this.httpService.post(this.usersEndpoint, employee);
  }

  public deleteEmployee(id: string): Promise<null> {
    return this.httpService.delete(`${this.employeesEndpoint}/${id}`);
  }

  public updateEmployeeRole(id: string, employeeRole: IUpdateEmployeeRole) {
    return this.httpService.put(`${this.employeesEndpoint}/${id}`, employeeRole);
  }

  public getEmployee(id: string): Promise<IEmployee> {
    return this.httpService.get(`${this.employeesEndpoint}/${id}`);
  }

  public updateEmployeeInfo(id: string, employee: IUpdateEmployeeBody): Promise<IEmployee> {
    return this.httpService.put(`${this.employeesEndpoint}/${id}`, employee);
  }

  public resetEmployeePassword(id: string): Promise<null> {
    return this.httpService.post(`${this.usersEndpoint}/${id}/reset-password`, {});
  }

  public changeEmployeeRole(employeeId: string, roleId: string): Promise<null> {
    return this.httpService.put(`${this.usersEndpoint}/${employeeId}/role`, { roleId });
  }

  public addPosition(id: string, position: IAddPositionBody): Promise<null> {
    return this.httpService.post(`${this.employeesEndpoint}/${id}/positions`, position);
  }

  public addEducation(id: string, education: IAddEducationBody): Promise<null> {
    return this.httpService.post(`${this.employeesEndpoint}/${id}/educations`, education);
  }

  public addAcademicDegrees(id: string, degree: IAddAcademicDegreesBody): Promise<null> {
    return this.httpService.post(`${this.employeesEndpoint}/${id}/academic-degrees`, degree);
  }

  public addAcademicRank(id: string, rank: IAddAcademicRank): Promise<null> {
    return this.httpService.post(`${this.employeesEndpoint}/${id}/academic-ranks`, rank);
  }

  public deletePosition(id: string, index: string): Promise<null> {
    return this.httpService.delete(`${this.employeesEndpoint}/${id}/positions/${index}`);
  }

  public deleteEducation(id: string, index: string): Promise<null> {
    return this.httpService.delete(`${this.employeesEndpoint}/${id}/educations/${index}`);
  }

  public deleteAcademicDegrees(id: string, index: string): Promise<null> {
    return this.httpService.delete(`${this.employeesEndpoint}/${id}/academic-degrees/${index}`);
  }

  public deleteAcademicRank(id: string, index: string): Promise<null> {
    return this.httpService.delete(`${this.employeesEndpoint}/${id}/academic-ranks/${index}`);
  }

  public uploadPhoto(id: string, photo: File): Promise<null> {
    return this.httpService.put(
      `${this.employeesEndpoint}/${id}/photo`,
      { file: photo },
      {
        headers: {
          'Content-Type': 'multipart/form-data'
        }
      }
    );
  }

  public deletePhoto(id: string): Promise<null> {
    return this.httpService.delete(`${this.employeesEndpoint}/${id}/photo`);
  }
}

export const employeesService = new EmployeesService(
  new HttpFactoryService().createAuthHttpService()
);
