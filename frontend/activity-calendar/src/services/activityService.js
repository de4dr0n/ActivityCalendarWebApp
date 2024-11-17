import axios from 'axios';

const API_URL = 'http://localhost:5181/api/Activities';

export const getActivitiesByDate = async (date) => {
    try {
        const response = await axios.get(`${API_URL}/${date.toISOString().split('T')[0]}`);
        return response.data;
    } catch (error) {
        console.error("Error fetching activities:", error);
        throw error;
    }
};

export const createActivity = async (activity) => {
    try {
        const response = await axios.post(API_URL, activity);
        return response.data;
    } catch (error) {
        console.error("Error creating activity:", error);
        throw error;
    }
};

export const updateActivity = async (id, activity) => {
    try {
        const response = await axios.put(`${API_URL}/${id}`, activity);
        return response.data;
    } catch (error) {
        console.error("Error updating activity:", error);
        throw error;
    }
};

export const deleteActivity = async (id) => {
    try {
        await axios.delete(`${API_URL}/${id}`);
    } catch (error) {
        console.error("Error deleting activity:", error);
        throw error;
    }
};