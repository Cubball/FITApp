import { IUseProfileReturn } from "./profile.types";
import { useMyProfile } from "./my-profile.hook";
import { useEmployeeProfile } from "./employee-profile.hook";

export const useProfile = (employeeId: string | undefined): IUseProfileReturn => {
  if (!employeeId) {
    return useMyProfile();
  }

  return useEmployeeProfile(employeeId);
}
