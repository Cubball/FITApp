import TrashIcon from './../../../assets/icons/trash-icon.svg';
import ResetIcon from '../../../assets/icons/reset-icon.svg';
import ChangeRoleIcon from '../../../assets/icons/change-role.svg'
import { NavLink } from 'react-router-dom';
import { useState } from 'react';
import ConfirmModal from '../../../shared/components/confirm-modal';
import { IEmployeeShortInfo } from '../../../services/employees/employees.types';
import { userPermissionsService } from '../../../services/auth/user-permissions.service';
import { PermissionsEnum } from '../../../services/role/role.types';

interface EmployeeEntryProps {
  employee: IEmployeeShortInfo;
  onDelete: (id: string) => void;
  onPasswordReset: (id: string) => void;
}

const EmployeeEntry = ({ employee, onDelete, onPasswordReset }: EmployeeEntryProps) => {
  const [confirmDeleteModalOpen, setConfirmDeleteModalOpen] = useState(false);
  const [confirmResetPasswordModalOpen, setConfirmResetPasswordModalOpen] = useState(false);
  const canEdit = userPermissionsService.hasPermission(PermissionsEnum.usersUpdate);
  const canDelete = userPermissionsService.hasPermission(PermissionsEnum.usersDelete);
  let displayName = "<ім'я не вказано>";
  if (employee.firstName && employee.patronymic) {
    displayName = `${employee.lastName} ${employee.firstName.substring(0, 1)}. ${employee.patronymic.substring(0, 1)}.`;
  }

  return (
    <>
      <ConfirmModal
        isOpen={confirmDeleteModalOpen}
        text={`Ви впевнені що хочете видалити співробітника ${displayName}?`}
        onConfirm={() => onDelete(employee.id)}
        onClose={() => setConfirmDeleteModalOpen(false)}
      />
      <ConfirmModal
        isOpen={confirmResetPasswordModalOpen}
        text={`Ви впевнені що хочете скинути пароль співробітника ${displayName}?`}
        onConfirm={() => onPasswordReset(employee.id)}
        onClose={() => setConfirmResetPasswordModalOpen(false)}
      />
      <div className="my-2 flex items-center justify-between rounded-lg bg-accent-background p-3">
        <div className="flex grow flex-col gap-3 md:flex-row">
          <NavLink className="font-semibold md:basis-[40%]" to={`/employees/${employee.id}`}>
            {displayName}
          </NavLink>
          <span className="md:basis-[35%]">{employee.email}</span>
          <span className="md:basis-[15%]">{employee.role}</span>
        </div>
        <div className="flex items-center justify-between gap-1 md:gap-3">
          {canEdit && (
            <>
              <NavLink
                className="mr-1"
                to={`/employees/${employee.id}/role`}
              >
                <img src={ChangeRoleIcon} />
              </NavLink>
              <button className="mr-1" onClick={() => setConfirmResetPasswordModalOpen(true)}>
                <img src={ResetIcon} />
              </button>
            </>
          )}
          {canDelete && (
            <button className="mr-1" onClick={() => setConfirmDeleteModalOpen(true)}>
              <img src={TrashIcon} />
            </button>
          )}
        </div>
      </div>
    </>
  );
};

export default EmployeeEntry;
