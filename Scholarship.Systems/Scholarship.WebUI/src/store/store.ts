import { configureStore } from "@reduxjs/toolkit";
import { combineReducers } from "redux";

import userReducer from './reducers/UserSlice';

const rootReducer = combineReducers({
    user: userReducer
})

export const setupStore = () => {
    return configureStore({
        reducer: rootReducer
    })
}

export type RootState = ReturnType<typeof rootReducer>;
export type AppStore = ReturnType<typeof setupStore>;
export type AppDispatch = AppStore['dispatch'];