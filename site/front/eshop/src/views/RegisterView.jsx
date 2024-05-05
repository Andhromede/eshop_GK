import React, { useEffect } from 'react';
import { useSelector } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { HOME } from '../constants/urls/urlFrontEnd';
import Register from '../components/account/Register';
import { selectIsLogged } from '../redux-store/authenticationSlice';


const RegisterView = () => {
    const navigate = useNavigate();
    let isAuthenticated = useSelector(selectIsLogged);

    useEffect(() => {
        if (isAuthenticated) navigate(HOME);
    }, []);

    return (
        <div className="" >
            <Register/>
        </div>
    );
 };
 
 export default RegisterView;
 