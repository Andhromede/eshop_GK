import { React } from "react";
// import { useSelector, useDispatch } from "react-redux";
import ModalText from "./ModalText";
import ProductCount from "./ProductCount";
import BtnAddOrRemove from "./BtnAddOrRemove";
import GaleryProduct from "./GaleryProduct";
// import { productsInBasket, addToBasket, removeToBasket, deleteToBasket, changePremiumInBasket } from '../../redux-store/basketSlice';




export default function Modal({ open, onClose, item }) {

    if (!open) return null;
    // const tabProducts = useSelector(productsInBasket);
    // const dispatch = useDispatch();


    return (

        <div className="fixed z-50 inset-0 overflow-y-auto">
            <div className="flex items-end justify-center min-h-screen pt-4 px-4 pb-20 text-center sm:block sm:p-0">

                <div className="fixed inset-0 transition-opacity" aria-hidden="true">
                    <div className="absolute inset-0 bg-gray-500 opacity-75"></div>
                </div>

                <span className="hidden sm:inline-block sm:align-middle sm:h-screen" aria-hidden="true">&#8203;</span>

                <div className="inline-block align-bottom bg-gray-900 rounded-lg text-left overflow-hidden shadow-xl transform transition-all sm:my-8 sm:align-middle sm:max-w-lg sm:w-full">
                    <div className="bg-gray-900 pt-3 sm:px-6 sm:flex sm:flex-row-reverse">
                        <button type="button" className="ml-3 inline-flex justify-center rounded-md border border-transparent shadow-sm px-3 py-1 bg-red-600 text-base font-medium text-white hover:bg-red-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-red-500 sm:ml-3 sm:w-auto sm:text-sm" onClick={onClose}>
                            X
                        </button>
                    </div>

                    <div className="bg-gray-900 sm:p-6 sm:pb-4">
                        <div className="flex justify-center items-center">
                            <div className="text-center sm:mt-0 sm:ml-4 sm:text-left">
                                <h3 className="text-3xl leading-6 font-medium text-rose-300 pb-3" id="modal-title">
                                    {item.name}
                                </h3>

                                <img className="mx-auto rounded-t-lg object-cover h-80" src={item.image} alt="image" />

                                <ModalText text={""} data={`${item.description}`} />

                                <hr className="border-1 border-gray-500 w-100 my-4" />

                                <ModalText text={"Taille : "} data={`H ${item.height} x L ${item.width} x long ${item.length} cm`} />
                                <ModalText text={"Poids : "} data={`${item.weight} gr`} />
                                <ModalText text={"Capacité : "} data={`${item.capacity} pièces`} />
                                <ModalText text={"Couleur : "} data={`${item.color}`} />
                                <ModalText text={"Marque : "} data={`${item.maker}`} />
                                <ModalText text={"Prix : "} data={`${item.price} €`} />

                                <div className="mt-5 flex">
                                    <BtnAddOrRemove text="-" item={item} />
                                    <ProductCount item={item} classe="text-white mx-5 my-auto text-xl"/>
                                    <BtnAddOrRemove text="+" item={item} />
                                    <BtnAddOrRemove text="supprimer" item={item} />
                                </div>

                            </div>
                        </div>
                    </div>

                    <GaleryProduct />

                    <div className="bg-gray-900 px-4 my-5 sm:px-6 sm:flex sm:flex-row-reverse">
                        <button type="button" className="ml-3 inline-flex justify-center rounded-md border border-transparent shadow-sm px-4 py-2 bg-indigo-600 text-base font-medium text-white hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500 sm:ml-3 sm:w-auto sm:text-sm" onClick={onClose}>
                            Fermer
                        </button>
                    </div>

                </div>
            </div>
        </div>
    );
}
