import { useSelector, useDispatch } from 'react-redux';
import { productsInBasket, addToBasket, removeToBasket, deleteToBasket } from '../../redux-store/basketSlice';
import { MdAddCircle, MdRemoveCircle } from 'react-icons/md';
import { FaTrashAlt } from 'react-icons/fa';
import { PRODUCTS } from "../../constants/urls/urlFrontEnd";
import Modal from "./Modal";
import { React, useState } from "react";
// import { basketTotalPrice } from '../../redux-store/basketSlice';


const BasketCard = ({ item }) => {
   const tabProducts = useSelector(productsInBasket);
   const dispatch = useDispatch();
   const [openModal, setOpenModal] = useState(false);
   // const totalPrice = useSelector(basketTotalPrice);

   const handleClick = () => {
      setOpenModal(true);
   };

   //    useEffect(() => {
   //       dispatch(calculTotalPrice());
   //   }, [dispatch, productsInBasket]);


   return (
      <div className="2xl:col-span-3 lg:col-span-4 md:col-span6 sm:col-span-12 m-5">
         {/* <div className="flex h-full flex-col overflow-y-scroll bg-white shadow-xl bg-opacity-70  rounded-2xl"> */}
         <div className="flex h-full flex-col overflow-y-scroll bg-gray-900 shadow-xl bg-opacity-70  rounded-2xl">
            <div className="flex-1 overflow-y-auto px-4 py-6 sm:px-6">

               <div className="flex items-start justify-between">
                  <div className="ml-3 flex h-7 items-center">
                     <button type="button" className="relative -m-2 p-2 text-gray-400 hover:text-gray-500">
                        <span className="absolute -inset-0.5" />

                        <span className="sr-only">Close panel</span>
                     </button>
                  </div>
               </div>

               <div className="mt-8">
                  <div className="flow-root">
                     <ul role="list" className="-my-6 divide-y divide-gray-200">
                        <li key={item.id} className="flex py-6">

                           <div className="h-24 w-24 flex-shrink-0 overflow-hidden rounded-md border border-gray-200">
                              <img src={item.image} alt={item.image} className="h-full w-full object-cover object-center border-2 border-gray-400 rounded-lg" />
                           </div>

                           <div className="ml-4 flex flex-1 flex-col">
                              <div>
                                 {/* <div className="flex justify-between text-base font-medium text-gray-900"> */}
                                 <div className="flex justify-between text-base font-medium text-gray-200">
                                    <h3>
                                       <a href="">{item.name}</a>
                                    </h3>

                                    <p className="ml-4">({item.price})</p>
                                 </div>

                                 <p className="mt-1 text-sm text-neutral-400">{item.color}</p>
                              </div>

                              <div className="flex flex-1 items-end text-sm grid grid-cols-12">
                                 <button
                                    onClick={() => dispatch(removeToBasket(item))}
                                    id={item.id} 
                                    name="btnRemove"
                                    className="col-span-3 mx-auto">

                                    <MdRemoveCircle className="text-terciary text-2xl text-rose-300" />
                                 </button>

                                 <div className="col-span-2 text-center">
                                    {/* <span className="text-neutral-400 text-md">Qte : </span> */}
                                    <span className="text-white text-xl">{item.quantity}</span>
                                 </div>

                                 <button
                                    onClick={() => dispatch(addToBasket(item))}
                                    id={item.id}
                                    name="btnAdd"
                                    className="col-span-3 mx-auto">

                                    <MdAddCircle className="text-terciary text-2xl text-rose-300" />
                                 </button>

                                 <div className="flex col-span-3 mx-auto pb-1">
                                    <button
                                       onClick={() => dispatch(deleteToBasket(item))}
                                       className="text-white text-sm hover:text-red-600">

                                       <FaTrashAlt className="text-green-200 text-lg" />
                                       {/* Supprimer */}
                                    </button>
                                 </div>
                              </div>
                           </div>
                        </li>
                     </ul>
                  </div>
               </div>
            </div>

            <div className="border-t border-gray-200 px-4 py-6 sm:px-6">
               <div className="flex justify-between text-base font-medium text-gray-900">
                  <p>Total :</p>

                  {(tabProducts.find(objects => objects.id === item.id)) &&
                     <span>
                        {item.totalPrice &&
                           (item.totalPrice).toLocaleString()
                        } €
                     </span>
                  }
               </div>

               <p className="mt-0.5 text-sm text-gray-500">Prix hors frais de livraisons.</p>

               <div className="mt-6">
                  <div onClick={handleClick} className="flex items-center justify-center rounded-md border border-transparent bg-indigo-500 px-6 py-3 text-base font-medium text-gray-200 shadow-sm hover:bg-indigo-600">
                     Detail du produit
                  </div>
               </div>

               <div className="mt-6 flex justify-center text-center text-sm text-gray-500">
                  <p className="">ou
                     <a href={PRODUCTS} type="button" className="font-medium text-indigo-500 hover:text-indigo-500 ml-2">
                        Continuer le Shopping
                        <span aria-hidden="true"> &rarr;</span>
                     </a>
                  </p>
               </div>
            </div>
         </div>

         <Modal open={openModal} onClose={() => setOpenModal(false)} item={item} />
      </div>
   );
};

export default BasketCard;
