import React from 'react';
import { Grid } from '@material-ui/core';
import styles from './styles.module.scss';
import { Formik } from "formik";
import * as Yup from "yup";

interface IProps {
    email?: string,
    disabled: boolean,
    showSuccess: boolean,
    sendChangePasswordEmail: (email: string) => void
}

const ForgottenPasswordRequest: React.FC<IProps> = (props: IProps) => {
    return (
        <Grid container justify="center">
            <Grid item xs={12} container justify="center">
                <div className={styles.resetPassword}>Reset password</div>
            </Grid>
            <Formik
                initialValues={{ email: props.email ? props.email : "" }}
                onSubmit={(values: any) => props.sendChangePasswordEmail(values.email)}
                validationSchema={Yup.object().shape({
                    email: Yup.string()
                        .email()
                        .required("Required")
                })}
            >
                {props2 => {
                    const {
                        values,
                        touched,
                        errors,
                        handleChange,
                        handleBlur,
                        handleSubmit
                    } = props2;
                    return (
                        <form onSubmit={handleSubmit}>

                            {!props.showSuccess ? <>
                                <Grid item xs={12} container justify="center">
                                    <div className={styles.resetPass}>Reseting password for email</div>
                                </Grid>
                                <Grid item xs={12} container justify="center">
                                    <Grid item xs={12} container justify="center" className={styles.inputField}>
                                        <Grid item xs={12} container justify="center">
                                            <input
                                                name="email"
                                                type="text"
                                                placeholder="Enter your email"
                                                value={values.email}
                                                onChange={handleChange}
                                                onBlur={handleBlur}
                                                style={{ width: 240 }}
                                            />
                                        </Grid>
                                        <Grid item xs={12} container justify="center">
                                            {errors.email && touched.email && (
                                                <div className="input-feedback" style={{ marginTop: -28 }}>{errors.email}</div>
                                            )}
                                        </Grid>
                                    </Grid>
                                    <Grid item>
                                        <div className={styles.resetText}>We will send you email with instructions how to reset Your password.</div>
                                    </Grid>
                                    <Grid item container xs={12} justify="center">
                                        <button type="submit" disabled={props.disabled} className={styles.loginButton} onClick={() => handleSubmit}>
                                            <span>Send email</span>
                                        </button >
                                    </Grid>
                                </Grid>
                            </>
                                : <Grid container justify="center">
                                    <Grid item xs={12}>
                                        <div className={styles.resetText}>
                                            You will soon recieve email with instructions how to reset Your password.
                                        </div>
                                        <div className={styles.resetText}>
                                            If you did not recieve email in couple of minutes, send Resend button.
                                        </div>
                                    </Grid>
                                    <Grid item container xs={12} justify="center">
                                        <button type="submit" disabled={props.disabled} className={styles.loginButton} onClick={() => handleSubmit()}>
                                            <span>Resend email</span>
                                        </button >
                                    </Grid>
                                </Grid>}
                        </form>
                    );
                }}
            </Formik>
        </Grid >
    )
}

export default ForgottenPasswordRequest;