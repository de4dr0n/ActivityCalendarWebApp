import axios from "axios";

const api = axios.create({
    baseURL: "http://localhost:5181/api",
})

export const fetchActivities = async (date, setActivities) => {
    try {
        const response = await api.get(`/activities/${date.toISOString()}`);
        setActivities(response.data);
    } catch (error) {
        console.error("Error fetching activities:", error);
        alert("Error fetching activities. Try again later");
    }
};

export const addActivity = async (activity, date) => {
    try {
        const activityToAdd = {
            ...activity,
            date: date,
        };
        await api.post("/activities", activityToAdd);
    } catch (error) {
        console.error("Error adding activity:", error);
        alert("Error adding activity. Please try again later.");
    }
};

export const deleteActivity = async (id) => {
    try {
        await api.delete(`/activities/${id}`);
    } catch (error) {
        console.error("Error deleting activity:", error);
        alert("Error deleting activity. Please try again later.");
    }
};

export const updateActivityStatus = async (id, updatedActivity, selectedDate, setActivities) => {
    try {
        await api.put(`/activities/${id}`, updatedActivity);
        fetchActivities(selectedDate, setActivities);
    } catch (error) {
        console.error("Error updating activity status:", error);
        alert("Error updating activity status. Please try again later.");
    }
};
