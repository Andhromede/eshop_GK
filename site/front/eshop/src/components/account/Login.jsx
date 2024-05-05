import { Field, Form, Formik } from "formik";
import React, { useState } from "react";
import { useDispatch } from "react-redux";
import { Link, useNavigate } from "react-router-dom";
import LoginSchema from "../../schema-yup/login-yup";
import { HOME, REGISTER } from "../../constants/urls/urlFrontEnd";
import { signIn } from "../../redux-store/authenticationSlice";
import apiBackEnd from '../../api/backend/api.Backend';
import { LOGIN } from '../../constants/urls/urlBackEnd';




const FormLogin = () => {
   const [errorLog, setErrorLog] = useState(false);
   const dispatch = useDispatch();
   const navigate = useNavigate();



   // const handleLogin = (values) => {
   //    apiBackEnd.post(LOGIN, values)
   //      .then((res) => {
   //        if (res.status === 200) {
   //          dispatch(signIn());
   //       //    dispatch(signIn({
   //       //       email: values.email
   //       //   }));
   //          window.location.reload()
   //          navigate(HOME);
   //        } 
   //      })
   //      .catch((err) => {
   //       console.log(err);
   //        setErrorLog(true);
   //      }
   //      );
   // };

   const handleLogin = (values) => {
      apiBackEnd.post(LOGIN, values)
          .then((res) => {
              if (res.status === 200) {
                  const clientData = res.data;
                  dispatch(signIn({
                      email: clientData.email,
                      role: clientData.role,
                      id: clientData.id,
                  }));
                  window.location.reload();
                  navigate(HOME);
              }
          })
          .catch((err) => {
              console.log(err);
              setErrorLog(true);
          });
  };

   return (
      <div className="lg:my-8 w-full max-w-md space-y-8 rounded-md bg-black bg-opacity-50 md:bg-opacity-85 p-8 px-4 shadow sm:px-6 lg:px-8">
         {/* <div className=" hidden md:flex justify-center" >
            <img className="h-24 " src="/assets/images/fond.jpg" alt="logo de space-trip" />
         </div> */}

         <h2 className="mt-6 text-center text-3xl font-extrabold text-white">
            Connexion
         </h2>
         <hr />

         <Formik
            validationSchema={LoginSchema}
            initialValues={{
               email: "",
               password: "",
            }}
            onSubmit={handleLogin}
         >

            {({ handleChange, handleBlur, values, touched, isSubmitting, errors, }) => (
               <Form className="mt-8 space-y-6">
                  <div className="flex flex-col space-y-3 rounded-md shadow-sm">
                     <Field
                        type="text"
                        name="email"
                        placeholder="Email"
                        value={values.email}
                        autoComplete="email"
                        className={errors.email && touched.email ? "border-2 border-rose-600 w-full rounded-[10px]" : "border-2 w-full rounded-[10px]"}
                        onChange={handleChange}
                        onBlur={handleBlur}
                     />

                     {errors.email && touched.email && <p className='error text-[#ff0000]'>{errors.email}</p>}

                     <Field
                        type="password"
                        name="password"
                        placeholder="Password"
                        autoComplete="current-password"
                        value={values.password}
                        className={errors.password && touched.password ? "border-2 border-rose-600 w-full rounded-[10px]" : "border-2 w-full rounded-[10px]"}
                        onChange={handleChange}
                        onBlur={handleBlur}
                     />

                     {errors.password && touched.password && <p className='error text-[#ff0000]'>{errors.password}</p>}

                  </div>

                  <div>
                     <button type="submit" className="btn bg-rose-400 hover:bg-rose-300 group relative w-full py-2 px-4 rounded">
                        Se connecter
                     </button>

                     <div className="mt-5 text-center text-white">
                        Pas encore inscrit ? C'est
                        <Link to={REGISTER} className="text-rose-400 hover:text-rose-300 font-bold"> Par ici </Link>
                        !
                     </div>

                  </div>
                  {errorLog && (
                     <div className="flex justify-center">
                        <small className="text-sm italic text-red-600">
                           Utilisateur/Mot de passe incorrect(s)
                        </small>
                     </div>
                  )}
               </Form>
            )}
         </Formik>
      </div>
   );
};

export default FormLogin;
