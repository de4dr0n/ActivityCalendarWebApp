import React, { useContext } from "react";
import { Link, useNavigate } from "react-router-dom";
import { AuthContext } from "../context/AuthContext";

const Header = () => {
    const { isAuthenticated, logout } = useContext(AuthContext);
    const navigate = useNavigate();

    const handleLogout = async () => {
        try {
            await logout();
            navigate("/login");
        } catch (error) {
            console.error("Logout failed:", error);
            alert("Error logging out. Please try again.");
        }
    };

    return (
        <header className="bg-gray-800 text-white p-4">
            <div className="container mx-auto flex justify-between items-center">
                <h1 className="text-xl font-bold">
                    <Link to="/">Sports Calendar</Link>
                </h1>
                <nav>
                    {isAuthenticated ? (
                        <button
                            onClick={handleLogout}
                            className="bg-red-500 px-4 py-2 rounded hover:bg-red-600"
                        >
                            Logout
                        </button>
                    ) : (
                        <div className="flex gap-4">
                            <Link to="/signup" className="bg-green-500 px-4 py-2 rounded hover:bg-green-600">
                                Sign Up
                            </Link>
                            <Link to="/login" className="bg-blue-500 px-4 py-2 rounded hover:bg-blue-600">
                                Login
                            </Link>
                        </div>
                    )}
                </nav>
            </div>
        </header>
    );
};

export default Header;
