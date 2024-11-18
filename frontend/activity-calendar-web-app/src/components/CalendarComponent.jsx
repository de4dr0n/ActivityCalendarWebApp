import React from "react";
import Calendar from "react-calendar";
import "react-calendar/dist/Calendar.css";

const CalendarComponent = ({ selectedDate, setSelectedDate }) => {
    return (
        <div>
            <Calendar
                onChange={setSelectedDate}
                value={selectedDate}
                className="shadow-lg"
            />
        </div>
    );
};

export default CalendarComponent;
