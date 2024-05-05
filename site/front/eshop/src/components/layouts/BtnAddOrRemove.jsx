import React from 'react';
import { useSelector, useDispatch } from "react-redux";
import { productsInBasket, addToBasket, removeToBasket, deleteToBasket } from '../../redux-store/basketSlice';
import { FaTrashAlt } from 'react-icons/fa';


const BtnAddOrRemove = ({ text, item }) => {

    const tabProducts = useSelector(productsInBasket);
    const dispatch = useDispatch();

    return (
        <>
            {text == "+" &&
                <button
                    type="button"
                    className="rounded-md border border-transparent shadow-sm px-4 py-2 bg-green-600 text-base font-medium text-white hover:bg-green-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-green-500 sm:text-sm"
                    onClick={() => dispatch(addToBasket(item))}
                >
                    {text}
                </button>
            }
            {text == "-" &&
                <button
                    type="button"
                    className="rounded-md border border-transparent shadow-sm px-4 py-2 bg-red-600 text-base font-medium text-white hover:bg-red-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-red-500 sm:text-sm"
                    onClick={() => dispatch(removeToBasket(item))}
                >
                    {text}
                </button>
            }
            {text == "supprimer" &&
                <button
                    type="button"
                    className="ml-auto rounded-md border border-transparent shadow-sm px-4 py-2 bg-red-600 text-base font-medium text-white hover:bg-red-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-red-500 sm:text-sm"
                    onClick={() => dispatch(deleteToBasket(item))}
                >
                    <FaTrashAlt className="text-lg"/>
                </button>
            }
        </>
    );
};

export default BtnAddOrRemove;
