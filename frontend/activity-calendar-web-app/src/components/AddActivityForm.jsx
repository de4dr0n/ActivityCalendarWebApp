import React from "react";

const AddActivityForm = ({ newActivity, setNewActivity, handleAddActivity }) => {
    return (
        <form onSubmit={handleAddActivity} className="mt-4">
            <h3 className="text-lg font-semibold mb-2">Add New Activity</h3>
            <div className="mb-2">
                <label className="block font-semibold mb-1">Type</label>
                <select
                    className="w-full p-2 border rounded"
                    value={newActivity.type}
                    onChange={(e) => setNewActivity({...newActivity, type: e.target.value})}
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
                    onChange={(e) => setNewActivity({...newActivity, description: e.target.value})}
                />
            </div>
            <div className="mb-2">
                <label className="block font-semibold mb-1">Progress</label>
                <input
                    type="number"
                    className="w-full p-2 border rounded"
                    value={newActivity.progress}
                    onChange={(e) => setNewActivity({...newActivity, progress: e.target.value})}
                />
            </div>
            <div className="mb-2">
                <label className="block font-semibold mb-1">Time</label>
                <input
                    type="time"
                    className="w-full p-2 border rounded"
                    onChange={(e) => setNewActivity({...newActivity, time: e.target.value})}
                />
            </div>
            <button
                type="submit"
                className="bg-green-500 text-white px-4 py-2 rounded hover:bg-green-600"
            >
                Add Activity
            </button>
        </form>
    );
};

export default AddActivityForm;
