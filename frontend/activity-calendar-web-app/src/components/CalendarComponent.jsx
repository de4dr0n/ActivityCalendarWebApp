import React from "react";
import Calendar from "react-calendar";
import "react-calendar/dist/Calendar.css";

const CalendarComponent = ({ selectedDate, setSelectedDate }) => {
    return (
        <div className="p-4 bg-white rounded-lg shadow-md">
            <h2 className="text-xl font-bold text-center mb-4">Select a Date</h2>
            <Calendar
                onChange={setSelectedDate}
                value={selectedDate}
                className="custom-calendar"
                tileClassName={({ date, view }) => {
                    if (view === 'month' && date.toDateString() === new Date().toDateString()) {
                        return 'current-day';
                    }
                }}
            />
        </div>
    );
};

export default CalendarComponent;
