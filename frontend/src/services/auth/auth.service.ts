import { STORAGE_KEYS } from '../../shared/keys/storage-keys';
import { HttpFactoryService } from '../../shared/services/http-factory.service';
import { HttpService } from '../../shared/services/http.service';
import { IAuth } from './auth.types';

class AuthService {
  constructor(private readonly httpService: HttpService) {
    this.httpService = httpService;
  }

  private readonly module = 'api/auth';

  public async login(email: string, password: string): Promise<IAuth> {
    return this.httpService.post(`${this.module}/login`, { email, password });
  }

  public async refresh(): Promise<IAuth> {
    const refresh = localStorage.getItem(STORAGE_KEYS.JWT_TOKEN);
    return this.httpService.post(`${this.module}/refresh`, { refreshToken: refresh || '' });
  }
}
export const authService = new AuthService(new HttpFactoryService().createHttpService());
