import { AxiosError } from "axios";
import { userService } from "../../services/UserService";
import { tokenManager } from "../../utils/token-manager";
import { AppDispatch } from "../store";
import { userSlice } from "./UserSlice";
import { IRegistrationInfo } from "../../models/user/IRegistrationInfo";
import { loanSlice } from "./LoanSlice";
import { loanService } from "../../services/LoanService";
import { IAddLoanRequest, ICloseLoanRequest } from "../../models/user/ILoanInfo";

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
        tokenManager.setAuth((await userService.login(email, password)).data);
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
export const registrateUser = (info: IRegistrationInfo) => async (dispatch: AppDispatch) => {
    try {
        dispatch(userSlice.actions.userFetching());
        tokenManager.setAuth((await userService.registration(info)).data)
        dispatch(fetchUser());
    }
    catch (error: any) {
        if (error instanceof AxiosError) {
            dispatch(userSlice.actions.userFetchingFailed(error.response?.data.errors));
        }
    }
}

export const fetchLoans = () => async (dispatch: AppDispatch) => {
    try {
        dispatch(loanSlice.actions.loansFetching());
        const loansList = (await loanService.getLoans()).data.filter(item => item.closeTime == null)
            .sort((a, b) => Date.parse(a.beforeTime) - Date.parse(b.beforeTime));
        dispatch(loanSlice.actions.loansFetchingSuccess(loansList));
    }
    catch (error: any) {
        if (error instanceof AxiosError) {
            dispatch(loanSlice.actions.loansFetchingFailed(error.response?.data.errors));
        }
    }
}
export const fetchClosedLoans = () => async (dispatch: AppDispatch) => {
    try {
        dispatch(loanSlice.actions.loansFetching());
        const loansList = (await loanService.getLoans()).data.filter(item => item.closeTime != null)
            .sort((a, b) => Date.parse(b.beforeTime) - Date.parse(a.beforeTime));
        dispatch(loanSlice.actions.loansFetchingSuccess(loansList));
    }
    catch (error: any) {
        if (error instanceof AxiosError) {
            dispatch(loanSlice.actions.loansFetchingFailed(error.response?.data.errors));
        }
    }
}
export const addLoan = (info: IAddLoanRequest) => async (dispatch: AppDispatch) => {
    try {
        dispatch(loanSlice.actions.loansFetching());
        await loanService.addLoan(info);
        dispatch(fetchLoans())
    }
    catch (error: any) {
        if (error instanceof AxiosError) {
            dispatch(loanSlice.actions.loansFetchingFailed(error.response?.data.errors));
        }
    }
}
export const closeLoan = (info: ICloseLoanRequest) => async (dispatch: AppDispatch) => {
    try {
        dispatch(loanSlice.actions.loansFetching());
        await loanService.closeLoan(info);
        dispatch(fetchLoans())
    }
    catch (error: any) {
        if (error instanceof AxiosError) {
            dispatch(loanSlice.actions.loansFetchingFailed(error.response?.data.errors));
        }
    }
}