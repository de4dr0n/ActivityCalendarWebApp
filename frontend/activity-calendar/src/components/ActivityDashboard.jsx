import React, { useEffect, useState } from 'react';
import { getActivitiesByDate } from '../services/activityService';
import ActivityForm from './ActivityForm';
import ActivityList from './ActivityList';

const ActivityDashboard = ({ selectedDate }) => {
    const [activities, setActivities] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    const loadActivities = async () => {
        setLoading(true);
        setError(null);
        try {
            const data = await getActivitiesByDate(selectedDate);
            setActivities(data);
        } catch (err) {
            setError("Failed to fetch activities.");
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        loadActivities();
    }, [selectedDate]);

    const handleActivityAdded = () => {
        loadActivities(); // Refresh activities after adding a new one
    };

    return (
        <div>
            <ActivityForm date={selectedDate} onActivityAdded={handleActivityAdded} />
            {loading && <p>Loading activities...</p>}
            {error && <p>{error}</p>}
            <ActivityList activities={activities} />
        </div>
    );
};

export default ActivityDashboard;