import EmployeeInfo from './info/employee.info.component';
import EmployeeEducations from './educations/employee-educations.component';
import EmployeeAcademicDegrees from './academic-degrees/employee-academic-degrees.component';
import EmployeeAcademicRanks from './academic-ranks/employee-academic-ranks.component';
import EmployeePositions from './positions/employee-positions.component';
import { useParams } from 'react-router-dom';
import { useProfile } from '../../hooks/profile/profile.hook';
import Loading from '../loading';
import Error from '../error';

interface ProfileProps {
  canEdit: boolean;
  isOwnProfile: boolean;
}

const Profile = ({ canEdit, isOwnProfile }: ProfileProps) => {
  let id: string | undefined = undefined;
  if (!isOwnProfile) {
    const { employeeId } = useParams();
    id = employeeId;
  }

  const {
    profile,
    updateProfile,
    isLoading,
    addAcademicRank,
    deleteAcademicRank,
    addAcademicDegree,
    deleteAcademicDegree,
    addEducation,
    deleteEducation,
    addPosition,
    deletePosition,
    uploadPhoto,
    deletePhoto
  } = useProfile(id);

  if (isLoading) {
    return <Loading />;
  }

  if (!profile) {
    return <Error />
  }

  return (
    <div className="px-10 py-5">
      <EmployeeInfo
        employee={profile}
        canEdit={canEdit}
        onSumbit={updateProfile}
        onPhotoUpload={uploadPhoto}
        onPhotoDelete={deletePhoto}
      />
      <EmployeeEducations
        employee={profile}
        canEdit={canEdit}
        onSubmit={addEducation}
        onDelete={deleteEducation}
      />
      <EmployeeAcademicDegrees
        employee={profile}
        canEdit={canEdit}
        onSubmit={addAcademicDegree}
        onDelete={deleteAcademicDegree}
      />
      <EmployeeAcademicRanks
        employee={profile}
        canEdit={canEdit}
        onSubmit={addAcademicRank}
        onDelete={deleteAcademicRank}
      />
      <EmployeePositions
        employee={profile}
        canEdit={canEdit}
        onSubmit={addPosition}
        onDelete={deletePosition}
      />
    </div>
  );
};

export default Profile;
