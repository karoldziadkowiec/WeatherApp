import React from 'react';
import { Routes, Route } from 'react-router-dom';
import Home from '../components/home/Home';
import SunInfo from '../components/suninfo/SunInfo';
import History from '../components/history/History';
import Saved from '../components/saved/Saved';

const Routing = () => {
  return (
    <Routes>
      <Route path="/" element={<Home />} />
      <Route path="/sun" element={<SunInfo />} />
      <Route path="/history" element={<History />} />
      <Route path="/saved" element={<Saved />} />
    </Routes>
  );
}

export default Routing;