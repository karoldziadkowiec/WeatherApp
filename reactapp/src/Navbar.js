import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import { Link } from 'react-router-dom';

const AppNavbar = () => {
  return (
    <>
      <Navbar bg="dark" variant="dark">
        <Container>
          <Navbar.Brand as={Link} to="/">WeatherApp</Navbar.Brand>
          <Nav className="me-auto">
            <Nav.Link as={Link} to="/">Weather</Nav.Link>
            <Nav.Link as={Link} to="/history">History</Nav.Link>
            <Nav.Link as={Link} to="/saved">Saved</Nav.Link>
          </Nav>
        </Container>
      </Navbar>
    </>
  );
}

export default AppNavbar;