import React, { useEffect } from 'react';
import { useSelector } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { HOME } from '../constants/urls/urlFrontEnd';
import Login from '../components/account/Login';
import { selectIsLogged } from '../redux-store/authenticationSlice';


const LoginView = () => {
    const navigate = useNavigate();
    let isAuthenticated = useSelector(selectIsLogged);

    useEffect(() => {
        if (isAuthenticated) navigate(HOME);
    }, []);

    return (
        <div className="" >
            <Login/>
        </div>
    );
 };
 
 export default LoginView;
 