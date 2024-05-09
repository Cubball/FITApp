import ExpandableList from '../expandable.list.component';
import EmployeePosition from './employee.position.component';
import PositionIcon from '../../../../assets/icons/position-icon.svg';
import { IEmployee } from '../../../../services/profile/profile.types';
import AddPositionModal from './add-position-modal.component';
import { useState } from 'react';

interface EmployeePositionsProps {
  employee: IEmployee;
  canEdit: boolean;
  isOwnProfile: boolean;
}

const EmployeePositions = ({ employee, canEdit, isOwnProfile }: EmployeePositionsProps) => {
  const [addModalOpen, setAddModalOpen] = useState(false);

  return (
    <>
      <AddPositionModal
        isOpen={addModalOpen}
        onClose={() => setAddModalOpen(false)}
        onSubmit={() => console.log('foo')}
      />
      <ExpandableList
        title="Посади"
        icon={PositionIcon}
        onAddClick={()=> setAddModalOpen(true)}
        onDeleteClick={(index: number) => console.log('delete ' + index)}
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
