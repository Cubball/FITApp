import ExpandableList from '../expandable.list.component';
import EmployeePosition from './employee.position.component';
import PositionIcon from '../../../../assets/icons/position-icon.svg';
import { IAddPositionBody, IEmployee } from '../../../../services/profile/profile.types';
import AddPositionModal from './add-position-modal.component';
import { useState } from 'react';

interface EmployeePositionsProps {
  employee: IEmployee;
  canEdit: boolean;
  onSubmit: (body: IAddPositionBody) => void;
  onDelete: (index: number) => void;
}

const EmployeePositions = ({ employee, canEdit, onSubmit, onDelete }: EmployeePositionsProps) => {
  const [addModalOpen, setAddModalOpen] = useState(false);

  return (
    <>
      <AddPositionModal
        isOpen={addModalOpen}
        onClose={() => setAddModalOpen(false)}
        onSubmit={onSubmit}
      />
      <ExpandableList
        title="Посади"
        icon={PositionIcon}
        onAddClick={() => setAddModalOpen(true)}
        onDeleteClick={onDelete}
        canEdit={canEdit}
      >
        {employee.positions.map((position, index) => (
          <EmployeePosition position={position} key={index} />
        ))}
      </ExpandableList>
    </>
  );
};

export default EmployeePositions;
