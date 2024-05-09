import { IAcademicDegree } from "../../../services/profile/profile.types";

const EmployeeAcademicDegree = ({
  academicDegree
}: {
  academicDegree: IAcademicDegree;
}) => {
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
