import React, { useState, useEffect } from "react";
import CalendarComponent from "./CalendarComponent";
import ActivityList from "./ActivityList";
import AddActivityForm from "./AddActivityForm";
import { fetchActivities, addActivity, deleteActivity, updateActivityStatus } from "../services/activityService";


const Dashboard = () => {
    const [selectedDate, setSelectedDate] = useState(new Date());
    const [activities, setActivities] = useState([]);
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
        try {
            await addActivity(newActivity, selectedDate);
            setNewActivity({ type: "Running", description: "", progress: 0 });
            fetchActivities(selectedDate, setActivities);
        } catch (error) {
            console.error("Error adding activity:", error);
            alert("Error adding activity. Please try again later.");
        }
    };

    const handleDeleteActivity = async (id) => {
        try {
            await deleteActivity(id);
            fetchActivities(selectedDate, setActivities);
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
            }
            await updateActivityStatus(id, updatedActivity, selectedDate, setActivities);
        } catch (error) {
            console.error("Error updating activity status:", error);
            alert("Error updating activity status. Please try again later.");
        }
    };

    return (
        <div className="container mx-auto p-4">
            <h1 className="text-3xl font-bold mb-4 text-center">Sports Calendar</h1>
            <div className="flex gap-8">
                <div className="w-1/2">
                    <CalendarComponent selectedDate={selectedDate} setSelectedDate={setSelectedDate} />
                </div>
                <div className="w-1/2">
                    <ActivityList
                        activities={activities}
                        handleStatusChange={handleStatusChange}
                        handleDeleteActivity={handleDeleteActivity}
                        selectedDate={selectedDate}
                    />
                    <AddActivityForm
                        newActivity={newActivity}
                        setNewActivity={setNewActivity}
                        handleAddActivity={handleAddActivity}
                    />
                </div>
            </div>
        </div>
    );
};

export default Dashboard;
