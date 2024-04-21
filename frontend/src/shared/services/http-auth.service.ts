import { STORAGE_KEYS } from '../keys/storage-keys';
import type { HttpService } from './http.service';
import type { IHttpConfig, IMap } from './types';

export class EnhancedWithAuthHttpService {
  constructor(private readonly httpService: HttpService) {
    this.httpService = httpService;
  }

  public createQueryLink(base: string, parameters: IMap): string {
    return this.httpService.createQueryLink(base, parameters);
  }

  public async get<T>(url: string, config: IHttpConfig = {}): Promise<T> {
    return this.httpService.get<T>(url, await this.attachAuthHeader(config));
  }

  public async post<T, TD>(url: string, data: TD, config: IHttpConfig = {}): Promise<T> {
    return this.httpService.post<T, TD>(url, data, await this.attachAuthHeader(config));
  }

  public async put<T, TD>(url: string, data: TD, config: IHttpConfig = {}): Promise<T> {
    return this.httpService.put<T, TD>(url, data, await this.attachAuthHeader(config));
  }

  public async patch<T, TD>(url: string, data: TD, config: IHttpConfig = {}): Promise<T> {
    return this.httpService.patch<T, TD>(url, data, await this.attachAuthHeader(config));
  }

  public async delete<T>(url: string, config: IHttpConfig = {}): Promise<T> {
    return this.httpService.delete<T>(url, await this.attachAuthHeader(config));
  }

  private async attachAuthHeader(config: IHttpConfig): Promise<IHttpConfig> {
    const token = await this.populateTokenToHeaderConfig();
    return {
      ...config,
      headers: {
        ...config.headers,
        ...token
      }
    };
  }

  private async populateTokenToHeaderConfig(): Promise<object> {
    const token = localStorage.getItem(STORAGE_KEYS.JWT_TOKEN);
    return {
      Authorization: `Bearer ${token}`
    };
  }
}
