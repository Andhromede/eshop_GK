import { React, useState, useEffect } from "react";
import { Link, useNavigate } from "react-router-dom";
import { BsBasket } from "react-icons/bs";
import { useSelector, useDispatch } from "react-redux";
import { selectIsLogged, signOut } from "../../redux-store/authenticationSlice";
import { LOGIN, REGISTER, HOME, PRODUCTS, PANIER } from "../../constants/urls/urlFrontEnd";
import { profilGet } from "./../../api/backend/profil";
import { productsInBasket, clearBasket } from '../../redux-store/basketSlice';

const Navbar = () => {
   const [currentClient, setCurrentClient] = useState(false);
   const navigate = useNavigate();
   const dispatch = useDispatch();
   const isLogged = useSelector(selectIsLogged);
   const client = useSelector((state) => state.auth.client);
   const id = client?.id;
   const role = client?.role.name;
   const tabProducts = useSelector(productsInBasket);

   useEffect(() => {
      if (isLogged) {
         profilGet(id)
            .then((result) => {
               if (result.status === 200) {
                  setCurrentClient(result.data);
               }
            })
            .catch((err) => console.log(err));
      }
   }, []);


   /********** DÉCONNEXION **********/
   const handleSignOut = () => {
      dispatch(signOut());
   };



   return (

      <nav className="bg-white border-gray-200 dark:bg-gray-900 dark:border-gray-700">

         <div className="max-w-screen-xl flex flex-wrap items-center mx-auto p-4">
            <a href="#" className="flex items-left mr-5">
               <img src="https://flowbite.com/docs/images/logo.svg" className="h-8 mr-3" alt="Flowbite Logo" />
               <span className="self-center text-2xl font-semibold whitespace-nowrap dark:text-white">Eshop</span>
            </a>

            <div className="flex flex-col font-medium p-4 md:p-0 mt-4 border border-gray-100 rounded-lg bg-gray-50 md:flex-row md:space-x-8 md:mt-0 md:border-0 md:bg-white dark:bg-gray-800 md:dark:bg-gray-900 dark:border-gray-700" id="navbar-dropdown">
               <ul className="flex flex-col font-medium p-4 md:p-0 mt-4 border border-gray-100 rounded-lg bg-gray-50 md:flex-row md:space-x-8 md:mt-0 md:border-0 md:bg-white dark:bg-gray-800 md:dark:bg-gray-900 dark:border-gray-700">
                  <li>
                     <Link to={HOME} className="block py-2 pl-3 pr-4 text-gray-900 rounded hover:bg-gray-100 md:hover:bg-transparent md:border-0 md:hover:text-blue-400 md:p-0 dark:text-white md:dark:hover:text-blue-200 dark:hover:bg-gray-700 dark:hover:text-white md:dark:hover:bg-transparent">
                        <span className="">Accueil</span>
                     </Link>
                  </li>

                  <li>
                     <Link to={PRODUCTS} className="mr-5 block py-2 pl-3 pr-4 text-gray-900 rounded hover:bg-gray-100 md:hover:bg-transparent md:border-0 md:hover:text-blue-400 md:p-0 dark:text-white md:dark:hover:text-blue-200 dark:hover:bg-gray-700 dark:hover:text-white md:dark:hover:bg-transparent">
                        <span className="">Produits</span>
                     </Link>
                  </li>

                  <li className="md:hover:text-blue-400 flex flex-col font-medium p-4 hover:bg-gray-100 md:p-0 mt-4 rounded-lg bg-gray-50 md:flex-row md:space-x-1 md:mt-0 md:border-0 md:bg-white dark:bg-gray-800 md:dark:bg-gray-900 dark:border-gray-700 text-white md:dark:hover:text-blue-200 dark:hover:bg-gray-700 dark:hover:text-white md:dark:hover:bg-transparent">
                     <a href={PANIER}><BsBasket className="text-xl" /></a>
                     <div className="text-red-500 md:border-0 md:p-0 dark:text-red-500 md:dark:hover:bg-transparent text-sm">
                        {tabProducts.length > 0 && (
                           <span>
                              {tabProducts.reduce((total, product) => total + product.quantity, 0)}
                           </span>
                        )}
                     </div>
                  </li>

                  <li className="my-auto block px-3 text-gray-900 rounded md:border-0 md:p-0 dark:text-white md:dark:hover:bg-transparent">
                     {role && (
                        <button id="role" className="text-blue-200 flex items-center text-sm">
                           ({role})
                        </button>
                     )}
                  </li>

                  {!isLogged && (
                     <li>
                        <Link to={LOGIN} className="block py-2 pl-3 pr-4 text-gray-900 rounded hover:bg-gray-100 md:hover:bg-transparent md:border-0 md:hover:text-blue-400 md:p-0 dark:text-white md:dark:hover:text-blue-200 dark:hover:bg-gray-700 dark:hover:text-white md:dark:hover:bg-transparent">
                           Connexion
                        </Link>
                     </li>
                  )}
                  {!isLogged && (
                     <li>
                        <Link to={REGISTER} className="block py-2 pl-3 pr-4 text-gray-900 rounded hover:bg-gray-100 md:hover:bg-transparent md:border-0 md:hover:text-blue-400 md:p-0 dark:text-white md:dark:hover:text-blue-200 dark:hover:bg-gray-700 dark:hover:text-white md:dark:hover:bg-transparent">
                           Inscription
                        </Link>
                     </li>
                  )}

                  {isLogged && (
                     <li>
                        <Link
                           to={HOME}
                           // onClick={handleSignOut}
                           onClick={() => {
                              handleSignOut();
                              dispatch(clearBasket());
                           }}
                           className="block py-2 pl-3 pr-4 text-gray-900 rounded hover:bg-gray-100 md:hover:bg-transparent md:border-0 md:hover:text-blue-400 md:p-0 dark:text-white md:dark:hover:text-blue-200 dark:hover:bg-gray-700 dark:hover:text-white md:dark:hover:bg-transparent">
                           Déconnexion
                        </Link>
                     </li>
                  )}
               </ul>
            </div>
         </div>
      </nav>






   );
};

export default Navbar;
