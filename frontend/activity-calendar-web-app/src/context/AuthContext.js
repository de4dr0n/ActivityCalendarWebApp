import React, { createContext, useState, useEffect } from "react";
import axios from "axios";

export const AuthContext = createContext();
const api = axios.create({
    baseURL: "http://localhost:5181/api/Authorization",
});
export const AuthProvider = ({ children }) => {
    const [isAuthenticated, setIsAuthenticated] = useState(false);

    useEffect(() => {
        // Проверяем, есть ли токен в cookies для определения авторизации
        const checkAuth = () => {
            const token = document.cookie.includes("tasty-cookies");
            setIsAuthenticated(token);
        };
        checkAuth();
    }, []);

    const login = async (credentials) => {
        await api.post("/Login", credentials);
        setIsAuthenticated(true);
    };

    const register = async (credentials) => {
        await api.post("/Register", credentials);
    };

    const logout = async () => {
        await api.post("/Logout",{}, { withCredentials: true });
        setIsAuthenticated(false);
    };

    return (
        <AuthContext.Provider value={{ isAuthenticated, login, register, logout }}>
            {children}
        </AuthContext.Provider>
    );
};
