import axios from "axios";
import config from "src/config.json";
import { callbackSuccessType, callbackErrorType, callbackFinishType } from "src/utils/global-type";
import RoleForm from "src/models/RoleForm";

export async function search(value: any,
    callbackSuccess?: callbackSuccessType,
    callbackError?: callbackErrorType,
    callbackFinish?: callbackFinishType) {
    axios.post(config.basePathAPI + 'RoleManagement/Search', value,
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

export async function getById(id: string,
    callbackSuccess?: callbackSuccessType,
    callbackError?: callbackErrorType,
    callbackFinish?: callbackFinishType) {
    axios.post(config.basePathAPI + 'RoleManagement/GetById', id,
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

export async function isExists(id: string): Promise<{ data: boolean }> {
    const configRequest = {
        headers: {
            'Content-Type': 'application/json'
        }
    };

    return axios.post(config.basePathAPI + 'RoleManagement/IsExists', id, configRequest);
}

export async function saveRole(value: RoleForm,
    callbackSuccess?: callbackSuccessType,
    callbackError?: callbackErrorType,
    callbackFinish?: callbackFinishType) {
    axios.post(config.basePathAPI + 'RoleManagement/SaveRole', value,
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