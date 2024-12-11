import React, { useState, useEffect } from "react";
import CalendarComponent from "./CalendarComponent";
import ActivityList from "./ActivityList";
import AddActivityForm from "./AddActivityForm";
import { fetchActivities, addActivity, deleteActivity, updateActivityStatus } from "../services/activityService";

const Dashboard = () => {
    const [selectedDate, setSelectedDate] = useState(new Date());
    const [activities, setActivities] = useState([]);
    const [showAddActivityForm, setShowAddActivityForm] = useState(false);
    const [newActivity, setNewActivity] = useState({
        type: "Running",
        description: "",
        progress: 0,
    });

    useEffect(() => {
        fetchActivities(selectedDate, setActivities);
    }, [selectedDate]);

    const handleAddActivity = async (e) => {
        e.preventDefault();
        const dateTime = new Date(selectedDate);
        const [hours, minutes] = newActivity.time.split(":");
        dateTime.setHours(hours);
        dateTime.setMinutes(minutes);

        const utcDateTime = new Date(dateTime.getTime());

        try {
            await addActivity(newActivity, utcDateTime);
            setNewActivity({ type: "Running", description: "", progress: 0, time: "" });
            setShowAddActivityForm(false);
            await fetchActivities(selectedDate, setActivities);
        } catch (error) {
            console.error("Error adding activity:", error);
            alert("Error adding activity. Please try again later.");
        }
    };

    const handleDeleteActivity = async (id) => {
        try {
            await deleteActivity(id);
            await fetchActivities(selectedDate, setActivities);
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
            await updateActivityStatus(id, updatedActivity, selectedDate, setActivities);
        } catch (error) {
            console.error("Error updating activity status:", error);
            alert("Error updating activity status. Please try again later.");
        }
    };

    return (
        <div className="container mx-auto p-4">
                <div className="flex justify-center items-center h-full">
                    <CalendarComponent selectedDate={selectedDate} setSelectedDate={setSelectedDate}/>
                </div>
                <div className="m-14 items-center justify-center h-full">
                    <ActivityList
                        activities={activities}
                        handleStatusChange={handleStatusChange}
                        handleDeleteActivity={handleDeleteActivity}
                        selectedDate={selectedDate}
                    />
                    <div className="mt-4">
                        <button
                            className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600"
                            onClick={() => setShowAddActivityForm(true)}
                        >
                            Add Activity
                        </button>
                    </div>
                </div>

            {showAddActivityForm && (
                <div className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center z-50">
                    <div className="bg-white p-6 rounded shadow-lg w-96 relative">
                        <button
                            className="absolute top-2 right-2 text-gray-500 hover:text-gray-700"
                            onClick={() => setShowAddActivityForm(false)}
                        >
                            &times;
                        </button>
                        <AddActivityForm
                            newActivity={newActivity}
                            setNewActivity={setNewActivity}
                            handleAddActivity={handleAddActivity}
                        />
                    </div>
                </div>
            )}
        </div>
    );
};

export default Dashboard;
