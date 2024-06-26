import { IAddAcademicRank, IEmployee } from '../../../../services/profile/profile.types';
import ExpandableList from '../expandable.list.component';
import EmployeeAcademicRank from './employee.academic.rank.component';
import AcademicRankIcon from '../../../../assets/icons/academic-rank-icon.svg';
import AddAcademicRankModal from './add-academic-rank-modal.component';
import { useState } from 'react';

interface EmployeeAcademicRanksProps {
  employee: IEmployee;
  canEdit: boolean;
  onSubmit: (body: IAddAcademicRank) => void;
  onDelete: (index: number) => void;
}

const EmployeeAcademicRanks = ({
  employee,
  canEdit,
  onSubmit,
  onDelete
}: EmployeeAcademicRanksProps) => {
  const [editModalOpen, setEditModalOpen] = useState(false);

  return (
    <>
      <AddAcademicRankModal
        isOpen={editModalOpen}
        onSubmit={onSubmit}
        onClose={() => setEditModalOpen(false)}
      />
      <ExpandableList
        title="Наукові звання"
        icon={AcademicRankIcon}
        onAddClick={() => setEditModalOpen(true)}
        onDeleteClick={onDelete}
        canEdit={canEdit}
      >
        {employee.academicRanks.map((academicRank, index) => (
          <EmployeeAcademicRank academicRank={academicRank} key={index} />
        ))}
      </ExpandableList>
    </>
  );
};

export default EmployeeAcademicRanks;
