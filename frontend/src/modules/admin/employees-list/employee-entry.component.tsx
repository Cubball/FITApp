import { IEmployeeShortInfo } from './employees-list.component';
import TrashIcon from './../../../assets/icons/trash-icon.svg';
import { NavLink } from 'react-router-dom';
import { useState } from 'react';
import ConfirmModal from '../../../shared/components/confirm-modal';

interface EmployeeEntryProps {
  employee: IEmployeeShortInfo;
}

const EmployeeEntry = ({ employee }: EmployeeEntryProps) => {
  // TODO:
  const onDeleteClick = () => {};
  const [confirmModalOpen, setConfirmModalOpen] = useState(false);
  const displayName = `${employee.lastName} ${employee.firstName.substring(0, 1)}. ${employee.patronymic.substring(0, 1)}.`

  return (
    <>
      <ConfirmModal
        isOpen={confirmModalOpen}
        text={`Ви впевнені що хочете видалити співробітника ${displayName}?`}
        onConfirm={() => onDeleteClick()}
        onClose={() => setConfirmModalOpen(false)}
      />
      <div className="my-2 flex items-center justify-between rounded-lg bg-accent-background p-3">
        <NavLink className="flex grow flex-col gap-3 md:flex-row" to={`/employees/${employee.id}`}>
          <span className="font-semibold md:basis-[40%]">
            {displayName}
          </span>
          <span className="md:basis-[35%]">{employee.email}</span>
          <span className="md:basis-[15%]">{employee.role}</span>
        </NavLink>
        <div className="flex items-center justify-between gap-1 md:gap-3">
          <button className="mr-1" onClick={() => setConfirmModalOpen(true)}>
            <img src={TrashIcon} />
          </button>
        </div>
      </div>
    </>
  );
};

export default EmployeeEntry;
