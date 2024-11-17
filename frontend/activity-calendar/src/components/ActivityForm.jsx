import React, { useState } from 'react';
import { TextField, Button, MenuItem, Snackbar } from '@mui/material';
import { createActivity } from '../services/activityService';

const ActivityForm = ({ date, onActivityAdded }) => {
    const [activity, setActivity] = useState({
        date: date.toISOString().split('T')[0],
        type: '',
        description: '',
        progress: 0
    });
    const [error, setError] = useState(null);
    const [success, setSuccess] = useState(false);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setActivity({ ...activity, [name]: value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            await createActivity(activity);
            setSuccess(true);
            onActivityAdded(); // Notify parent to refresh activity list
            setActivity({
                date: date.toISOString().split('T')[0],
                type: '',
                description: '',
                progress: 0
            }); // Reset form
        } catch (err) {
            setError("Failed to add activity. Please try again.");
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <TextField
                select
                label="Activity Type"
                name="type"
                value={activity.type}
                onChange={handleChange}
                fullWidth
                required
            >
                <MenuItem value="Running">Running</MenuItem>
                <MenuItem value="Cycling">Cycling</MenuItem>
                <MenuItem value="Walking">Walking</MenuItem>
                <MenuItem value="Yoga">Yoga</MenuItem>
                <MenuItem value="Strength">Strength </MenuItem>
            </TextField>
            <TextField
                label="Description"
                name="description"
                value={activity.description}
                onChange={handleChange}
                fullWidth
                required
            />
            <TextField
                label="Progress (%)"
                name="progress"
                type="number"
                value={activity.progress}
                onChange={handleChange}
                fullWidth
                required
                inputProps={{ min: 0, max: 100 }}
            />
            <Button type="submit" variant="contained" color="primary">
                Add Activity
            </Button>
            <Snackbar
                open={success}
                autoHideDuration={6000}
                onClose={() => setSuccess(false)}
                message="Activity added successfully!"
            />
            {error && <p>{error}</p>}
        </form>
    );
};

export default ActivityForm;