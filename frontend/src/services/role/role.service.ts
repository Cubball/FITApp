import { HttpFactoryService } from '../../shared/services/http-factory.service';
import { EnhancedWithAuthHttpService } from '../../shared/services/http-auth.service.ts';
import { ICreateRoleRequest, IRolesResponse } from './role.types.ts';

class RoleService {
  httpService: EnhancedWithAuthHttpService;

  constructor(httpService: EnhancedWithAuthHttpService) {
    this.httpService = httpService;
  }

  private readonly module = '/roles';

  public async getRoles(): Promise<IRolesResponse> {
    return this.httpService.get(this.module);
  }

  public async createRole(data: ICreateRoleRequest): Promise<null> {
    return this.httpService.post(this.module, data);
  }

  public async updateRole(id: string, data: ICreateRoleRequest): Promise<null> {
    return this.httpService.put(`${this.module}/${id}`, data);
  }

  public async deleteRole(id: string): Promise<null> {
    return this.httpService.delete(`${this.module}/${id}`);
  }
}
export const authService = new RoleService(new HttpFactoryService().createAuthHttpService());
