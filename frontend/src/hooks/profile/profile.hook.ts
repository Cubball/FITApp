import { IUseProfileReturn } from "./profile.types";
import { useMyProfile } from "./my-profile.hook";

export const useProfile = (employeeId: string | undefined): IUseProfileReturn => {
  if (!employeeId) {
    return useMyProfile();
  }

  // TODO: make a hook that takes in employeeId
  return useMyProfile();
}
