import { IAcademicRank } from '../../../../services/profile/profile.types';

interface EmployeeAcademicRankProps {
  academicRank: IAcademicRank;
}

const EmployeeAcademicRank = ({ academicRank }: EmployeeAcademicRankProps) => {
  return (
    <div>
      <h3 className="text-md font-semibold">{academicRank.name}</h3>
      <div>{academicRank.certificateNumber}</div>
      <div className="italic">{new Date(academicRank.dateOfIssue).toLocaleDateString('uk-UA')}</div>
    </div>
  );
};

export default EmployeeAcademicRank;
