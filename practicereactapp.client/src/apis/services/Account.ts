import axios from "axios";
import config from "src/config.json";
import { callbackSuccessType, callbackErrorType, callbackFinishType } from "src/utils/global-type";
import RegisterForm from "src/models/RegisterForm";

export async function register(value: RegisterForm,
    callbackSuccess?: callbackSuccessType,
    callbackError?: callbackErrorType,
    callbackFinish?: callbackFinishType) {
    axios.post(config.basePathAPI + 'Account/Register', value,
        {
            headers: {
                'Content-Type': 'application/json'
            }
        })
        .then(function (response) {
            if (callbackSuccess !== undefined && callbackSuccess !== null) {
                callbackSuccess(response);
            }
        })
        .catch(function (error) {
            if (callbackError !== undefined && callbackError !== null) {
                callbackError(error);
            }
        })
        .finally(function () {
            if (callbackFinish !== undefined && callbackFinish !== null) {
                callbackFinish();
            }
        });
}

export async function isExistsEmail(email: string): Promise<{ data: boolean }> {
    const configRequest = {
        headers: {
            'Content-Type': 'application/json'
        }
    };

    return axios.post(config.basePathAPI + 'Account/IsExistsEmail', email, configRequest);
}

export async function getCurrentUserMenus(
    callbackSuccess?: callbackSuccessType,
    callbackError?: callbackErrorType,
    callbackFinish?: callbackFinishType) {
    axios.get(config.basePathAPI + 'Account/GetCurrentUserMenus',
        {
            headers: {
                'Content-Type': 'application/json'
            }
        })
        .then(function (response) {
            if (callbackSuccess !== undefined && callbackSuccess !== null) {
                callbackSuccess(response);
            }
        })
        .catch(function (error) {
            if (callbackError !== undefined && callbackError !== null) {
                callbackError(error);
            }
        })
        .finally(function () {
            if (callbackFinish !== undefined && callbackFinish !== null) {
                callbackFinish();
            }
        });
}