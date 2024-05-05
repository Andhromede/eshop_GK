import { React, useState, useEffect } from "react";
import BasketCard from "../components/layouts/BasketCard";
import { PRODUCTS } from "../constants/urls/urlFrontEnd";
import { Link } from "react-router-dom";
// import { ToastContainer } from "react-toastify";
import { BsCartXFill } from "react-icons/bs"
import { GoArrowRight, GoArrowLeft } from "react-icons/go"
import { basketTotalPrice } from '../redux-store/basketSlice';
import { useSelector, useDispatch } from 'react-redux';
import { productsInBasket, calculTotalPrice } from '../redux-store/basketSlice';

const PanierView = () => {
    const tabProducts = useSelector(productsInBasket);
    const dispatch = useDispatch();
    const totalPrice = useSelector(basketTotalPrice);

    useEffect(() => {
        dispatch(calculTotalPrice());
    }, [dispatch, tabProducts]);



    return (
        <section className="text-gray-700 body-font">
            <div className="container px-5 py-24 mx-auto">
                <h1 className="text-2xl lg:text-4xl mb-10 title-jedi text-primary text-center">
                    Mon panier
                </h1>

                <div className="flex flex-wrap m-4 grid grid-cols-12">
                    {tabProducts.map((data) => {
                        return <BasketCard item={data} key={data.id} />;
                    })}
                </div>

                <div className="text-2xl mb-10 text-center bg-gray-800 opacity-80 py-5 mt-10">
                    <span className="text-white">Prix du panier :</span>
                    <span className="text-white"> {totalPrice.toLocaleString()} €</span>
                </div>
                


                {tabProducts.length > 0 ? (
                    <div className="flex justify-center">
                        <Link to="">
                            <button className="btn bg-rose-400 hover:bg-rose-300 w-40 py-2 px-4 rounded font-bold text-lg text-black">
                                Paiement
                            </button>
                        </Link>
                    </div>
                ) : (
                    <div>
                        <BsCartXFill className="mx-auto text-rose-400 text-8xl md:text-9xl" />

                        <Link className="font-Varino" to={PRODUCTS}>
                            <div className="mt-20 text-sm md:text-2xl text-center flex justify-center text-terciary hover:text-terciary-light"><GoArrowRight className="mx-6 text-terciary-light text-3xl" /> Venez découvrir nos produits <GoArrowLeft className="mx-6 text-terciary-light text-3xl" /></div>
                        </Link>
                    </div>
                )}
            </div>
            {/* <ToastContainer theme="dark" /> */}
        </section>
    );
};

export default PanierView;
