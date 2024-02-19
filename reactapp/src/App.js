import React from 'react';
import './App.css';
import Navbar from './Navbar';
import Footer from './Footer';
import Home from './Home';
import SunInfo from './SunInfo';
import History from './History';
import Saved from './Saved';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';

const App = () => {
  return (
    <Router>
      <Navbar />
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/sun" element={<SunInfo />} />
        <Route path="/history" element={<History />} />
        <Route path="/saved" element={<Saved />} />
      </Routes>
      <Footer /> 
    </Router>
  );
}

export default App;