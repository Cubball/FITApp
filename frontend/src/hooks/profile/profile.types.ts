import { IAddAcademicDegreesBody, IAddAcademicRank, IAddEducationBody, IAddPositionBody, IEmployee, IUpdateEmployeeBody } from "../../services/profile/profile.types";

export interface IUseProfileReturn {
  profile: IEmployee | undefined,
  isLoading: boolean,
  updateProfile: (body: IUpdateEmployeeBody) => void;
  addEducation: (body: IAddEducationBody) => void;
  deleteEducation: (index: number) => void;
  addAcademicRank: (body: IAddAcademicRank) => void;
  deleteAcademicRank: (index: number) => void;
  addAcademicDegree: (body: IAddAcademicDegreesBody) => void;
  deleteAcademicDegree: (index: number) => void;
  addPosition: (body: IAddPositionBody) => void;
  deletePosition: (index: number) => void;
}
