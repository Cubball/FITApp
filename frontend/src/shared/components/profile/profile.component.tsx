import EmployeeInfo from './employee.info.component';
import ExpandableList from './expandable.list.component';
import EducationIcon from '../../../assets/icons/education-icon.svg';
import EmployeeEducation from './employee.education.component';
import AcademicRankIcon from '../../../assets/icons/academic-rank-icon.svg';
import AcademicDegreeIcon from '../../../assets/icons/academic-degree-icon.svg';
import PositionIcon from '../../../assets/icons/position-icon.svg';
import EmployeeAcademicDegree from './employee.academic.degree.component';
import EmployeeAcademicRank from './employee.academic.rank.component';
import EmployeePosition from './employee.position.component';
import { IEmployee } from '../../../services/profile/profile.types';

const Profile = ({ employee, canEdit, isOwnProfile }: { employee: IEmployee, canEdit: boolean, isOwnProfile: boolean }) => {
  return (
    <div className="px-10 py-5">
      <EmployeeInfo employee={employee} canEdit={canEdit}/>
      <ExpandableList
        title="Освіта"
        icon={EducationIcon}
        onAddClick={() => console.log('add')}
        onDeleteClick={(index: number) => console.log('delete ' + index)}
        canEdit={canEdit}
      >
        {employee.educations.map((education, index) => (
          <EmployeeEducation education={education} key={index} />
        ))}
      </ExpandableList>
      <ExpandableList
        title="Наукові ступені"
        icon={AcademicDegreeIcon}
        onAddClick={() => console.log('add')}
        onDeleteClick={(index: number) => console.log('delete ' + index)}
        canEdit={canEdit}
      >
        {employee.academicDegrees.map((academicDegree, index) => (
          <EmployeeAcademicDegree academicDegree={academicDegree} key={index} />
        ))}
      </ExpandableList>
      <ExpandableList
        title="Наукові звання"
        icon={AcademicRankIcon}
        onAddClick={() => console.log('add')}
        onDeleteClick={(index: number) => console.log('delete ' + index)}
        canEdit={canEdit}
      >
        {employee.academicRanks.map((academicRank, index) => (
          <EmployeeAcademicRank academicRank={academicRank} key={index} />
        ))}
      </ExpandableList>
      <ExpandableList
        title="Посади"
        icon={PositionIcon}
        onAddClick={() => console.log('add')}
        onDeleteClick={(index: number) => console.log('delete ' + index)}
        canEdit={canEdit}
      >
        {employee.positions.map((position, index) => (
          <EmployeePosition position={position} key={index} />
        ))}
      </ExpandableList>
    </div>
  );
};

export default Profile;
