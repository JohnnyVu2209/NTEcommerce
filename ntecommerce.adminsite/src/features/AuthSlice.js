import authService from "../services/AuthService";

const { createSlice, createAsyncThunk } = require("@reduxjs/toolkit");

export const login = createAsyncThunk("auth/login", async ({ uName, password }, { rejectWithValue }) => {
    try {
        const response = await authService.login(uName, password);

        const { token, username, expiration } = response.data;

        localStorage.setItem("tokenInfo", JSON.stringify({ token, expiration }));

        const user = await authService.getUser(username, token);

        const { id, fullName } = user.data;

        return { token, expiration, id, fullName, username };

    } catch (error) {
        if (!error.response) {
            throw error
        }
        return rejectWithValue(error.response.data.Message);
    }
})

const authSlice = createSlice({
    name: "auth",
    initialState: {
        isAuth: !!JSON.parse(localStorage.getItem("tokenInfo")),
        user: {}
    },
    reducers: {
        logout: (state) => {
            localStorage.removeItem("tokenInfo");
            state.user = {}
            state.isAuth = !!JSON.parse(localStorage.getItem("tokenInfo"));
        }
    },
    extraReducers: {
        [login.fulfilled]: (state, { payload }) =>
        ({
            ...state,
            isAuth: true,
            user: {
                ...state.user,
                id: payload.id,
                username: payload.username,
                fullName: payload.fullName
            }
        })
    }
})

const { reducer, actions } = authSlice;
export const { logout } = actions;
export default reducer;