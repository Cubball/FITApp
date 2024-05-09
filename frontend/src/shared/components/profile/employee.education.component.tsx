import { IEducation } from '../../../services/profile/profile.types';

interface EmployeeEducationProps {
  education: IEducation;
}

const EmployeeEducation = ({ education }: EmployeeEducationProps) => {
  return (
    <div>
      <h3 className="text-md font-semibold">{education.university}</h3>
      <div className="font-semibold">{education.specialty}</div>
      <div className="italic">{education.diplomaDateOfIssue.toLocaleDateString('uk-UA')}</div>
    </div>
  );
};

export default EmployeeEducation;
