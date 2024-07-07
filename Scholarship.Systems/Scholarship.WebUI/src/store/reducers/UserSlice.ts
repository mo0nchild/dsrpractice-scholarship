import { createSlice } from "@reduxjs/toolkit";
import { IUserInfo } from "../../models/IUserInfo";

export interface UserState {
    user: IUserInfo | null;
    isLoading: boolean,
    error: string | null,
}

const initialState: UserState = {
    error: null,
    isLoading: false,
    user: null,
}

export const userSlice = createSlice({
    name: 'user',
    initialState,
    reducers: {

    }
});
export default userSlice.reducer;