export interface IPosition {
  name: string;
  startDate: string;
  endDate: string | null; // може бути null, якщо позиція ще не закрита
}

export interface IEducation {
  university: string;
  specialty: string;
  diplomaDateOfIssue: string;
}

export interface IAcademicDegree {
  fullName: string;
  shortName: string;
  diplomaNumber: string;
  dateOfIssue: string;
}

export interface IAcademicRank {
  name: string;
  certificateNumber: string;
  dateOfIssue: string;
}

export interface IUser {
  userId: string;
  email: string;
  role: string;
  roleId: string;
}

export interface IEmployee {
  id: string;
  user: IUser;
  firstName: string;
  lastName: string;
  patronymic: string;
  birthDate: string;
  photo: string;
  positions: IPosition[];
  educations: IEducation[];
  academicDegrees: IAcademicDegree[];
  academicRanks: IAcademicRank[];
}

export interface IUpdateEmployeeBody {
  firstName: string;
  lastName: string;
  patronymic: string;
  birthDate: string; // TODO I am not sure about the type
}

export interface IAddPositionBody {
  name: string;
  startDate: string; // TODO type of date
  endDate: string;
}

export interface IAddEducationBody {
  university: string;
  specialty: string;
  diplomaDateOfIssue: string; // TODO type of date
}

export interface IAddAcademicDegreesBody {
  fullName: string;
  shortName: string;
  diplomaNumber: string;
  dateOfIssue: string; // TODO
}

export interface IAddAcademicRank {
  name: string;
  certificateNumber: string;
  dateOfIssue: string; // TODO
}
