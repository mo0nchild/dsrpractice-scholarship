/// <reference types="redux-persist"/>
import { configureStore } from "@reduxjs/toolkit";
import { combineReducers } from "redux";

import userReducer from './reducers/UserSlice';
import loanReducer from './reducers/LoanSlice'

import sessionStorage from "redux-persist/lib/storage/session";
import { PersistConfig } from "redux-persist";
import persistReducer from "redux-persist/es/persistReducer";
import persistStore from "redux-persist/es/persistStore";

const persistConfig: PersistConfig<any> = {
    key: 'user',
    storage: sessionStorage, 
};
const rootReducer = combineReducers({
    user: persistReducer<ReturnType<typeof userReducer>>(persistConfig, userReducer),
    loan: loanReducer
})

export const store = configureStore({
    reducer: rootReducer,
    middleware: (getDefaultMiddleware) =>
        getDefaultMiddleware({
            serializableCheck: false
        }),
})
export const persistor = persistStore(store);

export type RootState = ReturnType<typeof rootReducer>;
export type AppStore = typeof store;
export type AppDispatch = AppStore['dispatch'];