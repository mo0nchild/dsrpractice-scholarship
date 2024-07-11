import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { ILoanInfo } from "../../models/user/ILoanInfo";

export interface LoansState {
    list: ILoanInfo[],
    isLoading: boolean,
    error: string | null
}

const initialState: LoansState = {
    error: null,
    isLoading: false,
    list: []
}

export const loanSlice = createSlice({
    name: 'loan',
    initialState,
    reducers: {
        loansFetching(state) {
            state.isLoading = true;
        },
        loansFetchingSuccess(state, action: PayloadAction<ILoanInfo[]>) {
            state.isLoading = false;
            state.error = null;
            state.list = action.payload;
        },
        loansFetchingFailed(state, action: PayloadAction<string>) {
            state.isLoading = false;
            state.list = [];
            state.error = action.payload;
        },
    }
})
export default loanSlice.reducer;