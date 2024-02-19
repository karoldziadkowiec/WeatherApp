import React, { useState } from 'react';
import './App.css';

const Home = () => {
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
    <div className="App">
      <h1>Weather App</h1>
      <input type="text" placeholder="Enter city" value={city} onChange={handleInputChange} />
      <button onClick={getWeather}>Get Weather</button>
      {weatherData && (
        <div>
          <p>City: {weatherData.name}, {weatherData.sys.country}</p>
          <img src={`http://openweathermap.org/img/w/${weatherData.weather[0].icon}.png`} alt="Weather Icon" />
          <p>Temperature: {weatherData.main.temp}°C</p>
          <p>Weather: {weatherData.weather[0].main}</p>
          <p>Description: {weatherData.weather[0].description}</p>
          <p>Wind Speed: {weatherData.wind.speed} m/s</p>
          <p>Pressure: {weatherData.main.pressure} hPa</p>
          <p>Humidity: {weatherData.main.humidity}%</p>
          <p>Sunrise: {new Date(weatherData.sys.sunrise * 1000).toLocaleTimeString()}</p>
          <p>Sunset: {new Date(weatherData.sys.sunset * 1000).toLocaleTimeString()}</p>
        </div>
      )}
      {error && <p>{error}</p>}
    </div>
  );
}

export default Home;
