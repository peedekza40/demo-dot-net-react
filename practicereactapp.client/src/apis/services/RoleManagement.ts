import axios from "axios";
import config from "src/config.json";
import { callbackSuccessType, callbackErrorType, callbackFinishType } from "src/utils/global-type";

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
        .then(function(response) {
            if (callbackSuccess !== undefined && callbackSuccess !== null) {
                callbackSuccess(response);
            }
        })
        .catch(function(error) {
            if (callbackError !== undefined && callbackError !== null) {
                callbackError(error);
            }
        })
        .finally(function() {
            if (callbackFinish !== undefined && callbackFinish !== null) {
                callbackFinish();
            }
        });
}