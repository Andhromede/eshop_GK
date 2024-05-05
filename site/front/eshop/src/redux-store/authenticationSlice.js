import { createSlice } from '@reduxjs/toolkit';

const initialState = {
    isAuthenticated: false,
    client: null,
};

export const authenticationSlice = createSlice({
    name: 'auth',
    initialState,
    reducers: {
        signIn: (state, action) => {
            const client = {
                email: action.payload.email,
                role: action.payload.role,
                id: action.payload.id,
            };
            state.client = client;
            state.isAuthenticated = true;
        },
        signOut: (state) => {
            localStorage.clear();
            sessionStorage.clear();
            state.isAuthenticated = false;
            state.client = null
        },
    },
});

export const { signIn, signOut } = authenticationSlice.actions;
export const selectIsLogged = (state) => state.auth.isAuthenticated;
export const selectClient = (state) => state.auth.client;
export const selectHasRole = (state) => state.auth.role;
export default authenticationSlice.reducer;
