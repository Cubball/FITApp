import type { AxiosResponse } from 'axios';
import axios from 'axios';
import { STORAGE_KEYS } from '../keys/storage-keys';

export const mainAxios = axios.create({
  withCredentials: true
});

mainAxios.interceptors.response.use(
  (response): AxiosResponse<unknown, unknown> => response,
  async (error) => {
    if (Boolean(error.response) && error.response.status === 401) {
      localStorage.setItem(STORAGE_KEYS.JWT_TOKEN, '');
    }
    return Promise.reject(error);
  }
);
