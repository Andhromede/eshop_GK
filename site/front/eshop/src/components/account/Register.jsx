import React, { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { Field, Form, Formik } from "formik";
import RegisterSchema from "../../schema-yup/register-yup";
import apiBackEnd from '../../api/backend/api.Backend';
import { useDispatch } from "react-redux";
import { signIn } from "../../redux-store/authenticationSlice";
import { HOME } from "../../constants/urls/urlFrontEnd";
import { LOGIN, REGISTER } from '../../constants/urls/urlBackEnd';



const FormRegister = () => {
   const [errorLog, setErrorLog] = useState(false);
   const [errorMessage, setErrorMessage] = useState("");
   const dispatch = useDispatch();
   const navigate = useNavigate();

   // const handleRegister = (values) => {
   //    apiBackEnd.post(REGISTER, values)
   //       .then(res => {
   //          if (res.status === 200) {
   //             return apiBackEnd.post(LOGIN, values);
   //          }
   //       })
   //       .then(response => {
   //          if (response && response.status === 200) {
   //              dispatch(signIn(response.data));
   //              window.location.reload();
   //              navigate(HOME);
   //          } 
   //      })
   //       .catch(error => {
   //          setErrorLog(true);
   //          if (error.message.includes("Echec d'enregistrement !")) {
   //             setErrorMessage("Erreur lors de l'enregistrement !");
   //          }
   //          else {
   //             setErrorMessage('Cet email est déjà pris !');
   //          }
   //       });
   // };

   const handleRegister = (values) => {
      apiBackEnd.post(REGISTER, values)
         .then(res => {
            if (res.status === 200) {
               return apiBackEnd.post(LOGIN, values);
            } 
            else {
               throw new Error('Erreur lors de l\'enregistrement !');
            }
         })
         .then(response => {
            if (response && response.status === 200) {
               dispatch(signIn(response.data));
               navigate(HOME);
            } 
            else {
               throw new Error('Erreur lors de la connexion !');
            }
         })
         .catch(error => {
            setErrorLog(true);
            setErrorMessage(error.message || 'Une erreur inattendue s\'est produite !');
         });
   };



   return (
      <div className=" w-full max-w-md space-y-4 rounded-md bg-black bg-opacity-50 md:bg-opacity-85 py-8 px-4 sm:px-6 lg:px-8">

         <h2 className="mt-2 text-center text-3xl font-extrabold text-white">
            Inscription
         </h2>

         <hr />

         <Formik
            validationSchema={RegisterSchema}
            initialValues={{
               email: "",
               password: "",
               confirmPassword: ""
            }}
            onSubmit={handleRegister}
         >

            {({
               handleChange,
               handleBlur,
               values,
               touched,
               errors,
            }) => (
               <Form className="mt-8 space-y-6">
                  <div className="flex flex-col space-y-3 rounded-md shadow-sm">
                     <Field
                        type="text"
                        name="email"
                        id="email"
                        placeholder="Email"
                        value={values.email}
                        autoComplete="email"
                        className={
                           errors.email && touched.email
                              ? "border-2 border-rose-600 w-full rounded-[10px]"
                              : "border-2 w-full rounded-[10px]"
                        }
                        onChange={handleChange}
                        onBlur={handleBlur}
                     />

                     {errors.email && touched.email && (
                        <p className="error text-[#ff0000]">{errors.email}</p>
                     )}

                     <Field
                        type="password"
                        name="password"
                        placeholder="Password"
                        autoComplete="current-password"
                        value={values.password}
                        className={
                           errors.password && touched.password
                              ? "border-2 border-rose-600 w-full rounded-[10px]"
                              : "border-2 w-full rounded-[10px]"
                        }
                        onChange={handleChange}
                        onBlur={handleBlur}
                     />

                     {errors.password && touched.password && (
                        <p className="error text-[#ff0000]">{errors.password}</p>
                     )}

                     <Field
                        type="password"
                        name="confirmPassword"
                        placeholder="ConfirmPassword"
                        autoComplete="current-password"
                        value={values.confirmPassword}
                        className={
                           errors.confirmPassword && touched.confirmPassword
                              ? "border-2 border-rose-600 w-full rounded-[10px]"
                              : "border-2 w-full rounded-[10px]"
                        }
                        onChange={handleChange}
                        onBlur={handleBlur}
                     />

                     {errors.confirmPassword && touched.confirmPassword && (
                        <p className="error text-[#ff0000]">{errors.confirmPassword}</p>
                     )}
                  </div>
                  <div>

                     <button
                        type="submit"
                        className="btn bg-rose-400 hover:bg-rose-300 group relative w-full py-2 px-4 rounded mt-4"
                     >
                        S'inscrire
                     </button>

                     <div className="mt-5 text-center text-white">
                        Déjà inscrit ? C'est <Link to={LOGIN} className="text-rose-400 hover:text-rose-300 font-bold"> Par ici</Link> !
                     </div>
                  </div>

                  {errorLog && (
                     <div className="flex justify-center">
                        <small className="text-xl font-bold italic text-red-500">
                           {errorMessage}
                        </small>
                     </div>
                  )}
               </Form>
            )}
         </Formik>
      </div>
   );
};

export default FormRegister;
