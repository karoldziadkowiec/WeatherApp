import React from 'react';

const Footer = () => {
    return (
        <footer className="border-top footer bg-white" style={{ position: 'fixed', bottom: 0, width: '100%', zIndex: 100 }}>
            <div className="container py-3">
                <center>&copy; 2024 - WeatherApp</center>
            </div>
        </footer>
    );
}

export default Footer;