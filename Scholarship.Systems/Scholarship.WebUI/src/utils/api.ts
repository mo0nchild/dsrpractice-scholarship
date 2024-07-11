import axios from "axios";
import { IAuthResponse } from "../models/user/IAuthResponse";
import { appStorage } from "./localstorage";
import { tokenManager } from "./token-manager";
import { store } from "../store/store";
import { userSlice } from "../store/reducers/UserSlice";

export const apiBaseUrl = `http://localhost:8082/api`;
export const refreshUrl = `/users/refresh`

export const accessTokenKey = 'accessToken'
export const refreshTokenKey = 'refreshToken'
export const roleKey = 'role'

const $api = axios.create({baseURL: apiBaseUrl})
$api.interceptors.request.use((config) => {
    const accessToken = appStorage.getItem(accessTokenKey);
    if (accessToken != null) {
        config.headers.Authorization = `Bearer ${accessToken}`
    }
    return config;
})
$api.interceptors.response.use(config => config, async (requestError) => {
    const originalRequest = requestError.config;
    if (requestError.response.status == 401 && requestError.config && !requestError.config._isRetry) {
        originalRequest._isRetry = true;
        try {
            const refreshToken = appStorage.getItem(refreshTokenKey);
            const response = await axios.get<IAuthResponse>(`${refreshUrl}?token=${refreshToken}`);
            
            console.log(response);
            appStorage.setItem(accessTokenKey, response.data.accessToken);
            appStorage.setItem(refreshTokenKey, response.data.refreshToken);
            console.log('Refresh token');

            return $api.request(originalRequest);
        } catch (error) {
            console.log('Not auth')
            tokenManager.removeAuth();
            store.dispatch(userSlice.actions.userClear())
        }
    }
    throw requestError;
})
export default $api;