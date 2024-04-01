import React from 'react';
import './App.css';
import Navbar from './components/layout/Navbar';
import Footer from './components/layout/Footer';
import Home from './components/home/Home';
import SunInfo from './components/suninfo/SunInfo';
import History from './components/history/History';
import Saved from './components/saved/Saved';
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