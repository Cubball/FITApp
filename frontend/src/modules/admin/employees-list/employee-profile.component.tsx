import { IEmployee } from '../../../services/profile/profile.types';
import Profile from '../../../shared/components/profile/profile.component';

const date = new Date('4/20/2024');
const fakeEmployee: IEmployee = {
  id: '123',
  firstName: 'Іван',
  lastName: 'Острозький',
  patronymic: 'Іванович',
  user: {
    userId: '1',
    roleId: '1',
    email: 'foo@bar.baz',
    role: 'Викладач'
  },
  birthDate: date,
  photoUrl:
    'https://static9.depositphotos.com/1729220/1229/i/950/depositphotos_12294106-stock-photo-chimpanzee-sitting-in-bed-on.jpg',
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
const EmployeeProfile = () => {
  // TODO: get id from route -> fetch user
  const canEdit = true; // TODO: get permissions and determine whether admin can edit employees

  return <Profile employee={fakeEmployee} canEdit={canEdit} isOwnProfile={false}/>;
};

export default EmployeeProfile;
