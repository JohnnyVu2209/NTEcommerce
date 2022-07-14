import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import productService from "../services/ProductService";

export const createProduct = createAsyncThunk("product/create", async (data, { rejectWithValue }) => {
    try {
        const response = await productService.createProduct(data);
        return response.data;
    } catch (error) {
        if (!error.response) {
            throw error
        }
        return rejectWithValue(error.response.data.Message);
    }
});

export const getProduct = createAsyncThunk("product/detail", async (id, { rejectWithValue }) => {
    try {
        const response = await productService.getProduct(id);
        return response.data;
    } catch (error) {
        if (!error.response) {
            throw error
        }
        return rejectWithValue(error.response.data.Message);
    }
})

export const updateProduct = createAsyncThunk("product/update",async({id,data}, {rejectWithValue }) => {
    try{
        const response = await productService.updateProduct(id,data);
        return response.data;
    }catch (error) {
        if (!error.response) {
            throw error
        }
        return rejectWithValue(error.response.data.Message);
    }
})

export const deleteProduct = createAsyncThunk("product/delete",async (id,{rejectWithValue}) => {
    try {
        await productService.deleteProduct(id);
        return id;
    } catch (error) {
        if (!error.response) {
            throw error
        }
        return rejectWithValue(error.response.data.Message);
    }
})

const initialState = { product: {} }

const productSlice = createSlice({
    name: "product",
    initialState,
    reducers: {
        removeProduct: (state) => {
            state.product = {}
        }
    },
    extraReducers: {
        [getProduct.fulfilled]: (state, { payload }) => ({ ...state, product: payload })
    }
});

const { reducer, actions } = productSlice;
export const { removeProduct } = actions
export default reducer;