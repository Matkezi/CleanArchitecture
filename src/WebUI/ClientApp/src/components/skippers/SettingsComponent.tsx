import React from 'react';
import { Grid } from '@material-ui/core';
import { Formik } from "formik";
import * as Yup from "yup";
import styles from './styles.module.scss'

interface IProps {
    email: string,
    clearInput?: () => void,
    requestEmailChange: (newEmail: string) => void,
    changePassword: (currentPassword: string, newPassword: string) => void
}

const SettingsComponent: React.FC<IProps> = (props: IProps) => {

    return (
        <>
            <Formik
                initialValues={{ email: "", repeatEmail: "" }}
                onSubmit={(values: any) => props.requestEmailChange(values.email)}
                validationSchema={Yup.object().shape({
                    email: Yup.string().email().required("Required"),
                    repeatEmail: Yup.string().test('email-match', 'Emails do not match', function (value) {
                        const { email } = this.parent;
                        return email === value;
                    })
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
                            <div className={styles.wrapper}>
                                <Grid container item xs={12} direction="column">
                                    <Grid item container justify="flex-start">
                                        <div className={styles.settingsText}>Personal account settings</div>
                                    </Grid>
                                    <Grid item container justify="center" xs={12}>
                                        <Grid item container xs={12}>
                                            <Grid item xs={12} md={3}>
                                                <div className={styles.settingsText2}> Change your email</div>
                                            </Grid>
                                            <Grid item xs={12} md={9} container justify="flex-start">
                                                <Grid item xs={12}>
                                                    <div className={styles.settingsText3}>Enter your new email and press SEND EMAIL and we will send you intstructions how to change your email.</div>
                                                </Grid>
                                                <Grid item xs={12} container justify="flex-start">
                                                    <Grid item container justify="center" className={styles.inputField} md={4} alignItems="flex-start">
                                                        <Grid item xs={12} container justify="center" alignItems="center">
                                                            <input
                                                                name="email"
                                                                type="text"
                                                                id='email'
                                                                placeholder="Enter new email"
                                                                value={values.email}
                                                                onChange={handleChange}
                                                                onBlur={handleBlur}
                                                            />
                                                        </Grid>
                                                        {errors.email && touched.email && (<Grid item xs={12}>
                                                            <div className="input-feedback">{errors.email}</div>
                                                        </Grid>
                                                        )}
                                                    </Grid>
                                                    <Grid item container justify="center" className={styles.inputField} md={4} alignItems="flex-start">
                                                        <Grid item xs={12} container justify="center" alignItems="center">
                                                            <input
                                                                name="repeatEmail"
                                                                id='repeatEmail'
                                                                type="text"
                                                                placeholder="Repeat new email"
                                                                value={values.repeatEmail}
                                                                onChange={handleChange}
                                                                onBlur={handleBlur}
                                                            />
                                                        </Grid>
                                                        {errors.repeatEmail && touched.repeatEmail && (
                                                            <Grid item xs={12}>
                                                                <div className="input-feedback">{errors.repeatEmail}</div>
                                                            </Grid>
                                                        )}
                                                    </Grid>
                                                    <Grid item container justify="center" md={3} alignItems="flex-start">
                                                        <button type="submit" disabled={isSubmitting} className={styles.buttonSend}>
                                                            <span>Send email</span>
                                                        </button>
                                                    </Grid>
                                                </Grid>
                                            </Grid>
                                        </Grid>

                                    </Grid>
                                    <Grid item container xs={12} justify="center">
                                    </Grid>

                                </Grid>
                            </div>
                        </form>
                    );
                }}
            </Formik >
            <Formik
                initialValues={{ currentPassword: "", password: "", repeatPassword: "" }}
                onSubmit={(values: any) => props.changePassword(values.currentPassword, values.password)}
                validationSchema={Yup.object().shape({
                    currentPassword: Yup.string().required('Required'),
                    password: Yup.string().required("Required"),
                    repeatPassword: Yup.string().test('pass-match', 'Passwords do not match', function (value) {
                        const { password } = this.parent;
                        return password === value;
                    })
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
                        <form onSubmit={handleSubmit} className={styles.passwordChange}>
                            <div className={styles.wrapper}>
                                <Grid container item xs={12} direction="column">
                                    <Grid item container justify="center" xs={12}>
                                        <Grid item container xs={12}>
                                            <Grid item xs={12} md={3}>
                                                <div className={styles.settingsText2}> Change your password</div>
                                            </Grid>
                                            <Grid item xs={12} md={9} container justify="flex-start">
                                                <Grid item xs={12}>
                                                    <div className={styles.settingsText3}>Enter your new password and press CHANGE PASSWORD.</div>
                                                </Grid>
                                                <Grid item xs={12} container justify="flex-start">
                                                    <Grid item container justify="center" className={styles.inputField} md={4} alignItems="flex-start">
                                                        <Grid item xs={12} container justify="center" alignItems="center">
                                                            <input
                                                                name="currentPassword"
                                                                type="password"
                                                                id='currentPassword'
                                                                placeholder="Enter current password"
                                                                value={values.currentPassword}
                                                                onChange={handleChange}
                                                                onBlur={handleBlur}
                                                            />
                                                        </Grid>
                                                        {errors.currentPassword && touched.currentPassword && (<Grid item xs={12}>
                                                            <div className="input-feedback">{errors.currentPassword}</div>
                                                        </Grid>
                                                        )}
                                                    </Grid>
                                                    <Grid item container justify="center" className={styles.inputField} md={4} alignItems="flex-start">
                                                        <Grid item xs={12} container justify="center" alignItems="center">
                                                            <input
                                                                name="password"
                                                                type="password"
                                                                id='password'
                                                                placeholder="Enter new password"
                                                                value={values.password}
                                                                onChange={handleChange}
                                                                onBlur={handleBlur}
                                                            />
                                                        </Grid>
                                                        {errors.password && touched.password && (<Grid item xs={12}>
                                                            <div className="input-feedback">{errors.password}</div>
                                                        </Grid>
                                                        )}
                                                    </Grid>
                                                    <Grid item container justify="center" className={styles.inputField} md={4} alignItems="flex-start">
                                                        <Grid item xs={12} container justify="center" alignItems="center">
                                                            <input
                                                                name="repeatPassword"
                                                                id='repeatPassword'
                                                                type="password"
                                                                placeholder="Repeat new password"
                                                                value={values.repeatPassword}
                                                                onChange={handleChange}
                                                                onBlur={handleBlur}
                                                            />
                                                        </Grid>
                                                        {errors.repeatPassword && touched.repeatPassword && (
                                                            <Grid item xs={12}>
                                                                <div className="input-feedback">{errors.repeatPassword}</div>
                                                            </Grid>
                                                        )}
                                                    </Grid>
                                                </Grid>
                                                <Grid item xs={12} container justify="flex-end">
                                                    <Grid item container justify="center" md={3} alignItems="flex-end" className={styles.buttonDiv}>
                                                        <button type="submit" disabled={isSubmitting} className={styles.buttonChange}>
                                                            <span>Change password</span>
                                                        </button>
                                                    </Grid>
                                                </Grid>
                                            </Grid>
                                        </Grid>

                                    </Grid>
                                    <Grid item container xs={12} justify="center">
                                    </Grid>

                                </Grid>
                            </div>
                        </form>
                    );
                }}
            </Formik >
        </>
    )
}

export default SettingsComponent;