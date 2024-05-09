import EmployeeInfo from './info/employee.info.component';
import { IEmployee } from '../../../services/profile/profile.types';
import EmployeeEducations from './educations/employee-educations.component';
import EmployeeAcademicDegrees from './academic-degrees/employee-academic-degrees.component';
import EmployeeAcademicRanks from './academic-ranks/employee-academic-ranks.component';
import EmployeePositions from './positions/employee-positions.component';

interface ProfileProps {
  employee: IEmployee;
  canEdit: boolean;
  isOwnProfile: boolean;
}

const Profile = ({ employee, canEdit, isOwnProfile }: ProfileProps) => {
  return (
    <div className="px-10 py-5">
      <EmployeeInfo employee={employee} canEdit={canEdit} isOwnProfile={isOwnProfile} />
      <EmployeeEducations employee={employee} canEdit={canEdit} isOwnProfile={isOwnProfile} />
      <EmployeeAcademicDegrees employee={employee} canEdit={canEdit} isOwnProfile={isOwnProfile} />
      <EmployeeAcademicRanks employee={employee} canEdit={canEdit} isOwnProfile={isOwnProfile} />
      <EmployeePositions employee={employee} canEdit={canEdit} isOwnProfile={isOwnProfile} />
    </div>
  );
};

export default Profile;
