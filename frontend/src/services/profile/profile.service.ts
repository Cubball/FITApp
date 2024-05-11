import { HttpFactoryService } from '../../shared/services/http-factory.service';
import { EnhancedWithAuthHttpService } from '../../shared/services/http-auth.service.ts';
import {
  IAddAcademicDegreesBody,
  IAddAcademicRank,
  IAddEducationBody,
  IAddPositionBody,
  IEmployee,
  IUpdateEmployeeBody
} from './profile.types.ts';

class ProfileService {
  private readonly httpService: EnhancedWithAuthHttpService;

  constructor(httpService: EnhancedWithAuthHttpService) {
    this.httpService = httpService;
  }

  private readonly module = 'api/profile';

  public getProfile(): Promise<IEmployee> {
    return this.httpService.get(this.module);
  }

  public updateProfile(data: IUpdateEmployeeBody): Promise<null> {
    // TODO change respornse type
    return this.httpService.put(this.module, data);
  }

  public addPosition(data: IAddPositionBody): Promise<null> {
    // TODO change response type
    return this.httpService.post(`${this.module}/positions`, data);
  }

  public addEducation(data: IAddEducationBody): Promise<null> {
    // TODO change response type
    return this.httpService.post(`${this.module}/educations`, data);
  }

  public addAcademicDegrees(data: IAddAcademicDegreesBody): Promise<null> {
    // TODO change response type
    return this.httpService.post(`${this.module}/academic-degrees`, data);
  }

  public addAcademicRank(data: IAddAcademicRank): Promise<null> {
    // TODO change response type
    return this.httpService.post(`${this.module}/academic-ranks`, data);
  }

  public deletePosition(index: string): Promise<null> {
    return this.httpService.delete(`${this.module}/positions/${index}`);
  }

  public deleteEducation(index: string) {
    return this.httpService.delete(`${this.module}/educations/${index}`);
  }

  public deleteAcademicDegrees(index: string) {
    return this.httpService.delete(`${this.module}/academic-degrees/${index}`);
  }

  public deleteAcademicRank(index: string) {
    return this.httpService.delete(`${this.module}/academic-ranks/${index}`);
  }

  public uploadPhoto(photo: File) {
    return this.httpService.put(
      `${this.module}/photo`,
      { file: photo },
      {
        headers: {
          'Content-Type': 'multipart/form-data'
        }
      }
    );
  }

  public deletePhoto() {
    return this.httpService.delete(`${this.module}/photo`);
  }
}
export const authService = new ProfileService(new HttpFactoryService().createAuthHttpService());
