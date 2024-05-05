import { React, useState } from "react";
import Modal from "./Modal";



const ProductCard = ({ item }) => {

    const [openModal, setOpenModal] = useState(false);

    const handleClick = () => {
        setOpenModal(true);
      };


    return (
        <>
            <div className="m-10 lg:col-span-3 md:col-span-4 sm:col-span-6 mt-8 mr-8 max-w-sm bg-white border border-gray-200 rounded-lg shadow dark:bg-gray-800 dark:border-gray-700">
                <a href="#" className="">
                    <img className="rounded-t-lg object-contain h-80 w-80 bg-white" src={item.image}  alt="image" />
                </a>

                <div className="p-5">
                    <a href="#">
                        <h5 className="mb-2 text-2xl font-bold tracking-tight text-gray-900 dark:text-white">{item.name}</h5>
                    </a>

                    <p className="mb-3 font-normal text-gray-700 dark:text-gray-400">{item.description}</p>

                    <button onClick={handleClick} className="inline-flex items-center px-3 py-2 text-sm font-medium text-center text-white rounded-md border border-transparent shadow-sm px-4 py-2 bg-indigo-600 text-base font-medium text-white hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500 sm:w-auto sm:text-sm">
                        Détails

                        <svg className="w-3.5 h-3.5 ml-2" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 14 10">
                            <path stroke="currentColor" strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M1 5h12m0 0L9 1m4 4L9 9" />
                        </svg>
                    </button>

                    <Modal
                        open={openModal}
                        onClose={() => setOpenModal(false)}
                        item={item}
                    />
                </div>
            </div>
        </>
    );
};

export default ProductCard;













