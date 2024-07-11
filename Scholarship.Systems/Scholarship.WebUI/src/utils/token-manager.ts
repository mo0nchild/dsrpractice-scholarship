import { IAuthResponse } from "../models/user/IAuthResponse";
import { accessTokenKey, refreshTokenKey, roleKey } from "./api";
import { appStorage } from "./localstorage";

class TokenManager {
    public setAuth(info: IAuthResponse): void {
        appStorage.setItem(accessTokenKey, info.accessToken);
        appStorage.setItem(refreshTokenKey, info.refreshToken);
        appStorage.setItem(roleKey, info.role);
    }
    public removeAuth(): void {
        appStorage.removeItem(accessTokenKey);
        appStorage.removeItem(refreshTokenKey);
        appStorage.removeItem(roleKey);
    }
}
export const tokenManager = new TokenManager();