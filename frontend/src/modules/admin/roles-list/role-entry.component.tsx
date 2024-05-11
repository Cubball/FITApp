import TrashIcon from './../../../assets/icons/trash-icon.svg';
import { NavLink } from 'react-router-dom';
import { useState } from 'react';
import ConfirmModal from '../../../shared/components/confirm-modal';
import { IRoleShortInfo } from '../../../services/role/role.types';

interface RoleEntryProps {
  role: IRoleShortInfo;
  onDelete: (id: string) => void;
}

const RoleEntry = ({ role, onDelete }: RoleEntryProps) => {
  const [confirmModalOpen, setConfirmModalOpen] = useState(false);

  return (
    <>
      <ConfirmModal
        isOpen={confirmModalOpen}
        text={`Ви впевнені що хочете видалити роль ${role.name}?`}
        onConfirm={() => onDelete(role.id)}
        onClose={() => setConfirmModalOpen(false)}
      />
      <div className="my-2 flex items-center justify-between rounded-lg bg-accent-background p-3">
        <NavLink className="flex grow flex-col gap-3 md:flex-row" to={`/roles/${role.id}`}>
          {role.name}
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

export default RoleEntry;
