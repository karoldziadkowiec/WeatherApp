import React, { useState } from 'react';
import './App.css';

const SunInfo = () => {
  const [city, setCity] = useState('');
  const [weatherData, setWeatherData] = useState(null);
  const [error, setError] = useState('');

  const handleInputChange = (event) => {
    setCity(event.target.value);
  };

  const getWeather = async () => {
    try {
      const response = await fetch(`https://localhost:7043/api/weather/${city}`);

      if (response.ok) {
        const data = await response.json();
        setWeatherData(data);
        setError('');
      } else {
        const errorMessage = await response.text();
        setError(errorMessage);
        setWeatherData(null);
      }
    } catch (error) {
      setError('Something went wrong');
      setWeatherData(null);
    }
  };

  return (
    <div className="SunInfo">
      <h1>Sun info</h1>
      <input type="text" placeholder="Enter city" value={city} onChange={handleInputChange} />
      <button onClick={getWeather}>Get info</button>
      {weatherData && (
        <div>
          <p>City: {weatherData.name}, {weatherData.sys.country}</p>
          <img src={`http://openweathermap.org/img/w/${weatherData.weather[0].icon}.png`} alt="Weather Icon" />
          <p>Temperature: {weatherData.main.temp}°C</p>
          <p>Sunrise: {new Date(weatherData.sys.sunrise * 1000).toLocaleTimeString()}</p>
          <p>Sunset: {new Date(weatherData.sys.sunset * 1000).toLocaleTimeString()}</p>
        </div>
      )}
      {error && <p>{error}</p>}
    </div>
  );
}

export default SunInfo;