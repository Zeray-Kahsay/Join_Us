import { observer } from "mobx-react-lite";
import { useStore } from "../app/stores/store";
import { ErrorMessage, Formik } from "formik";
import { Button, Form, Header } from "semantic-ui-react";
import MyTextInput from "../app/common/form/MyTextInput";
import * as yup from "yup";
import ValidationError from "../features/errors/ValidationError";

const RegisterForm = () => {
  const { userStore } = useStore();
  return (
    <Formik
      initialValues={{
        displayName: "",
        username: "",
        email: "",
        password: "",
        error: null,
      }}
      onSubmit={(values, { setErrors }) =>
        userStore.register(values).catch((error) => setErrors({ error }))
      }
      validationSchema={yup.object({
        displayName: yup.string().required(),
        username: yup.string().required(),
        email: yup.string().required(),
        password: yup.string().required(),
      })}
    >
      {({ handleSubmit, isSubmitting, errors, isValid, dirty }) => (
        <Form
          className="ui form error"
          onSubmit={handleSubmit}
          autoComplete="off"
        >
          <Header
            as="h2"
            content="Register to Join~Us"
            color="teal"
            textAlign="center"
          />
          <MyTextInput placeholder="Display name" name="displayName" />
          <MyTextInput placeholder="username" name="username" />
          <MyTextInput placeholder="Email" name="email" />
          <MyTextInput placeholder="Password" name="password" type="password" />
          <ErrorMessage
            name="error"
            render={() => (
              <ValidationError errors={errors.error as unknown as string[]} />
            )}
          />
          <Button
            disabled={!isValid || !dirty || isSubmitting}
            loading={isSubmitting}
            positive
            content="Register"
            type="submit"
            fluid
          />
        </Form>
      )}
    </Formik>
  );
};

export default observer(RegisterForm);
