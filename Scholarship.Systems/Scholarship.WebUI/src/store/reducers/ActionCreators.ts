import { AxiosError } from "axios";
import { userService } from "../../services/UserService";
import { tokenManager } from "../../utils/token-manager";
import { AppDispatch } from "../store";
import { userSlice } from "./UserSlice";

export const fetchUser = () => async (dispatch: AppDispatch) => {
    try {
        dispatch(userSlice.actions.userFetching());
        const userInfo = await userService.getInfo();
        console.log(userInfo)
        dispatch(userSlice.actions.userFetchingSuccess(userInfo.data));
    }
    catch (error: any) {
        if (error instanceof AxiosError) {
            dispatch(userSlice.actions.userFetchingFailed(error.response?.data.errors));
        }
    }
}
export const loginUser = (email: string, password: string) => async (dispatch: AppDispatch) => {
    try {
        dispatch(userSlice.actions.userFetching());
        const authInfo = await userService.login(email, password);
        tokenManager.setAuth(authInfo.data);
        dispatch(fetchUser());
    }
    catch (error: any) {
        if (error instanceof AxiosError) {
            dispatch(userSlice.actions.userFetchingFailed(error.response?.data.errors));
        }
    }
}
export const logoutUser = () => async (dispatch: AppDispatch) => {
    tokenManager.removeAuth();
    dispatch(userSlice.actions.userClear());
}