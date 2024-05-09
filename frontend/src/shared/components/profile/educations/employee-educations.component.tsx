import { useState } from 'react';
import EducationIcon from '../../../../assets/icons/education-icon.svg';
import { IEmployee } from '../../../../services/profile/profile.types';
import ExpandableList from '../expandable.list.component';
import EmployeeEducation from './employee.education.component';
import AddEducationModal from './add-education-modal.component';

interface EmployeeEducationsProps {
  employee: IEmployee;
  canEdit: boolean;
  isOwnProfile: boolean;
}

const EmployeeEducations = ({ employee, canEdit, isOwnProfile }: EmployeeEducationsProps) => {
  const [addModalOpen, setAddModalOpen] = useState(false);

  return (
    <>
      <AddEducationModal
        isOpen={addModalOpen}
        onClose={() => setAddModalOpen(false)}
        onSubmit={() => console.log('foo')}
      />
      <ExpandableList
        title="Освіта"
        icon={EducationIcon}
        onAddClick={() => setAddModalOpen(true)}
        onDeleteClick={(index: number) => console.log('delete ' + index)}
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
