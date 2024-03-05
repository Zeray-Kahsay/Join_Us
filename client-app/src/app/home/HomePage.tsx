import { Link } from "react-router-dom";
import { Container } from "semantic-ui-react";

const HomePage = () => {
  return (
    <Container style={{ marginTop: "7em" }}>
      <h1>Home page</h1>
      <h3>
        <Link to="/activities"> Go to Activities Dashbord</Link>{" "}
      </h3>
    </Container>
  );
};

export default HomePage;
