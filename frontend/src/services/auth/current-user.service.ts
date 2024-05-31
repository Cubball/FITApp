import { STORAGE_KEYS } from "../../shared/keys/storage-keys";

class CurrentUserService {
  getId(): string | undefined {
    const token = localStorage.getItem(STORAGE_KEYS.JWT_TOKEN);
    if (!token) {
      return undefined;
    }
    const userObject = JSON.parse(atob(token.split('.')[1]));
    return userObject.sub;
  }
}

export const currentUserService = new CurrentUserService();
