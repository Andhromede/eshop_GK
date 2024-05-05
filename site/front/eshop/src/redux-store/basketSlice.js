import { createSlice } from "@reduxjs/toolkit";


const initialState = {
   products: [],
   totalPrice: 0,
};

const minQte = 1;


export const basketSlice = createSlice({
   name: "basket",
   initialState: initialState,

   reducers: {

      /******** Add or increase in the basket ********/
      addToBasket: (state, action) => {
         let productInBasket = state.products.find((item) => item.id === action.payload.id);

         if (productInBasket) {
            state.products = state.products.map(product =>
               product.id === action.payload.id
                  ? {
                     ...product,
                     quantity: product.quantity + 1,
                     totalPrice: (product.quantity + 1) * product.price,
                  }
                  : product
            );
         } else {
            state.products = [...state.products, { ...action.payload, quantity: minQte, totalPrice: action.payload.price }];
         }
      },

      /******** Decrease in the basket (Nath) ********/
      removeToBasket: (state, action) => {
         const productInBasket = state.products.find((item) => item.id === action.payload.id);

         if (productInBasket && productInBasket.quantity >= 1) {
            productInBasket.quantity--;
            productInBasket.totalPrice = productInBasket.quantity * productInBasket.price;
         }
      },

      /******** Remove to the basket (Nath) ********/
      deleteToBasket: (state, action) => {
         const productInBasket = state.products.find((item) => item.id === action.payload.id);

         if (productInBasket) {
            state.products = state.products.filter((item) => item.id !== action.payload.id);
         }
      },

      /******** Delete All to the basket  ********/
      deleteAllToBasket: (state, action) => {
         const productInBasket = state.products.find((item) => item.id === action.payload.id);
         if (productInBasket) {
            state.products = state.products.filter((item) => item.id !== action.payload.id);
         }
      },

      /******** clear the basket (Nath) ********/
      clearBasket: (state) => {
         state.products = [];
      },

      /******** Calcul total price ********/
      calculTotalPrice: (state) => {
         let total = 0;
      
         state.products.forEach((item) => {
            if (typeof item.totalPrice === 'number') {
               total += item.totalPrice;
            } 
            // else {
            //    console.error('Invalid totalPrice:', item.totalPrice, 'for item:', item);
            // }
         });
         state.totalPrice = total;
      }

   }
});


export const { addToBasket, removeToBasket, addProductInBasket, deleteToBasket, changePremiumInBasket, deleteAllToBasket, clearBasket, calculTotalPrice } = basketSlice.actions;
export const basketTotalPrice = (state) => state.basket.totalPrice;
export const productsInBasket = (state) => state.basket.products;
export default basketSlice.reducer;
