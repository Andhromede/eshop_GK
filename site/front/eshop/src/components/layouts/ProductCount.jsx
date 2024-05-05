import { useSelector } from "react-redux";
import { productsInBasket } from '../../redux-store/basketSlice';
import { React, useState, useEffect } from "react";


const ProductCount = ({ item, classe }) => {
    const tabProducts = useSelector(productsInBasket);

    return (
        <div className={classe}>
            {(tabProducts.find(objects => objects.id === item.id)) &&
                <span>{tabProducts.find(objects => objects.id === item.id).quantity}</span>
            }

            {(!tabProducts.find(objects => objects.id === item.id)) &&
                <span>0</span>
            }
        </div>
    );
};

export default ProductCount;
