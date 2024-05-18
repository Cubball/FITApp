import { useState } from 'react';
import EditIcon from '../../assets/icons/edit-icon.svg';
import { userPermissionsService } from '../../services/auth/user-permissions.service';
import { PermissionsEnum } from '../../services/role/role.types';
import Error from '../../shared/components/error';
import Loading from '../../shared/components/loading';
import { useAdministration } from '../../shared/hooks/administration.hook';
import UpdateAdministrationEmployeeModal from './update-administration-employee-modal.component';

const Administration = () => {
  const [headOfDepartmentEditOpen, setHeadOfDepartmentEditOpen] = useState(false);
  const [secretaryEditOpen, setSecretaryEditOpen] = useState(false);
  const canEdit = userPermissionsService.hasPermission(PermissionsEnum.administrationUpdate);
  const {
    administration,
    isAdministrationLoading,
    employees,
    areEmployeesLoading,
    updateAdministration
  } = useAdministration();
  if (isAdministrationLoading || areEmployeesLoading) {
    return <Loading />;
  }

  if (!administration || !employees) {
    return <Error />;
  }

  let { firstName, lastName, patronymic } = administration.headOfDepartment;
  let headOfDepartmentDisplayName = "<ім'я не вказано>";
  if (firstName && lastName && patronymic) {
    headOfDepartmentDisplayName = `${lastName} ${firstName} ${patronymic}`;
  }

  let scientificSecretaryDisplayName = "<ім'я не вказано>";
  ({ firstName, lastName, patronymic } = administration.scientificSecretary);
  if (firstName && lastName && patronymic) {
    scientificSecretaryDisplayName = `${lastName} ${firstName} ${patronymic}`;
  }

  return (
    <>
      <UpdateAdministrationEmployeeModal
        isOpen={headOfDepartmentEditOpen}
        onClose={() => setHeadOfDepartmentEditOpen(false)}
        onUpdate={(headOfDepartment) =>
          updateAdministration({
            ...administration,
            headOfDepartment
          })
        }
        employee={administration.headOfDepartment}
        employees={employees}
      />
      <UpdateAdministrationEmployeeModal
        isOpen={secretaryEditOpen}
        onClose={() => setSecretaryEditOpen(false)}
        onUpdate={(scientificSecretary) =>
          updateAdministration({
            ...administration,
            scientificSecretary
          })
        }
        employee={administration.scientificSecretary}
        employees={employees}
      />
      <div className="flex flex-col items-center gap-5 p-5">
        <h1 className="border-b border-gray-300 px-10 pb-1 text-center text-xl font-semibold">
          Інформація про адміністрацію
        </h1>
        <div className="relative w-full max-w-xl rounded-md border border-gray-300 p-2 md:w-1/2">
          <h2 className="text-center text-lg font-semibold">Завідувач кафедри</h2>
          <p className="text-center text-lg">{headOfDepartmentDisplayName}</p>
          {canEdit && (
            <img
              className="absolute right-2 top-2 cursor-pointer"
              src={EditIcon}
              onClick={() => setHeadOfDepartmentEditOpen(true)}
            />
          )}
        </div>
        <div className="relative w-full max-w-xl rounded-md border border-gray-300 p-2 md:w-1/2">
          <h2 className="text-center text-lg font-semibold">Вчений секретар факультету</h2>
          <p className="text-center text-lg">{scientificSecretaryDisplayName}</p>
          {canEdit && (
            <img
              className="absolute right-2 top-2 cursor-pointer"
              src={EditIcon}
              onClick={() => setSecretaryEditOpen(true)}
            />
          )}
        </div>
      </div>
    </>
  );
};

export default Administration;
