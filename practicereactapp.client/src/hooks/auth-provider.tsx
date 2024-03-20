/* eslint-disable @typescript-eslint/no-explicit-any */
import { createContext, useState, ReactNode } from "react";
import axios, { AxiosResponse } from "axios";

export const AuthContext = createContext(null);

type Props = {
    children: ReactNode
}

const userProfileStorageKey = "userProfile";
export class UserProfile {
    public userName: string;
    public email: string;
    public firstName: string;
    public lastName: string;
    public photoURL: string = '/assets/images/avatars/avatar_25.jpg';
    public role: string;

    public get displayName() {
        return `${this.firstName} ${this.lastName}`;
    }
}

function AuthProvider({ children }: Props) {
    const userProfileStorageValue = localStorage.getItem(userProfileStorageKey);
    const [userProfile, setUserProfile] = useState<UserProfile | null>(userProfileStorageValue ? Object.assign(new UserProfile(), JSON.parse(userProfileStorageValue)) : null);

    const login = (values: any,
        callbackSuccess: (response: AxiosResponse) => any,
        callbackError: (error: any) => any,
        callbackFinish: () => any) => {

        axios.post('api/login?useCookies=true', {
            "email": values.email,
            "password": values.password
        },
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
    };

    const logout = (callbackSuccess: (response: AxiosResponse) => any) => {
        axios.post('api/Account/Logout')
            .then(function(response) {
                if (callbackSuccess !== undefined && callbackSuccess !== null) {
                    callbackSuccess(response);
                }
            });
    };

    const isLogin = (callbackSuccess: (response: AxiosResponse) => any,
        callbackError: (error: any) => any,
        callbackFinish: () => any) => {

        axios.get('api/Account/IsLogin')
            .then(function(response) {
                if (callbackSuccess !== undefined && callbackSuccess !== null) {
                    callbackSuccess(response);
                }
            })
            .catch(function(error) {
                if (callbackError !== undefined && callbackError !== null) {
                    callbackError(error);
                    localStorage.removeItem(userProfileStorageKey);
                }
            })
            .finally(function() {
                if (callbackFinish !== undefined && callbackFinish !== null) {
                    callbackFinish();
                }
            });
    };

    //get user profile
    if (!userProfileStorageValue) {
        axios.get('api/Account/GetCurrentUserProfile')
            .then(function (response) {
                localStorage.setItem(userProfileStorageKey, JSON.stringify(response.data));
                setUserProfile(Object.assign(new UserProfile(), response.data));
            });
    }

    return (
        <AuthContext.Provider value={{ login, logout, isLogin, userProfile }}>
            {children}
        </AuthContext.Provider>
    );
}

export default AuthProvider;

