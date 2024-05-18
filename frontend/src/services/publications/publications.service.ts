import { EnhancedWithAuthHttpService } from '../../shared/services/http-auth.service';
import { HttpFactoryService } from '../../shared/services/http-factory.service';
import {
  ICreateUpdatePublication,
  IPublication,
  IPublicationsPagedList
} from './publications.types';

class PublicationsService {
  private readonly httpService: EnhancedWithAuthHttpService;

  constructor(httpService: EnhancedWithAuthHttpService) {
    this.httpService = httpService;
  }

  private readonly publicationsEndpoint = 'api/publications';

  public getPublications(page: number, pageSize: number): Promise<IPublicationsPagedList> {
    return this.httpService.get(this.publicationsEndpoint, {
      params: {
        page,
        pageSize
      }
    });
  }

  public getPublication(id: string): Promise<IPublication> {
    return this.httpService.get(`${this.publicationsEndpoint}/${id}`);
  }

  public addPublication(publication: ICreateUpdatePublication): Promise<null> {
    return this.httpService.post(this.publicationsEndpoint, publication);
  }

  public updatePublication(id: string, publication: ICreateUpdatePublication): Promise<null> {
    return this.httpService.put(`${this.publicationsEndpoint}/${id}`, publication);
  }

  public deletePublication(id: string): Promise<null> {
    return this.httpService.delete(`${this.publicationsEndpoint}/${id}`);
  }
}
export const publicationsService = new PublicationsService(
  new HttpFactoryService().createAuthHttpService()
);
