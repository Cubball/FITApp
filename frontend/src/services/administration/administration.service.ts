import { EnhancedWithAuthHttpService } from '../../shared/services/http-auth.service';
import { HttpFactoryService } from '../../shared/services/http-factory.service';
import { IAdministration } from './administration.types';

class AdministrationService {
  private readonly administrationEndpoint = 'api/administration';

  constructor(private http: EnhancedWithAuthHttpService) { }

  public getAdministrationInfo(): Promise<IAdministration> {
    return this.http.get(this.administrationEndpoint);
  }

  public updateAdministrationInfo(administration: IAdministration): Promise<null> {
    return this.http.put(this.administrationEndpoint, administration);
  }
}

export const administrationService = new AdministrationService(
  new HttpFactoryService().createAuthHttpService()
);
