import { products } from "../api/backend/products";
import { React, useState, useEffect } from "react";
import ProductCard from "../components/layouts/ProductCard";
import { PRODUCTS } from "../constants/urls/urlBackEnd";



const ProductsView = () => {
    const [tabProducts, setTabProducts] = useState([]);

    useEffect(() => {
        products()
            .then((res) => {
                if (res.status === 200) {
                    setTabProducts(res.data);
                }
            })
            .catch((err) => console.log(err));
    }, []);


    return (
        <div className=" flex flex-row grid grid-cols-12">
            {tabProducts.map((product) => {
                console.log(product);
                return <ProductCard item={product} key={product._id} />;
            })}
        </div>
    );
};

export default ProductsView;
