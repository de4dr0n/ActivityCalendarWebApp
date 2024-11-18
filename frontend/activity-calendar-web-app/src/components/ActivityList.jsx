import React from "react";

const ActivityList = ({ activities, handleStatusChange, handleDeleteActivity }) => {
    return (
        <div>
            <h2 className="text-xl font-semibold mb-4">Activities</h2>
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
        </div>
    );
};

export default ActivityList;
