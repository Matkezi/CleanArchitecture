import React from 'react';
import { Grid } from '@material-ui/core';
import styles from './styles.module.scss';
import { dateHelper } from '../../helpers/dateHelper';
import { Booking } from '../../types/Booking';
import LocationIcon from '../../assets/img/icons/location-icon-blue-24.png';
import BoatIcon from '../../assets/img/icons/boat-icon-blue-24.png';
import CalendarIcon from '../../assets/img/icons/calendar-icon-blue-24.png';
import CrewIcon from '../../assets/img/icons/crew-icon-blue-24.png';
import PhoneIcon from '../../assets/img/icons/phone-icon-blue-29.png';
import EmailIcon from '../../assets/img/icons/email-icon-blue-28.png';


interface IProps {
    booking: Booking
}



const SkipperAcceptedComponent: React.FC<IProps> = (props: IProps) => {
    return (<div className={styles.wrapper}>
        <Grid container justify="center">
            <Grid container item xs={12} justify="flex-start">
                <p className={styles.heading}>Congratulations, the Skipper has accepted your request! <br /> Your trip is all set.</p>
            </Grid>
            <Grid container item xs={12}>
                <Grid item container justify="flex-start">
                    <p className={styles.booking}>
                        My Booking
                    </p>
                </Grid>
            </Grid>
            <Grid container direction="row" item className={styles.bookingCard}>
                <Grid item xs={3} container direction="column" className={styles.imageContainer}>
                    <Grid item>
                        <div className={styles.imageDiv}>
                            <img src={props.booking.boat!.boathPhotoUrl} alt="" className={styles.image} />
                        </div>
                    </Grid>
                    {/* Ovo je za Relju */}
                    {/* <Grid item>
                        <img src={props.booking.skipper!.userPhotoUrl} alt="" className={styles.userPhoto} />
                    </Grid> */}
                </Grid>
                <Grid container direction="row" xs={9} item justify="center" className={styles.infoDiv}>
                    <Grid item container alignItems="flex-start" justify="center">
                        <Grid item xs={10} container alignItems="flex-start" style={{ marginTop: 20 }}>
                            <div className={styles.skipperName}>
                                {props.booking.skipper?.firstName!}
                            </div>
                        </Grid>
                        <Grid item xs={10} container direction="row" alignItems="flex-start" style={{ marginTop: -20 }}>
                            <Grid item xs={12} md={6}>
                                <img src={PhoneIcon} alt="" height={29} width={29} className={styles.icon} />
                                <div className={styles.info}>+09975534651</div>
                            </Grid>
                            <Grid item xs={12} md={6}>
                                <img src={EmailIcon} alt="" height={29} width={29} className={styles.icon} />
                                <div className={styles.info}>{props.booking.skipper?.email!}</div>
                            </Grid>
                        </Grid>
                    </Grid>
                    <Grid item container alignItems="flex-end" style={{ marginBottom: 25 }} justify="center">
                        <Grid container item xs={10} direction="row" >
                            <Grid item xs={12} md={6}>
                                <img alt="" src={LocationIcon} className={styles.icon} />
                                <div className={styles.info}>{props.booking.onboardingLocation}</div>
                            </Grid>
                            <Grid item xs={12} md={6}>
                                <img alt="" src={BoatIcon} className={styles.icon} />
                                <div className={styles.info}>{props.booking.boat!.model} {props.booking.boat!.length} m</div>
                            </Grid>
                        </Grid>
                        <Grid container item xs={10} direction="row" >
                            <Grid item xs={12} md={6}>
                                <img alt="" src={CalendarIcon} className={styles.icon} />
                                <div className={styles.info}>{dateHelper.formatDates(props.booking.bookedTo!, props.booking.bookedFrom!)}</div>
                            </Grid>
                            <Grid item xs={12} md={6}>
                                <img alt="" src={CrewIcon} className={styles.icon} />
                                <div className={styles.info}>{props.booking.crewSize}</div>
                            </Grid>
                        </Grid>
                    </Grid>
                </Grid>
            </Grid>
        </Grid>
    </div>);

}

export default SkipperAcceptedComponent;