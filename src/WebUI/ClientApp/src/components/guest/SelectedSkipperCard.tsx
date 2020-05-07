import React from 'react';
import { Skipper } from './../../types/Skipper';
import { Grid, TextField } from '@material-ui/core'
import styles from './../../containers/guests/styles.module.scss'
import PhoneIcon from '../../assets/img/icons/phone-icon-black-29.png'
import EmailIcon from '../../assets/img/icons/email-icon-black-28.png'

interface IProps {
    skipper: Skipper,
    changeMessage: (text: string) => void
}

const SelectedSkipperCard: React.FC<IProps> = (props: IProps) => {

    const { skipper } = props;

    return (
        <Grid container>
            <Grid item xs={12}>
                <p className={styles.welcome}>You are welcome to contact {skipper?.firstName}.</p>
            </Grid>
            <Grid container item style={{ marginBottom: 50 }}>
                <Grid item xs={2} container justify="flex-start" alignItems="center" className={styles.photoDesktop}>
                    <img src={skipper?.userPhotoUrl} alt="skipper_photo" className={styles.photo} />
                </Grid>
                <Grid item xs={12} md={10} container alignItems="flex-start">
                    <Grid item xs={12} container>
                        <Grid item xs={5} className={styles.photoMobile}>
                            <img src={skipper?.userPhotoUrl} alt="skipper_photo" className={styles.photo} />
                        </Grid>
                        <Grid item xs={6}>
                            <p className={styles.skipperName}>{skipper?.firstName}</p>
                        </Grid>
                    </Grid>
                    <Grid item container>
                        <Grid item xs={12} md={4} container>
                            <Grid item xs={1} container alignItems="center">
                                <img src={PhoneIcon} height={28} width={28} alt="" />
                            </Grid>
                            <Grid item xs={11}>
                                <p>&nbsp; {skipper?.phoneNumber}</p>
                            </Grid>
                        </Grid>
                        <Grid item md={6} xs={12} container>
                            <Grid item xs={1} container alignItems="center">
                                <img src={EmailIcon} height={28} width={28} alt="" />
                            </Grid>
                            <Grid item xs={11}>
                                <p>&nbsp; {skipper?.email}</p>
                            </Grid>
                        </Grid>
                    </Grid>
                </Grid>
            </Grid>
            <Grid item xs={12} style={{ marginBottom: 25 }}>
                <label className={styles.messageLabel}>Add a message (Optional)</label>
            </Grid>
            <Grid item xs={12} md={8}>
                <TextField
                    className={styles.textField}
                    placeholder="Hi, I am really looking foorward to sail with..."
                    multiline
                    rows="4"
                    variant="outlined"
                    onChange={e => props.changeMessage(e.target.value)}
                ></TextField>
            </Grid>
        </Grid>
    );
}

export default SelectedSkipperCard;