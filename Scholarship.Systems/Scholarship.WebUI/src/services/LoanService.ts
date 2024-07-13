import { AxiosResponse } from "axios";
import { IAddLoanRequest, ICloseLoanRequest, ILoanInfo } from "../models/user/ILoanInfo";
import $api from "../utils/api";

class LoanService {
    public async getLoans(): Promise<AxiosResponse<ILoanInfo[]>> {
        return await $api.get<ILoanInfo[]>('/loans/list');
    } 
    public async addLoan(info: IAddLoanRequest): Promise<void> {
        return await $api.post('/loans/add', info)
    }
    public async closeLoan(info: ICloseLoanRequest): Promise<void> {
        return await $api.put('/loans/close', info);
    }
}
export const loanService = new LoanService();