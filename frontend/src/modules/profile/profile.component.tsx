import EmployeeInfo from './employee.info.component';
import ExpandableList from './expandable.list.component';
import EducationIcon from '../../assets/icons/education-icon.svg';
import EmployeeEducation from './employee.education.component';
import AcademicRankIcon from '../../assets/icons/academic-rank-icon.svg';
import AcademicDegreeIcon from '../../assets/icons/academic-degree-icon.svg';
import PositionIcon from '../../assets/icons/position-icon.svg';
import { IEmployeeData } from './types';
import EmployeeAcademicDegree from './employee.academic.degree.component';
import EmployeeAcademicRank from './employee.academic.rank.component';
import EmployeePosition from './employee.position.component';

const date = new Date('4/20/2024');
const fakeEmployee: IEmployeeData = {
  id: '123',
  firstName: 'Іван',
  lastName: 'Острозький',
  patronymic: 'Іванович',
  role: 'Викладач',
  birthDate: date,
  photoUrl:
    'https://static9.depositphotos.com/1729220/1229/i/950/depositphotos_12294106-stock-photo-chimpanzee-sitting-in-bed-on.jpg',
  email: 'foo@bar.baz',
  educations: [
    {
      specialty: 'Менеджмент',
      university: 'КНУ',
      diplomaDateOfIssue: date
    },
    {
      specialty: 'Кодер тупий',
      university: 'КНУ',
      diplomaDateOfIssue: date
    }
  ],
  positions: [
    {
      name: 'Доцент',
      startDate: date,
      endDate: date
    }
  ],
  academicRanks: [
    {
      name: 'Foo',
      dateOfIssue: date,
      certificateNumber: '#SD1233208'
    }
  ],
  academicDegrees: [
    {
      fullName: 'Foo bar baz',
      shortName: 'foo',
      dateOfIssue: date,
      diplomaNumber: '#SD1233208'
    }
  ]
};

const Profile = () => {
  return (
    // TODO: this component probably should take employee's id or smth to reuse it for admin
    <div className="px-10 py-5">
      <EmployeeInfo employee={fakeEmployee} />
      <ExpandableList
        title="Освіта"
        icon={EducationIcon}
        onAddClick={() => console.log('add')}
        onDeleteClick={(index: number) => console.log('delete ' + index)}
      >
        {fakeEmployee.educations.map((education, index) => (
          <EmployeeEducation education={education} key={index} />
        ))}
      </ExpandableList>
      <ExpandableList
        title="Наукові ступені"
        icon={AcademicDegreeIcon}
        onAddClick={() => console.log('add')}
        onDeleteClick={(index: number) => console.log('delete ' + index)}
      >
        {fakeEmployee.academicDegrees.map((academicDegree, index) => (
          <EmployeeAcademicDegree academicDegree={academicDegree} key={index} />
        ))}
      </ExpandableList>
      <ExpandableList
        title="Наукові звання"
        icon={AcademicRankIcon}
        onAddClick={() => console.log('add')}
        onDeleteClick={(index: number) => console.log('delete ' + index)}
      >
        {fakeEmployee.academicRanks.map((academicRank, index) => (
          <EmployeeAcademicRank academicRank={academicRank} key={index} />
        ))}
      </ExpandableList>
      <ExpandableList
        title="Посади"
        icon={PositionIcon}
        onAddClick={() => console.log('add')}
        onDeleteClick={(index: number) => console.log('delete ' + index)}
      >
        {fakeEmployee.positions.map((position, index) => (
          <EmployeePosition position={position} key={index} />
        ))}
      </ExpandableList>
    </div>
  );
};

export default Profile;
