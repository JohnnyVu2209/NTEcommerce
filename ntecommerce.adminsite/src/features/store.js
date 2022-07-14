import { configureStore } from "@reduxjs/toolkit";
import categoryReducer from "./categorySlice";
import productReducer from "./productSlice";
import authReducer from "./AuthSlice";

export const store = configureStore({
    reducer: {
        category: categoryReducer,
        product: productReducer,
        auth: authReducer,
    }
})