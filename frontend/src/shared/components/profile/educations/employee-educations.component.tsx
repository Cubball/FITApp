import { useState } from 'react';
import EducationIcon from '../../../../assets/icons/education-icon.svg';
import { IAddEducationBody, IEmployee } from '../../../../services/profile/profile.types';
import ExpandableList from '../expandable.list.component';
import EmployeeEducation from './employee.education.component';
import AddEducationModal from './add-education-modal.component';

interface EmployeeEducationsProps {
  employee: IEmployee;
  canEdit: boolean;
  onSubmit: (body: IAddEducationBody) => void;
  onDelete: (index: number) => void;
}

const EmployeeEducations = ({ employee, canEdit, onSubmit, onDelete }: EmployeeEducationsProps) => {
  const [addModalOpen, setAddModalOpen] = useState(false);

  return (
    <>
      <AddEducationModal
        isOpen={addModalOpen}
        onClose={() => setAddModalOpen(false)}
        onSubmit={onSubmit}
      />
      <ExpandableList
        title="Освіта"
        icon={EducationIcon}
        onAddClick={() => setAddModalOpen(true)}
        onDeleteClick={onDelete}
        canEdit={canEdit}
      >
        {employee.educations.map((education, index) => (
          <EmployeeEducation education={education} key={index} />
        ))}
      </ExpandableList>
    </>
  );
};

export default EmployeeEducations;
