import { React } from "react";
import { BsXLg, BsPatchCheck } from "react-icons/bs";
// import "../../assets/styles/modal.css";
import ModalText from "./ModalText";

export default function GaleryProduct({ }) {


    return (
        <>  
            <hr className="border-1 border-gray-500 w-80 my-4 mx-auto" />

            <div className="text-white text-sm px-2 pb-5 text-center">Autres couleurs :</div>

            <div className="container mx-auto px-2">
                <div className="-m-1 flex flex-wrap md:-m-2">

                    <div className="flex w-1/4 flex-wrap">
                        <div className="w-full p-1 md:p-2">
                            <img
                                alt="gallery"
                                className="block h-full w-full rounded-lg object-cover object-center"
                                src="https://tecdn.b-cdn.net/img/Photos/Horizontal/Nature/4-col/img%20(73).webp" />
                        </div>
                    </div>

                    <div className="flex w-1/4 flex-wrap">
                        <div className="w-full p-1 md:p-2">
                            <img
                                alt="gallery"
                                className="block h-full w-full rounded-lg object-cover object-center"
                                src="https://tecdn.b-cdn.net/img/Photos/Horizontal/Nature/4-col/img%20(74).webp" />
                        </div>
                    </div>

                    <div className="flex w-1/4 flex-wrap">
                        <div className="w-full p-1 md:p-2">
                            <img
                                alt="gallery"
                                className="block h-full w-full rounded-lg object-cover object-center"
                                src="https://tecdn.b-cdn.net/img/Photos/Horizontal/Nature/4-col/img%20(75).webp" />
                        </div>
                    </div>

                    <div className="flex w-1/4 flex-wrap">
                        <div className="w-full p-1 md:p-2">
                            <img
                                alt="gallery"
                                className="block h-full w-full rounded-lg object-cover object-center"
                                src="https://tecdn.b-cdn.net/img/Photos/Horizontal/Nature/4-col/img%20(70).webp" />
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
}
