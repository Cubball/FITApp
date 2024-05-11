import { HttpFactoryService } from '../../shared/services/http-factory.service';
import { EnhancedWithAuthHttpService } from '../../shared/services/http-auth.service.ts';
import { ICreateRoleRequest, IPermission, IRole, IRoleShortInfo } from './role.types.ts';

class RoleService {
  private readonly httpService: EnhancedWithAuthHttpService;

  constructor(httpService: EnhancedWithAuthHttpService) {
    this.httpService = httpService;
  }

  private readonly module = 'api/roles';

  public async getRoles(): Promise<Array<IRoleShortInfo>> {
    return this.httpService.get<Array<IRoleShortInfo>>(this.module);
  }

  public getRole(id: string): Promise<IRole> {
    return this.httpService.get<IRole>(`${this.module}/${id}`);
  }

  public getPermissions(): Promise<IPermission[]> {
    return this.httpService.get<IPermission[]>(`${this.module}/permissions`);
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
