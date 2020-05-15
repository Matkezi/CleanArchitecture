import React from 'react';
import { Grid } from '@material-ui/core';
import styles from './styles.module.scss';
import OkIcon from '../../assets/img/icons/ok-icon-blue-208.png';

interface IProps {
    isSuccessful: boolean,
    newEmail: string,
    changeEmail: () => void,
    loading: boolean
}

const ChangeEmailComponent: React.FC<IProps> = (props: IProps) => {
    return (
        <Grid container justify="center">
            <Grid item>
                <div className={styles.resetPassword}>Change email</div>
            </Grid>
            {!props.isSuccessful ?
                <>
                    <Grid item container justify="center">
                        <div className={styles.resetPass}>Changing email to <b>{props.newEmail}</b></div>

                        <div className={styles.resetPass}>Press change email if you want to change your email.</div>

                        <Grid container item xs={12} justify="center">
                            <button disabled={props.loading} className={styles.buttonChange} onClick={() => props.changeEmail()}><span>Change email</span></button>
                        </Grid>
                    </Grid>
                </>
                :
                <Grid container item xs={12} justify="center">
                    <Grid container item xs={12} justify="center">
                        <div>You sucessfuly changed your email!</div>
                        <div>We will redirect You to login in few seconds...</div>
                    </Grid>
                    <Grid container item xs={12} justify="center">
                        <img style={{ marginTop: 23 }} src={OkIcon} alt="" />
                    </Grid>
                </Grid>
            }
        </Grid>)
}

export default ChangeEmailComponent;