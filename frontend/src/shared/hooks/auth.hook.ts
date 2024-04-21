import { useMutation, useQuery, useQueryClient } from 'react-query';
import { IAuth, IAuthData } from "../../services/auth/auth.types";
import { authService } from "../../services/auth/auth.service";
import { QUERY_KEYS } from "../keys/query-keys";
import { STORAGE_KEYS } from '../keys/storage-keys';

interface IUseAuthReturn {
    authData: IAuth | undefined;
    isRefreshLoading: boolean;
    handleLogin: (data: IAuthData) => void;
    isLoginLoading: boolean; 
}

export const useAuth = (): IUseAuthReturn => {
    const client = useQueryClient()

    const onError = () => {
        localStorage.setItem(STORAGE_KEYS.JWT_TOKEN, '')
        client.setQueryData([QUERY_KEYS.AUTH], {
            accessToken: '' 
        })
    }

    const onSuccess = (data) => {
        localStorage.setItem(STORAGE_KEYS.JWT_TOKEN, data.accessToken)
    }

    const {data: authData, isLoading: isRefreshLoading} = useQuery(
        [QUERY_KEYS.AUTH],
        async () => authService.refresh(),
        {onSuccess,onError, retry: false}
    );

    const {mutateAsync: handleLogin, isLoading: isLoginLoading} = useMutation(
        [QUERY_KEYS.AUTH],
        ({email, password}: IAuthData) => authService.login(email, password),
        {onSuccess,onError, retry: false}
     )

    return {authData, isRefreshLoading, handleLogin, isLoginLoading}
}