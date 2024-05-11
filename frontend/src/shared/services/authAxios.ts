import axios from 'axios';
import { authService } from '../../services/auth/auth.service';
import { HttpStatusCode } from './types';
import { STORAGE_KEYS } from '../keys/storage-keys';

export const authAxios = axios.create();

authAxios.interceptors.response.use(
  (config) => config,
  async (error) => {
    const req = error.config;
    if (error.response.status === HttpStatusCode.UNAUTHORIZED && req && !req.isFirst) {
      req.isFirst = true;
      try {
        const response = await authService.refresh();
        localStorage.setItem(STORAGE_KEYS.JWT_TOKEN, response.accessToken)
        req.headers.Authorization = `Bearer ${response.accessToken}`;
        const res = await authAxios.request(req);
        return res;
      } catch (e) {
        return error;
      }
    }
  }
);
