import React, { useState, useEffect } from "react";
import Calendar from "react-calendar";
import "react-calendar/dist/Calendar.css";
import axios from "axios";

const api = axios.create({
    baseURL: "http://localhost:5181/api",
})
const Dashboard = () => {
    const [selectedDate, setSelectedDate] = useState(new Date());
    const [activities, setActivities] = useState([]);
    const [newActivity, setNewActivity] = useState({
        type: "Running",
        description: "",
        progress: 0,
    });

    useEffect(() => {
        fetchActivities(selectedDate);
    }, [selectedDate]);

    const fetchActivities = async (date) => {
        try {
            const response = await api.get(`/activities/${date.toISOString()}`);
            setActivities(response.data);
        } catch (error) {
            console.error("Error fetching activities:", error);
            alert("Error fetching activities. Try again later");
        }
    };

    const handleAddActivity = async (e) => {
        e.preventDefault();
        try {
            const activityToAdd = {
                ...newActivity,
                date: selectedDate,
            };
            await api.post("/activities", activityToAdd);
            setNewActivity({ type: "Running", description: "", progress: 0 });
            fetchActivities(selectedDate);
        } catch (error) {
            console.error("Error adding activity:", error);
            alert("Error adding activity. Please try again later.");
        }
    };

    const handleDeleteActivity = async (id) => {
        try {
            await api.delete(`/activities/${id}`);
            fetchActivities(selectedDate);
        } catch (error) {
            console.error("Error deleting activity:", error);
            alert("Error deleting activity. Please try again later.");
        }
    };

    const handleStatusChange = async (id, status) => {
        try {
            const activity = activities.find((a) => a.id === id);

            const updatedStatus =
                status === "Planned"
                    ? "InProgress"
                    : status === "InProgress"
                        ? "Completed"
                        : "Planned";

            const updatedActivity = {
                ...activity,
                status: updatedStatus,
            };
            await api.put(`/activities/${id}`, updatedActivity);
            fetchActivities(selectedDate);
        } catch (error) {
            console.error("Error updating activity status:", error);
            alert("Error updating activity status. Please try again later.");
        }
    };


    return (
        <div className="container mx-auto p-4">
            <h1 className="text-3xl font-bold mb-4 text-center">Sports Calendar</h1>

            <div className="flex gap-8">
                {/* Calendar */}
                <div className="w-1/2">
                    <Calendar
                        onChange={setSelectedDate}
                        value={selectedDate}
                        className="shadow-lg"
                    />
                </div>

                {/* Dashboard */}
                <div className="w-1/2">
                    <h2 className="text-xl font-semibold mb-4">Activities on {selectedDate.toDateString()}</h2>
                    <ul>
                        {activities.map((activity) => (
                            <li key={activity.id} className="border-b py-2 flex justify-between items-center">
                                <div>
                                    <p><strong>Type:</strong> {activity.type}</p>
                                    <p><strong>Description:</strong> {activity.description || "No description"}</p>
                                    <p><strong>Progress:</strong> {activity.progress}</p>
                                </div>
                                <div className="flex gap-2">
                                    <button
                                        className="bg-blue-500 text-white px-3 py-1 rounded hover:bg-blue-600"
                                        onClick={() => handleStatusChange(activity.id, activity.status)}
                                    >
                                        {activity.status}
                                    </button>
                                    <button
                                        className="bg-red-500 text-white px-3 py-1 rounded hover:bg-red-600"
                                        onClick={() => handleDeleteActivity(activity.id)}
                                    >
                                        Delete
                                    </button>
                                </div>
                            </li>
                        ))}
                    </ul>

                    {/* Add Activity Form */}
                    <form onSubmit={handleAddActivity} className="mt-4">
                        <h3 className="text-lg font-semibold mb-2">Add New Activity</h3>
                        <div className="mb-2">
                            <label className="block font-semibold mb-1">Type</label>
                            <select
                                className="w-full p-2 border rounded"
                                value={newActivity.type}
                                onChange={(e) => setNewActivity({ ...newActivity, type: e.target.value })}
                            >
                                <option value="Running">Running</option>
                                <option value="Cycling">Cycling</option>
                                <option value="Walking">Walking</option>
                                <option value="Yoga">Yoga</option>
                                <option value="Strength">Strength</option>
                            </select>
                        </div>
                        <div className="mb-2">
                            <label className="block font-semibold mb-1">Description</label>
                            <input
                                type="text"
                                className="w-full p-2 border rounded"
                                value={newActivity.description}
                                onChange={(e) => setNewActivity({ ...newActivity, description: e.target.value })}
                            />
                        </div>
                        <div className="mb-2">
                            <label className="block font-semibold mb-1">Progress</label>
                            <input
                                type="number"
                                className="w-full p-2 border rounded"
                                value={newActivity.progress}
                                onChange={(e) => setNewActivity({ ...newActivity, progress: e.target.value })}
                            />
                        </div>
                        <button
                            type="submit"
                            className="bg-green-500 text-white px-4 py-2 rounded hover:bg-green-600"
                        >
                            Add Activity
                        </button>
                    </form>
                </div>
            </div>
        </div>
    );
};

export default Dashboard;
