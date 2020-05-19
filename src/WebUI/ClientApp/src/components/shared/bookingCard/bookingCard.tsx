import React from "react";
import { Grid } from "@material-ui/core";
import { Booking, BookingStatusEnum, SkipperActionEnum } from "../../../types/Booking";
import styles from './styles.module.scss'
import { dateHelper } from "../../../helpers/dateHelper";
import LocationIcon from '../../../assets/img/icons/location-icon-blue-24.png';
import BoatIcon from '../../../assets/img/icons/boat-icon-blue-24.png';
import CalendarIcon from '../../../assets/img/icons/calendar-icon-blue-24.png';
import CrewIcon from '../../../assets/img/icons/crew-icon-blue-24.png';


export interface IProps {
  booking: Booking;
  bookingAction?: (id: number, action: SkipperActionEnum) => void;
  showMessage: boolean;
  textForDisplay: string;
  skipperView: boolean;
  pending?: boolean;
}

const BookingCard: React.FC<IProps> = (props: IProps) => {
  return (
    <Grid container direction="row">
      <Grid item xs={3} className={styles.imageDiv}>
        <div className={styles.imageDiv}>
          <img src={props.booking.boat!.boatPhotoUrl} alt="" className={styles.image}></img>
        </div>
      </Grid>
      <Grid container item xs={9} direction="row" justify="space-evenly" spacing={1} className={props.skipperView ? props.pending ? styles.infoDivSkipperPending : styles.infoDivSkipper : styles.infoDiv}>
        <Grid item xs={11} container alignItems="flex-start">
          <div className={props.skipperView ? styles.guestNameSkipper : styles.guestName}>
            {props.textForDisplay}
          </div>
        </Grid>
        <Grid container item xs={11} direction="row" justify="space-evenly" spacing={1}>
          <Grid item xs={12} md={6}>
            <img alt="" src={LocationIcon} className={styles.icon} />
            <div className={styles.info}>{props.booking.onboardingLocation}</div>
          </Grid>
          <Grid item xs={12} md={6}>
            <img alt="" src={BoatIcon} className={styles.icon} />
            <div className={styles.info}>{props.booking.boat!.model}, {props.booking.boat!.length} m</div>
          </Grid>
        </Grid>
        <Grid container item xs={11} direction="row" justify="space-evenly" spacing={1}>
          <Grid item xs={12} md={6}>
            <img alt="" src={CalendarIcon} className={styles.icon} />
            <div className={styles.info}>{dateHelper.formatDates(props.booking.bookedFrom!, props.booking.bookedTo!)}</div>
          </Grid>
          <Grid item xs={12} md={6}>
            <img alt="" src={CrewIcon} className={styles.icon} />
            <div className={styles.info}>{props.booking.crewSize}</div>
          </Grid>
        </Grid>
        {props.booking.status === BookingStatusEnum.SkipperAccepted &&
          <Grid container item xs={11} direction="row" justify="flex-start" spacing={1}>
            <Grid item xs={12} md={6}>
              <img alt="" src={CalendarIcon} className={styles.icon} />
              <div className={styles.info}>{props.booking.guestEmail}</div>
            </Grid>
          </Grid>}

        {props.showMessage && <Grid container item xs={11} direction="row" justify="flex-start">
          <Grid item xs={11}>{props.booking ? props.booking.guestMessege : ""} </Grid>
        </Grid>}
        {props.booking.status === BookingStatusEnum.SkipperRequested ? (<Grid container item xs={12} direction="row" justify="flex-end">
          <>
            <Grid item xs={6} sm={3} container justify="flex-end">
              <button
                className={styles.declineBtn}
                onClick={() => props.bookingAction!(props.booking.id!, SkipperActionEnum.Decline)}
              >
                <span>Decline</span>
              </button>
            </Grid>
            <Grid item xs={6} sm={3} container justify="flex-end">
              <button
                className={styles.acceptBtn}
                onClick={() => props.bookingAction!(props.booking.id!, SkipperActionEnum.Accept)}
              >
                <span>Accept</span>
              </button>
            </Grid>
          </>
        </Grid>) : null}
      </Grid>
    </Grid >
  );
};

export default BookingCard;
