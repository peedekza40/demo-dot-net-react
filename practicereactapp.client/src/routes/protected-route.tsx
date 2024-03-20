/* eslint-disable @typescript-eslint/no-unused-vars */
/* eslint-disable react-hooks/exhaustive-deps */
/* eslint-disable @typescript-eslint/no-explicit-any */
import React, { useEffect, useState } from 'react';
import { useNavigate, useLocation } from "react-router-dom";
import { useAuth } from "src/hooks/use-auth";

function ProtectedRoute(props: any) {
    const auth = useAuth();
    const navigate = useNavigate();
    const [isLoggedIn, setIsLoggedIn] = useState(false);

    const location = useLocation();
    const { pathname } = location;

    const checkUserIsLogin = () => {
        auth.isLogin(
            () => {
                setIsLoggedIn(true);
            },
            () => {
                setIsLoggedIn(false);
                return navigate('/login');
            });
    }

    useEffect(() => {
        checkUserIsLogin();
    }, [isLoggedIn]);

    return (
        <React.Fragment>
            {
                isLoggedIn ? props.children : null
            }
        </React.Fragment>
    );
}

export default ProtectedRoute;