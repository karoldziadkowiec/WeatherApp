import React, { useState, useEffect } from 'react';
import { Table, Modal, Button} from 'react-bootstrap';
import './Saved.css';

const Saved = () => {
  const [savedData, setSavedData] = useState([]);
  const [selectedSavedItem, setSelectedSavedItem] = useState(null);
  const [showModal, setShowModal] = useState(false);

  useEffect(() => {
    fetchSavedData();
  }, []);

  const fetchSavedData = async () => {
    try {
      const response = await fetch('https://localhost:7043/api/saved');
      if (response.ok) {
        const data = await response.json();
        const fetchedData = await Promise.all(data.map(async (item) => {
          const weatherResponse = await fetch(`https://localhost:7043/api/history/${item.weatherId}`);
          if (weatherResponse.ok) {
            const weatherData = await weatherResponse.json();
            return {
              id: item.id,
              city: weatherData.city,
              country: weatherData.country,
              title: weatherData.title,
              description: weatherData.description,
              temperature: weatherData.temperature,
              icon: weatherData.icon,
              created: weatherData.created,
              pressure: weatherData.pressure,
              humidity: weatherData.humidity,
              windSpeed: weatherData.windSpeed,
              sunrise: weatherData.sunrise,
              sunset: weatherData.sunset,
            };
          } else {
            console.error(`Failed to fetch weather data for saved item with id ${item.id}`);
            return null;
          }
        }));
        setSavedData(fetchedData.filter(item => item !== null));
      } else {
        console.error('Failed to fetch saved data');
      }
    } catch (error) {
      console.error('Error fetching saved data:', error);
    }
  };

  const formatDate = (dateString) => {
    const date = new Date(dateString);
    return date.toLocaleDateString('en-US') + ' ' + date.toLocaleTimeString('en-US');
  };

  const handleDetailsClick = (savedItem) => {
    setSelectedSavedItem(savedItem);
    setShowModal(true);
  };

  const handleCloseModal = () => {
    setShowModal(false);
    setSelectedSavedItem(null);
  };

  const handlePrintClick = async () => {
    try {
      const response = await fetch('https://localhost:7043/api/saved/csv');
      if (response.ok) {
        const blob = await response.blob();
        const url = window.URL.createObjectURL(new Blob([blob]));
        const a = document.createElement('a');
        a.href = url;
        a.download = 'saved.xlsx';
        document.body.appendChild(a);
        a.click();
        a.remove();
      } else {
        console.error('Failed to fetch saved data');
      }
    } catch (error) {
      console.error('Error fetching saved data:', error);
    }
  };

  const handleRemoveButtonClick = async (savedItemId) => {
    try {
      const response = await fetch(`https://localhost:7043/api/saved/${savedItemId}`, {
        method: 'DELETE',
      });

      if (response.ok) {
        console.log('Weather removed successfully');
        fetchSavedData();
      } else {
        console.error('Failed to remove weather');
      }
    } catch (error) {
      console.error('Error removing weather:', error);
    }
  };

  return (
    <div className="Saved">
      <div className="centered-saved-content">
        <h1>WeatherApp</h1>
        <h4>Saved Weather</h4>
        <h5>Check out saved weather details.</h5>
        <Button variant="dark" onClick={handlePrintClick}>Print saved weather</Button>
        <Table striped bordered hover variant="light" className="table">
          <thead>
            <tr>
              <th>City</th>
              <th>Country</th>
              <th>Weather</th>
              <th>Temperature</th>
              <th>Search time</th>
              <th></th>
            </tr>
          </thead>
          <tbody>
            {savedData.map((savedItem) => (
              <tr key={savedItem.id}>
                <td>{savedItem.city}</td>
                <td>{savedItem.country}</td>
                <td>{savedItem.title} - {savedItem.description}</td>
                <td><img src={`http://openweathermap.org/img/w/${savedItem.icon}.png`} alt="Weather Icon" /> {savedItem.temperature}°C</td>
                <td>{formatDate(savedItem.created)}</td>
                <td>
                  <Button variant="info" onClick={() => handleDetailsClick(savedItem)}>Details</Button>
                  <Button variant="danger" onClick={() => handleRemoveButtonClick(savedItem.id)}>Remove</Button>
                </td>
              </tr>
            ))}
          </tbody>
        </Table>
        <Modal show={showModal} onHide={handleCloseModal} centered>
          <Modal.Header closeButton>
            <Modal.Title>{selectedSavedItem && `${selectedSavedItem.city}, ${selectedSavedItem.country}`}</Modal.Title>
          </Modal.Header>
          <Modal.Body className="text-center">
            {selectedSavedItem && (
              <div>
                <img src={`http://openweathermap.org/img/w/${selectedSavedItem.icon}.png`} alt="Weather Icon" />
                <b>{selectedSavedItem.temperature}°C</b>
                <p>Description: <b>{selectedSavedItem.title} - {selectedSavedItem.description}</b></p>
                <p>Pressure: <b>{selectedSavedItem.pressure} hPa</b></p>
                <p>Humidity: <b>{selectedSavedItem.humidity}%</b></p>
                <p>Wind Speed: <b>{selectedSavedItem.windSpeed} m/s</b></p>
                <p>Sunrise: <b>{formatDate(selectedSavedItem.sunrise)}</b></p>
                <p>Sunset: <b>{formatDate(selectedSavedItem.sunset)}</b></p>
                <p>Searched: <b>{formatDate(selectedSavedItem.created)}</b></p>
              </div>
            )}
          </Modal.Body>
          <Modal.Footer className="justify-content-center">
            <Button variant="dark" onClick={handleCloseModal}>Close</Button>
          </Modal.Footer>
        </Modal>
      </div>
    </div>
  );
};

export default Saved;