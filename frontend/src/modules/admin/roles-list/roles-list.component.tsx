import { IRoleShortInfo } from '../../../services/role/role.types';
import Error from '../../../shared/components/error';
import Loading from '../../../shared/components/loading';
import { useRoles } from '../../../shared/hooks/roles.hook';
import RoleEntry from './role-entry.component';

const RolesList = () => {
  const { roles, isGetRolesLoading, handleDeleteRole } = useRoles();
  if (isGetRolesLoading) {
    return <Loading />;
  }

  if (!roles) {
    return <Error />;
  }

  return (
    <div className="flex h-full flex-col items-center justify-between px-10 py-5">
      <div className="w-full">
        <h1 className="py-3 text-xl font-bold">Список ролей</h1>
        {roles.map((role: IRoleShortInfo) => (
          <RoleEntry role={role} key={role.id} onDelete={handleDeleteRole} />
        ))}
      </div>
    </div>
  );
};

export default RolesList;
