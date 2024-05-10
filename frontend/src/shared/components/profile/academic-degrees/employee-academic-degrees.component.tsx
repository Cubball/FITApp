import { IAddAcademicDegreesBody, IEmployee } from '../../../../services/profile/profile.types';
import ExpandableList from '../expandable.list.component';
import EmployeeAcademicDegree from './employee.academic.degree.component';
import AcademicDegreeIcon from '../../../../assets/icons/academic-degree-icon.svg';
import AddAcademicDegreeModal from './add-academic-degree-modal.component';
import { useState } from 'react';

interface EmployeeAcademicDegreesProps {
  employee: IEmployee;
  canEdit: boolean;
  onSubmit: (body: IAddAcademicDegreesBody) => void;
  onDelete: (index: number) => void;
}

const EmployeeAcademicDegrees = ({
  employee,
  canEdit,
  onSubmit,
  onDelete
}: EmployeeAcademicDegreesProps) => {
  const [addModalOpen, setAddModalOpen] = useState(false);

  return (
    <>
      <AddAcademicDegreeModal
        isOpen={addModalOpen}
        onClose={() => setAddModalOpen(false)}
        onSubmit={onSubmit}
      />
      <ExpandableList
        title="Наукові ступені"
        icon={AcademicDegreeIcon}
        onAddClick={() => setAddModalOpen(true)}
        onDeleteClick={onDelete}
        canEdit={canEdit}
      >
        {employee.academicDegrees.map((academicDegree, index) => (
          <EmployeeAcademicDegree academicDegree={academicDegree} key={index} />
        ))}
      </ExpandableList>
    </>
  );
};

export default EmployeeAcademicDegrees;
