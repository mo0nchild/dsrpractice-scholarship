import { AxiosResponse } from "axios"

export type RequestAction<T> = () => Promise<AxiosResponse<T>>

export default async function authRequest<TReq>(request: RequestAction<TReq>): Promise<void> {
    
}