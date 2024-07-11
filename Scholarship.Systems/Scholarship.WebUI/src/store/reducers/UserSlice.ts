import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { IUserInfo } from "../../models/user/IUserInfo";

export interface UserState {
    info: IUserInfo | null;
    isLoading: boolean,
    error: string | null,
}

const initialState: UserState = {
    error: null,
    isLoading: false,
    info: null,
}

export const userSlice = createSlice({
    name: 'user',
    initialState,
    reducers: {
        userFetching(state) {
            state.isLoading = true;
        },
        userFetchingSuccess(state, action: PayloadAction<IUserInfo>) {
            state.isLoading = false;
            state.error = null;
            state.info = action.payload;
        },
        userFetchingFailed(state, action: PayloadAction<string>) {
            state.isLoading = false;
            state.info = null;
            state.error = action.payload;
        },
        userClear(state) {
            state.isLoading = false;
            state.error = null;
            state.info = null;
        }
    }
});
export default userSlice.reducer;