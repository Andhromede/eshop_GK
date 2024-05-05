import React from "react";
import { Route, Routes as RoutesContainer } from "react-router-dom";
import * as URL from "../constants/urls/urlFrontEnd";

import HomeView from "../views/HomeView";
import NotFoundView from "../views/NotFoundView";
import ProductsView from "../views/ProductsView";
import LoginView from "../views/LoginView";
import RegisterView from "../views/RegisterView";
import PanierView from "../views/PanierView";




const MyRoutes = () => {
    return (
        <>
            <RoutesContainer>
                <Route path='*' element={<NotFoundView />} />
                <Route index element={<HomeView />} />
                <Route path={URL.HOME} element={<HomeView />} />
                <Route path={URL.PRODUCTS} element={<ProductsView />} />
                <Route path={URL.LOGIN} element={<LoginView />} />
                <Route path={URL.REGISTER} element={<RegisterView />} />
                <Route path={URL.PANIER} element={<PanierView />} />
            </RoutesContainer>
        </>
    );
};

export default MyRoutes;