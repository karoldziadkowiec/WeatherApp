import React, { useState, useEffect } from 'react';

const Clock = () => {
  const [currentDateTime, setCurrentDateTime] = useState('');

  useEffect(() => {
    const timerID = setInterval(() => tick(), 1000);

    return function cleanup() {
      clearInterval(timerID);
    };
  });

  const tick = () => {
    const now = new Date();
    const options = { weekday: 'long', year: 'numeric', month: 'numeric', day: 'numeric' };
    setCurrentDateTime(now.toLocaleDateString('en-US', options) + ' - ' + now.toLocaleTimeString());
  };

  return (
    <span className="text-light">{currentDateTime}</span>
  );
}

export default Clock;