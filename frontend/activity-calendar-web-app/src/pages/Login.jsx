import React, { useState, useContext } from "react";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../context/AuthContext";

const Login = () => {
    const { login } = useContext(AuthContext);
    const [credentials, setCredentials] = useState({ username: "", password: "" });
    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            await login(credentials);
            navigate("/");
        } catch (error) {
            console.error("Login failed:", error);
            alert("Invalid credentials. Please try again.");
        }
    };

    return (
        <div className="min-h-screen flex items-center justify-center bg-gray-100">
            <div className="w-96 bg-white p-6 rounded shadow-lg">
                <h1 className="text-2xl font-bold mb-4">Login</h1>
                <form onSubmit={handleSubmit}>
                    <div className="mb-4">
                        <label className="block font-semibold mb-1">Username</label>
                        <input
                            type="text"
                            className="w-full p-2 border rounded"
                            value={credentials.username}
                            onChange={(e) => setCredentials({ ...credentials, username: e.target.value })}
                        />
                    </div>
                    <div className="mb-4">
                        <label className="block font-semibold mb-1">Password</label>
                        <input
                            type="password"
                            className="w-full p-2 border rounded"
                            value={credentials.password}
                            onChange={(e) => setCredentials({ ...credentials, password: e.target.value })}
                        />
                    </div>
                    <button
                        type="submit"
                        className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600"
                    >
                        Login
                    </button>
                </form>
            </div>
        </div>
    );
};

export default Login;
