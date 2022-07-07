import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import categoryService from "../services/CategoryService";

export const createCategory = createAsyncThunk("category/create", async (data, { rejectWithValue }) => {
    try {
        const response = await categoryService.createCategory(data);
        return response.data;
    } catch (error) {
        if (!error.response) {
            throw error
        }
        return rejectWithValue(error.response.data.Message);
    }
});

export const getCategory = createAsyncThunk("category/detail", async (id, { rejectWithValue }) => {
    try {
        const response = await categoryService.getCategoryDetail(id);
        return response.data;
    } catch (error) {
        if (!error.response) {
            throw error
        }
        return rejectWithValue(error.response.data.Message);
    }
})

const initialState = { category: {} };

const categorySlice = createSlice({
    name: "category",
    initialState,
    reducers: {
        removeCategory: (state) => {
            state.category = {}
        }
    },
    extraReducers: {
        [getCategory.fulfilled]: (state, { payload }) => ({ ...state, category: payload })
    }
});

const { reducer, actions } = categorySlice;
export const { removeCategory } = actions;
export default reducer;