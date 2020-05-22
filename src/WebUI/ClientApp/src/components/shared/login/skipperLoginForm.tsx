import React from "react";
import styles from "./styles.module.scss";
import FacebookLogin, { ReactFacebookLoginInfo } from "react-facebook-login";
import { ILoginData } from "../../../types/LoginProps";
import { Formik } from "formik";
import * as Yup from "yup";
import { Grid } from "@material-ui/core";
import { CLIENT } from "../../../constants/clientRoutes";

export interface IProps {
  facebookLoginAction: (facebookResponse: ReactFacebookLoginInfo) => void;
  normalLoginAction: (loginData: ILoginData) => void;
  history: any;
}

const SkipperLoginForm: React.FC<IProps> = (props: IProps) => {
  return (
    <>
      <Grid item container justify="center" xs={12}>
        <span className={styles.welcome}>Welcome!</span>
      </Grid>
      <Grid item container justify="center" xs={12}>
        <FacebookLogin
          appId="1511986492259035"
          fields="name,email,picture"
          cssClass={styles.facebookLogin}
          callback={props.facebookLoginAction}
          icon="fab fa-facebook-square"
          textButton="Continue with Facebook"
        />
      </Grid>
      <Formik
        initialValues={{ email: "", password: "", rememberMe: false }}
        onSubmit={(values: ILoginData) => props.normalLoginAction(values)}
        validationSchema={Yup.object().shape({
          email: Yup.string()
            .email()
            .required("Required"),
          password: Yup.string()
            .required("Required"),
        })}
      >
        {props2 => {
          const {
            values,
            touched,
            errors,
            isSubmitting,
            handleChange,
            handleBlur,
            handleSubmit
          } = props2;
          return (
            <form onSubmit={handleSubmit}>
              <Grid container item xs={12} direction="column">
                <Grid item container justify="center" xs={12}>
                  <span className={styles.orText}>or</span>
                </Grid>
                <Grid item container justify="center" xs={12}>
                  <Grid item xs={10} container justify="center" style={{ justifyContent: "left" }} className={styles.inputField}>
                    <input
                      name="email"
                      type="text"
                      placeholder="Enter your email"
                      value={values.email}
                      onChange={handleChange}
                      onBlur={handleBlur}
                    />
                    {errors.email && touched.email && (
                      <div className="input-feedback" style={{ marginTop: -28 }}>{errors.email}</div>
                    )}
                  </Grid>
                  <Grid item xs={10} container justify="center" style={{ justifyContent: "left" }} className={styles.inputField}>
                    <input
                      name="password"
                      type="password"
                      placeholder="Enter your password"
                      value={values.password}
                      onChange={handleChange}
                      onBlur={handleBlur}
                    />
                    {errors.password && touched.password && (
                      <div className="input-feedback" style={{ marginTop: -28 }}>{errors.password}</div>
                    )}
                  </Grid>
                </Grid>
                <Grid item container xs={12} justify="center">
                  <button type="submit" disabled={isSubmitting} className={styles.loginButton}>
                    <span>Login</span>
                  </button>
                </Grid>
                <Grid item container xs={12} justify="center">
                  <div style={{ cursor: "pointer", marginTop: 20 }} className={styles.forgottenPassword} onClick={() => props.history.push(CLIENT.APP.FORGOTTEN_PASSWORD)}>Forgot your password?</div>
                </Grid>
              </Grid>
            </form>
          );
        }}
      </Formik>
    </>
  );
};

export default SkipperLoginForm;
