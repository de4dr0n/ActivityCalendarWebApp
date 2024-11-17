import React from 'react';
import Calendar from 'react-calendar';
import 'react-calendar/dist/Calendar.css';

const ActivityCalendar = ({ onDateChange }) => {
    const handleDateChange = (date) => {
        onDateChange(date);
    };

    return (
        <div>
            <Calendar onChange={handleDateChange} />
        </div>
    );
};

export default ActivityCalendar;