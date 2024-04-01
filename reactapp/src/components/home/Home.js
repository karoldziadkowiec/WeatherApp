import React, { useState } from 'react';
import { Modal, Button } from 'react-bootstrap';
import ServerURL from '../../config/ServerURL';
import '../../App.css';

const Home = () => {
  const [city, setCity] = useState('');
  const [weatherData, setWeatherData] = useState(null);
  const [error, setError] = useState('');
  const [showModal, setShowModal] = useState(false);

  const handleInputChange = (event) => {
    setCity(event.target.value);
  };

  const getWeather = async () => {
    try {
      const response = await fetch(`${ServerURL}/weather/${city}`);

      if (response.ok) {
        const data = await response.json();
        setWeatherData(data);
        setShowModal(true);
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

  const handleClose = () => setShowModal(false);

  return (
    <div className="App">
      <div className="centered-content">
      <h1>WeatherApp</h1>
      <h5>Check the current weather below!</h5>
      <input type="text" placeholder="Search city" value={city} onChange={handleInputChange} />
      <p><Button variant="info" onClick={getWeather}>Search</Button></p>
      </div>

      <Modal show={showModal} onHide={handleClose} centered>
        <Modal.Header closeButton>
          <Modal.Title>{weatherData && weatherData.name}, {weatherData && weatherData.sys && weatherData.sys.country}</Modal.Title>
        </Modal.Header>
        <Modal.Body className="text-center">
          {weatherData && (
            <div>
              <img src={`http://openweathermap.org/img/w/${weatherData.weather[0].icon}.png`} alt="Weather Icon" />
              <b>{weatherData.main.temp}°C</b>
              <p>Weather: <b>{weatherData.weather[0].main} - {weatherData.weather[0].description}</b></p>
              <p>Wind Speed: <b>{weatherData.wind.speed} m/s</b></p>
              <p>Pressure: <b>{weatherData.main.pressure} hPa</b></p>
              <p>Humidity: <b>{weatherData.main.humidity}%</b></p>
            </div>
          )}
          {error && <p>{error}</p>}
        </Modal.Body>
        <Modal.Footer className="justify-content-center">
          <Button variant="dark" onClick={handleClose}>Close</Button>
        </Modal.Footer>
      </Modal>
    </div>
  );
}

export default Home;