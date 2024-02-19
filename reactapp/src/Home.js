import React, { useState } from 'react';
import './App.css';

const Home = () => {
  const [city, setCity] = useState('');
  const [temperature, setTemperature] = useState('');
  const [error, setError] = useState('');

  const handleInputChange = (event) => {
    setCity(event.target.value);
  };

  const getWeather = async () => {
    try {
      const response = await fetch(`https://localhost:7043/api/weather/${city}`);

      if (response.ok) {
        const data = await response.json();
        setTemperature(data.main.temp);
        setError('');
      } else {
        const errorMessage = await response.text();
        setError(errorMessage);
        setTemperature('');
      }
    } catch (error) {
      setError('Something went wrong');
      setTemperature('');
    }
  };

  return (
    <div className="App">
      <h1>Weather App</h1>
      <input type="text" placeholder="Enter city" value={city} onChange={handleInputChange} />
      <button onClick={getWeather}>Get Weather</button>
      {temperature && <p>Temperature: {temperature}°C</p>}
      {error && <p>{error}</p>}
    </div>
  );
}

export default Home;