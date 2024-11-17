import React, { useState } from 'react';
import ActivityCalendar from './components/ActivityCalendar';
import ActivityDashboard from './components/ActivityDashboard';

const App = () => {
  const [selectedDate, setSelectedDate] = useState(new Date());

  const handleDateChange = (date) => {
    setSelectedDate(date);
  };

  return (
      <div>
        <h1>Activity Calendar</h1>
        <ActivityCalendar onDateChange={handleDateChange} />
        <ActivityDashboard selectedDate={selectedDate} />
      </div>
  );
};

export default App;