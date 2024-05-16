/* eslint-disable @typescript-eslint/no-unused-vars */
/* eslint-disable react-hooks/exhaustive-deps */
/* eslint-disable @typescript-eslint/no-explicit-any */
import React, { useEffect, useState } from 'react';
import { useNavigate, useLocation } from "react-router-dom";
import { useAuth } from "src/hooks/use-auth";

function ProtectedRoute(props: any) {
    const auth = useAuth();
    const navigate = useNavigate();
    const [isHavePermission, setIsHavePermission] = useState(false);

    const location = useLocation();
    const { pathname } = location;

    const checkUserIsHavePermission = () => {
        auth.isHavePermission(
            pathname,
            () => {
                setIsHavePermission(true);
            },
            (error) => {
                setIsHavePermission(false);
                if(error.response.status == 403){
                    return navigate('/403');
                }
                else{
                    return navigate('/login');
                }
            });
    }

    useEffect(() => {
        checkUserIsHavePermission();
    }, [isHavePermission]);

    return (
        <React.Fragment>
            {
                isHavePermission ? props.children : null
            }
        </React.Fragment>
    );
}

export default ProtectedRoute;