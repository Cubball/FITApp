import { IAcademicDegree } from '../../../../services/profile/profile.types';

interface EmployeeAcademicDegreeProps {
  academicDegree: IAcademicDegree;
}

const EmployeeAcademicDegree = ({ academicDegree }: EmployeeAcademicDegreeProps) => {
  return (
    <div>
      <h3 className="text-md font-semibold">{academicDegree.fullName}</h3>
      <h3 className="text-md font-semibold">{academicDegree.shortName}</h3>
      <div>{academicDegree.diplomaNumber}</div>
      <div className="italic">{academicDegree.dateOfIssue.toLocaleDateString('uk-UA')}</div>
    </div>
  );
};

export default EmployeeAcademicDegree;
