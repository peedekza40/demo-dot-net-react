import { AxiosResponse } from "axios";

export type callbackSuccessType = (response: AxiosResponse) => any;
export type callbackErrorType = (error: any) => any;
export type callbackFinishType = () => any;