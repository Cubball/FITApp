import { STORAGE_KEYS } from '../../shared/keys/storage-keys';
import { HttpService } from '../../shared/services/http.service';
import { mainAxios } from '../../shared/services/mainAxios';
import { IHttpClient } from '../../shared/services/types';
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
    const jwt = localStorage.getItem(STORAGE_KEYS.JWT_TOKEN);
    return this.httpService.post(
      `${this.module}/refresh`,
      {},
      { headers: { Authorization: `Bearer ${jwt}` } }
    );
  }

  public resetPassword(email: string): Promise<null> {
    return this.httpService.post(`${this.module}/reset-password`, { email });
  }

  public confirmResetPassword(id: string, token: string): Promise<null> {
    return this.httpService.get(`${this.module}/reset-password-confirm`, {
      params: {
        id,
        token
      }
    });
  }
}
export const authService = new AuthService(new HttpService(mainAxios as IHttpClient));
