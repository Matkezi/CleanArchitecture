import React from 'react';
import { Grid } from '@material-ui/core';
import { Formik } from "formik";
import * as Yup from "yup";
import styles from './styles.module.scss';
import OkIcon from '../../../assets/img/icons/ok-icon-blue-208.png';

interface IProps {
    email: string,
    changePassword: (data: IData) => void,
    isSuccessful: boolean
}
interface IData {
    password: string,
    repeatPassword: string
}

const ForgottenPasswordComponent: React.FC<IProps> = (props: IProps) => {
    return (
        <Grid container justify="center">
            <Grid item>
                <div className={styles.resetPassword}>Reset password</div>
            </Grid>
            {!props.isSuccessful ?
                <>
                    <Grid item>
                        <div className={styles.resetPass}>Reseting password for email: <b>{props.email}</b></div>
                    </Grid>
                    <Formik
                        initialValues={{ password: "", repeatPassword: "" }}
                        onSubmit={(values: IData) => props.changePassword(values)}
                        validationSchema={Yup.object().shape({
                            password: Yup.string().required("Required").min(8, 'Password is to short, 8 character minimum'),
                            repeatPassword: Yup.string().test('password-match', 'Passwords do not match', function (value) {
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
                                <form onSubmit={handleSubmit}>
                                    <Grid container item xs={12} direction="column">
                                        <Grid item container justify="center" xs={12}>
                                            <Grid item xs={10} container justify="center" className={styles.inputField}>
                                                <input
                                                    name="password"
                                                    type="password"
                                                    id='password'
                                                    placeholder="Enter new password"
                                                    value={values.password}
                                                    onChange={handleChange}
                                                    onBlur={handleBlur}
                                                />
                                                {errors.password && touched.password && (
                                                    <div className="input-feedback" style={{ marginTop: -28 }}>{errors.password}</div>
                                                )}
                                            </Grid>
                                            <Grid item xs={10} container justify="center" className={styles.inputField}>
                                                <input
                                                    name="repeatPassword"
                                                    id='repeatPassword'
                                                    type="password"
                                                    placeholder="Repeat new password"
                                                    value={values.repeatPassword}
                                                    onChange={handleChange}
                                                    onBlur={handleBlur}
                                                />
                                                {errors.repeatPassword && touched.repeatPassword && (
                                                    <div className="input-feedback" style={{ marginTop: -28 }}>{errors.repeatPassword}</div>
                                                )}
                                            </Grid>
                                        </Grid>
                                        <Grid item container xs={12} justify="center">
                                            <button type="submit" disabled={isSubmitting} className={styles.loginButton}>
                                                <span>Change password</span>
                                            </button>
                                        </Grid>
                                    </Grid>
                                </form>
                            );
                        }}
                    </Formik>
                </>
                :
                <Grid container item xs={12} justify="center">
                    <Grid container item xs={12} justify="center">
                        <div>You sucessfuly changed your password!</div>
                        <div>We will redirect You to login in few seconds...</div>
                    </Grid>
                    <Grid container item xs={12} justify="center">
                        <img style={{ marginTop: 23 }} src={OkIcon} alt="" />
                    </Grid>
                </Grid>
            }
        </Grid>
    )
}

export default ForgottenPasswordComponent;