import { AxiosResponse } from "axios";

export type callbackSuccessType = (response: AxiosResponse) => any;
export type callbackErrorType = (error: any) => any;
export type callbackFinishType = () => any;

export type ErrorResponse = {
    errors: {
       code: string;
       description: string; 
    }[];
    succeeded: boolean
}