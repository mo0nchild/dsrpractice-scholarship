import { AxiosResponse } from "axios";
import { IAuthResponse } from "../models/user/IAuthResponse";
import $api from "../utils/api";
import { IRegistrationInfo } from "../models/user/IRegistrationInfo";
import { IUserInfo } from "../models/user/IUserInfo";

class UserService {
    public async login(email: string, password: string): Promise<AxiosResponse<IAuthResponse>> {
        return await $api.get(`/users/login?email=${email}&password=${password}`);
    }
    public async registration(info: IRegistrationInfo): Promise<AxiosResponse<IAuthResponse>> {
        return await $api.post(`/users/add`, {...info})
    }
    public async getInfo(): Promise<AxiosResponse<IUserInfo>> {
        return await $api.get(`/users/info`)
    }
}
export const userService = new UserService();