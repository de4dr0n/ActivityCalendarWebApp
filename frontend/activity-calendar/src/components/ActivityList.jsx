import React from 'react';
import { List, ListItem, ListItemText, Button } from '@mui/material';
import { deleteActivity } from '../services/activityService';

const ActivityList = ({ activities }) => {
    const handleDelete = async (id) => {
        try {
            await deleteActivity(id);
            // Optionally, refresh the activity list or notify the parent component
        } catch (error) {
            console.error("Failed to delete activity:", error);
        }
    };

    return (
        <List>
            {activities.map((activity) => (
                <ListItem key={activity.id}>
                    <ListItemText
                        primary={`${activity.type} - ${activity.description}`}
                        secondary={`Progress: ${activity.progress}%`}
                    />
                    <Button onClick={() => handleDelete(activity.id)} color="secondary">
                        Delete
                    </Button>
                </ListItem>
            ))}
        </List>
    );
};

export default ActivityList;