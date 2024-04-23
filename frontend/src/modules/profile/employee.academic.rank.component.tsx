import { IEmployeeAcademicRank } from './types';

const EmployeeAcademicRank = ({ academicRank }: { academicRank: IEmployeeAcademicRank }) => {
  return (
    <div>
      <h3 className="text-md font-semibold">{academicRank.name}</h3>
      <div>{academicRank.certificateNumber}</div>
      <div className="italic">{academicRank.dateOfIssue.toLocaleDateString('uk-UA')}</div>
    </div>
  );
};

export default EmployeeAcademicRank;
