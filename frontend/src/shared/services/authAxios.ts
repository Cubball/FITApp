import axios from "axios";

const authAxios = axios.create();

authAxios.interceptors.response.use(
//   (config) => config,
//   async (error) => {
//     const req = error.config;
//     if (error.response.status === 401 && req && !req.isFirst) {
//       req.isFirst = true;
//       try {
//         const response = await authHttp.refresh();
//         saveData(queryClient)(response);
//         req.headers.Authorization = `Bearer ${response.accessToken}`;
//         const res = await authAxios.request(req);
//         return res;
//       } catch (e) {
//         saveData(queryClient)(undefined);
//         return error;
//       }
//     }
//   }
);