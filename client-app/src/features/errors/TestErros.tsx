import axios from "axios";
import { useState } from "react";
import { Button, Header, Segment } from "semantic-ui-react";
import ValidationError from "./ValidationError";

const TestErros = () => {
  const baseUrl = "http://localhost:5000/api/";
  const [errors, setErrors] = useState(null);

  const handleNotFound = () => {
    axios
      .get(baseUrl + "buggy/not-found")
      .catch((error) => console.log(error.response));
  };

  const handleBadRequest = () => {
    axios
      .get(baseUrl + "buggy/bad-request")
      .catch((error) => console.log(error.response));
  };

  const handleUnauthorized = () => {
    axios
      .get(baseUrl + "buggy/unauthorized")
      .catch((error) => console.log(error.response));
  };

  const handleBadGuid = () => {
    axios
      .get(baseUrl + "activities/notaguid")
      .catch((error) => console.log(error.response));
  };

  const handleValidationError = () => {
    axios.post(baseUrl + "activities", {}).catch((error) => setErrors(error));
  };

  const handleServerError = () => {
    axios
      .get(baseUrl + "buggy/server-error")
      .catch((error) => console.log(error.response));
  };

  return (
    <>
      <Header as="h1" content="Test Error component" />
      <Segment>
        <Button.Group widths="7">
          <Button onClick={handleNotFound} content="Not Found" basic primary />
          <Button
            onClick={handleBadRequest}
            content="Bad Request"
            basic
            primary
          />
          <Button
            onClick={handleValidationError}
            content="Validation Error"
            basic
            primary
          />
          <Button
            onClick={handleServerError}
            content="Server Error"
            basic
            primary
          />
          <Button
            onClick={handleUnauthorized}
            content="Unauthorised"
            basic
            primary
          />
          <Button onClick={handleBadGuid} content="Bad Guid" basic primary />
        </Button.Group>
      </Segment>
      {errors && <ValidationError errors={errors} />}
    </>
  );
};

export default TestErros;
