export interface IPosition {
  name: string;
  startDate: Date;
  endDate: Date | null; // може бути null, якщо позиція ще не закрита
}

export interface IEducation {
  university: string;
  specialty: string;
  diplomaDateOfIssue: Date;
}

export interface IAcademicDegree {
  fullName: string;
  shortName: string;
  diplomaNumber: string;
  dateOfIssue: Date;
}

export interface IAcademicRank {
  name: string;
  certificateNumber: string;
  dateOfIssue: Date;
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
  birthDate: Date;
  photoUrl: string;
  positions: IPosition[];
  educations: IEducation[];
  academicDegrees: IAcademicDegree[];
  academicRanks: IAcademicRank[];
}

export interface IUpdateEmployeeBody {
  firstName: string;
  lastName: string;
  patronymic: string;
  birthDate: Date; // TODO I am not sure about the type
}

export interface IAddPositionBody {
  name: string;
  startDate: Date; // TODO type of date
  endDate: Date;
}

export interface IAddEducationBody {
  university: string;
  specialty: string;
  diplomaDateOfIssue: Date; // TODO type of date
}

export interface IAddAcademicDegreesBody {
  fullName: string;
  shortName: string;
  diplomaNumber: string;
  dateOfIssue: Date; // TODO
}

export interface IAddAcademicRank {
  name: string;
  certificateNumber: string;
  dateOfIssue: Date; // TODO
}
