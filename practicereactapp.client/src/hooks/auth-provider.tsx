/* eslint-disable @typescript-eslint/no-explicit-any */
import { createContext, useState, ReactNode } from "react";
import axios from "axios";
import { callbackSuccessType, callbackErrorType, callbackFinishType } from "src/utils/global-type";
import config from "src/config.json";

import UserProfile from "@/models/UserProfile";
import LoginForm from "@/models/LoginForm";
import Menu from "@/models/Menu";

//declare type user profile
type Props = {
    children: ReactNode
}

interface IAuthContextValue {
    login(
        values: LoginForm,
        callbackSuccess?: callbackSuccessType,
        callbackError?: callbackErrorType,
        callbackFinish?: callbackFinishType
    ): void,
    logout(
        callbackSuccess?: callbackSuccessType
    ): void,
    isHavePermission(
        path: string,
        callbackSuccess?: callbackSuccessType,
        callbackError?: callbackErrorType,
        callbackFinish?: callbackFinishType
    ): void,
    updateState(): void,
    userProfile: UserProfile | null,
    menus: Menu[] | null
}

const menuStorageKey = "menu";
const userProfileStorageKey = "userProfile";

export const AuthContext = createContext<IAuthContextValue>(null!);

function AuthProvider({ children }: Props) {
    const userProfileStorageValue = localStorage.getItem(userProfileStorageKey);
    const menuStorageValue = localStorage.getItem(menuStorageKey);

    const [userProfile, setUserProfile] = useState<UserProfile | null>(userProfileStorageValue ? Object.assign(new UserProfile(), JSON.parse(userProfileStorageValue)) : null);
    const [menus, setMenus] = useState<Menu[] | null>(menuStorageValue ? JSON.parse(menuStorageValue ?? "") : null);

    const getUserProfile = () => {
        if (!userProfileStorageValue) {
            axios.get(config.basePathAPI + 'Account/GetCurrentUserProfile')
            .then(function (response) {
                localStorage.setItem(userProfileStorageKey, JSON.stringify(response.data));
                setUserProfile(Object.assign(new UserProfile(), response.data));
            });
        }
    };

    const getMenus = () => {
        if (!menuStorageValue) {
            axios.get(config.basePathAPI + 'Account/GetCurrentUserMenus')
            .then(function (response) {
                localStorage.setItem(menuStorageKey, JSON.stringify(response.data.data));
                setMenus(response.data.data);
            });
        }
    }

    const login = (
        values: LoginForm,
        callbackSuccess?: callbackSuccessType,
        callbackError?: callbackErrorType,
        callbackFinish?: callbackFinishType
    ) => {

        axios.post(config.basePathAPI + 'login?useCookies=true', {
            "email": values.email,
            "password": values.password
        },
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
    };

    const logout = (callbackSuccess?: callbackSuccessType) => {
        axios.post(config.basePathAPI + 'Account/Logout')
            .then(function (response) {
                if (callbackSuccess !== undefined && callbackSuccess !== null) {
                    callbackSuccess(response);
                }
            });
    };

    const isHavePermission = (
        path: string,
        callbackSuccess?: callbackSuccessType,
        callbackError?: callbackErrorType,
        callbackFinish?: callbackFinishType
    ) => {
        axios.post(config.basePathAPI + 'Account/IsHavePermission', path,
        {
            headers: {
                'Content-Type': 'application/json'
            }
        }
        )
            .then(function(response) {
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
    };

    const updateState = () => {
        getUserProfile();
        getMenus();
    };

    return (
        <AuthContext.Provider value={{ login, logout, isHavePermission, updateState, userProfile, menus }}>
            {children}
        </AuthContext.Provider>
    );
}

export default AuthProvider;