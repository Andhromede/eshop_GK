import { React } from "react";


export default function ModalText({ text, data }) {

    return (
        <div className="mt-2">
            <span className="text-sm text-gray-300 font-bold">
                {text} 
            </span>

            <span className="text-sm text-gray-300">
                {data}
            </span>
        </div>
    );

}
