import React, { useState, useEffect } from 'react';
import { Table, Modal, Button, Form } from 'react-bootstrap';
import ServerURL from '../../services/server/ServerURL';
import '../../styles/History.css';

const History = () => {
  const [historyData, setHistoryData] = useState([]);
  const [selectedHistoryItem, setSelectedHistoryItem] = useState(null);
  const [showModal, setShowModal] = useState(false);
  const [searchTerm, setSearchTerm] = useState('');
  const [savedWeather, setSavedWeather] = useState([]);

  useEffect(() => {
    fetchHistoryData();
    fetchSavedWeather();
  }, []);

  const fetchHistoryData = async () => {
    try {
      const response = await fetch(`${ServerURL}/history`);

      if (response.ok) {
        const data = await response.json();
        setHistoryData(data);
      } else {
        console.error('Failed to fetch history data');
      }
    } catch (error) {
      console.error('Error fetching history data:', error);
    }
  };

  const fetchSavedWeather = async () => {
    try {
      const response = await fetch(`${ServerURL}/saved`);
      if (response.ok) {
        const data = await response.json();
        setSavedWeather(data);
      } else {
        console.error('Failed to fetch saved weather data');
      }
    } catch (error) {
      console.error('Error fetching saved weather data:', error);
    }
  };

  const formatDate = (dateString) => {
    const date = new Date(dateString);
    return date.toLocaleDateString('en-US') + ' ' + date.toLocaleTimeString('en-US');
  };

  const handleDetailsClick = (historyItem) => {
    setSelectedHistoryItem(historyItem);
    setShowModal(true);
  };

  const handleCloseModal = () => {
    setShowModal(false);
    setSelectedHistoryItem(null);
  };

  const handlePrintClick = async () => {
    try {
      const response = await fetch(`${ServerURL}/history/csv`);
      if (response.ok) {
        const blob = await response.blob();
        const url = window.URL.createObjectURL(new Blob([blob]));
        const a = document.createElement('a');
        a.href = url;
        a.download = 'history.xlsx';
        document.body.appendChild(a);
        a.click();
        a.remove();
      } else {
        console.error('Failed to fetch history data');
      }
    } catch (error) {
      console.error('Error fetching history data:', error);
    }
  };

  const handleSearchChange = (event) => {
    setSearchTerm(event.target.value);
  };

  const handleSearchSubmit = async (event) => {
    event.preventDefault();
    try {
      const response = await fetch(`${ServerURL}/history/partial/${searchTerm}`);
      if (response.ok) {
        const data = await response.json();
        setHistoryData(data);
      } else {
        console.error('Failed to fetch history data');
      }
    } catch (error) {
      console.error('Error fetching history data:', error);
    }
  };

  const isWeatherSaved = (id) => {
    return savedWeather.some(savedItem => savedItem.weatherId === id);
  };

  const handleSaveButtonClick = async (historyItem) => {
    try {
      const response = await fetch(`${ServerURL}/saved`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(historyItem),
      });
  
      if (response.ok) {
        console.log('Weather saved successfully');
        window.location.href = '/Saved';
      } else {
        console.error('Failed to save weather');
      }
    } catch (error) {
      console.error('Error saving weather:', error);
    }
  };

  return (
    <div className="History">
      <div className="centered-history-content">
        <h1>WeatherApp</h1>
        <h4>Search history</h4>
        <h5>Check out the latest weather searches.</h5>
        <Button variant="dark" onClick={handlePrintClick}>Print history</Button>
        <Form onSubmit={handleSearchSubmit} className="mb-3">
          <Form.Group controlId="searchTerm" style={{ display: 'flex', alignItems: 'center', justifyContent: 'center' }}>
            <Form.Control type="text" placeholder="Search city" value={searchTerm} onChange={handleSearchChange} style={{ width: '20%', marginRight: '10px' }} />
            <Button variant="primary" type="submit">Search</Button>
          </Form.Group>
        </Form>
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
            {historyData.map((historyItem) => (
              <tr key={historyItem.id}>
                <td>{historyItem.city}</td>
                <td>{historyItem.country}</td>
                <td>{historyItem.title} - {historyItem.description}</td>
                <td><img src={`http://openweathermap.org/img/w/${historyItem.icon}.png`} alt="Weather Icon" /> {historyItem.temperature}°C</td>
                <td>{formatDate(historyItem.created)}</td>
                <td>
                  <Button variant="info" onClick={() => handleDetailsClick(historyItem)}>Details</Button>
                  {!isWeatherSaved(historyItem.id) && <Button variant="success" onClick={() => handleSaveButtonClick(historyItem)}>Save</Button>} {/* Sprawdzamy czy pogoda jest zapisana */}
                </td>
              </tr>
            ))}
          </tbody>
        </Table>
        <Modal show={showModal} onHide={handleCloseModal} centered>
          <Modal.Header closeButton>
            <Modal.Title>{selectedHistoryItem && `${selectedHistoryItem.city}, ${selectedHistoryItem.country}`}</Modal.Title>
          </Modal.Header>
          <Modal.Body className="text-center">
            {selectedHistoryItem && (
              <div>
                <img src={`http://openweathermap.org/img/w/${selectedHistoryItem.icon}.png`} alt="Weather Icon" />
                <b>{selectedHistoryItem.temperature}°C</b>
                <p>Weather: <b>{selectedHistoryItem.title} - {selectedHistoryItem.description}</b></p>
                <p>Pressure: <b>{selectedHistoryItem.pressure} hPa</b></p>
                <p>Humidity: <b>{selectedHistoryItem.humidity}%</b></p>
                <p>Wind Speed: <b>{selectedHistoryItem.windSpeed} m/s</b></p>
                <p>Sunrise: <b>{formatDate(selectedHistoryItem.sunrise)}</b></p>
                <p>Sunset: <b>{formatDate(selectedHistoryItem.sunset)}</b></p>
                <p>Searched: <b>{formatDate(selectedHistoryItem.created)}</b></p>
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

export default History;