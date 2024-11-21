import React, { createContext, useState, useEffect } from "react";
import api from '../api';

export const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
    const [isAuthenticated, setIsAuthenticated] = useState(false);

    useEffect(() => {
        const checkAuth = () => {
            const token = document.cookie.includes("accessToken");
            setIsAuthenticated(token);
        };
        checkAuth();
    }, []);

    const login = async (credentials) => {
        await api.post("/Authorization/Login", credentials);
        setIsAuthenticated(true);
    };

    const register = async (credentials) => {
        try {
            await api.post("/Authorization/Register", credentials);
        } catch (error) {
            if (error.response && error.response.status === 409) {
                throw new Error("Username is already taken");
            }
            throw new Error("Registration failed. Please try again later.");
        }
    };

    const logout = async () => {
        await api.post("/Authorization/Logout", {}, { withCredentials: true });
        document.cookie = 'accessToken=; expires=Thu, 01 Jan 1970 00:00:00 GMT; path=/';
        document.cookie = 'refreshToken=; expires=Thu, 01 Jan 1970 00:00:00 GMT; path=/';
        setIsAuthenticated(false);
    };

    return (
        <AuthContext.Provider value={{ isAuthenticated, login, register, logout }}>
            {children}
        </AuthContext.Provider>
    );
};
