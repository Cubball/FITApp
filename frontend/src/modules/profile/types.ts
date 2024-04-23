export interface IEmployeeData {
  id: string;
  email: string;
  role: string;
  firstName: string;
  lastName: string;
  patronymic: string;
  birthDate: Date;
  photoUrl: string;
  positions: IEmployeePosition[];
  educations: IEmployeeEducation[];
  academicDegrees: IEmployeeAcademicDegree[];
  academicRanks: IEmployeeAcademicRank[];
}

export interface IEmployeePosition {
  name: string;
  startDate: Date;
  endDate: Date;
}

export interface IEmployeeEducation {
  university: string;
  specialty: string;
  diplomaDateOfIssue: Date;
}

export interface IEmployeeAcademicDegree {
  fullName: string;
  shortName: string;
  diplomaNumber: string;
  dateOfIssue: Date;
}

export interface IEmployeeAcademicRank {
  name: string;
  certificateNumber: string;
  dateOfIssue: Date;
}
