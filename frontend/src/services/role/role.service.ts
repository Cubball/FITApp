import { HttpFactoryService } from '../../shared/services/http-factory.service';
import { EnhancedWithAuthHttpService } from '../../shared/services/http-auth.service.ts';
import { ICreateRoleRequest, IRole, IRolesResponse } from './role.types.ts';

class RoleService {
  private readonly httpService: EnhancedWithAuthHttpService;

  constructor(httpService: EnhancedWithAuthHttpService) {
    this.httpService = httpService;
  }

  private readonly module = '/roles';

  public async getRoles(): Promise<Array<IRole>> {
    const response = await this.httpService.get<IRolesResponse>(this.module);
    return response.roles;
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
export const roleService = new RoleService(new HttpFactoryService().createAuthHttpService());
