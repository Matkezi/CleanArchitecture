import { Booking, BookingStatusEnum } from "../../../types/Booking";
import React, { useState } from "react";
import { Grid, Divider } from "@material-ui/core";
import styles from "./styles.module.scss";
import { dateHelper } from './../../../helpers/dateHelper';
import CheckIcon from '../../../assets/img/icons/check-icon-green-37.png';
import WaitingIcon from '../../../assets/img/icons/clock-icon-green-37.png';
import ArrowIcon from '../../../assets/img/icons/arrow-icon-green-30.png';
import RejectIcon from '../../../assets/img/icons/reject-icon-black-38.png';
import NoSkipperIcon from '../../../assets/img/icons/no-skipper-icon-green-56.png';

export interface IProps {
  booking: Booking;
  editAction: (booking: Booking) => void;
}


const CharterBookingRow: React.FC<IProps> = (props: IProps) => {
  const [expanded, setExpanded] = useState(false);

  return (
    <React.Fragment>
      <Grid
        container
        alignItems="center"
        className={expanded ? styles.bookingRowExpanded : styles.bookingRow}
        direction="column"
      >
        <Grid
          container
          item
          justify="space-around"
          alignItems="center"
          className={styles.row}
        >
          <Grid item xs={1} container justify="center">
            {
              props.booking.status === BookingStatusEnum.SkipperAccepted ?
                <img alt="" width={37} height={37} src={CheckIcon} /> :
                (props.booking.status === BookingStatusEnum.SkipperRequested || props.booking.status === BookingStatusEnum.SkipperRequestPending ?
                  <img alt="" width={37} height={37} src={WaitingIcon} /> :
                  null
                )
            }
          </Grid>
          <Divider orientation="vertical" />
          <Grid item xs={2} container justify="center">
            <div className={styles.info}>{props.booking.guestName}</div>
          </Grid>
          <Divider orientation="vertical" />
          <Grid item xs={2} container justify="center">
            <div className={styles.info}>{props.booking.boat?.name}</div>
          </Grid>
          <Divider orientation="vertical" />
          <Grid item xs={2} container direction="row">
            {props.booking.skipper?.id ? (
              <>
                <Grid item xs={6} container alignItems="center">
                  <img
                    className={styles.avatar}
                    src={props.booking.skipper?.userPhotoUrl}
                    alt=""
                  />
                </Grid>
                <Grid item xs={6} container alignItems="center">
                  <div className={styles.name}>
                    {props.booking.skipper?.firstName}
                  </div>
                </Grid>
              </>
            ) : (
                <>
                  <Grid item xs={6} container alignItems="center">
                    <img
                      className={styles.avatarReject}
                      src={NoSkipperIcon}
                      alt=""
                    />
                  </Grid>
                  <Grid item xs={6} container alignItems="center">
                    <div className={styles.name}>Skipper not chosen</div>
                  </Grid>
                </>
              )}
          </Grid>
          <Divider orientation="vertical" />
          <Grid item xs={3} direction="row" container>
            <Grid item xs={9}>
              <div className={styles.info} style={{ marginTop: 3 }}>
                {dateHelper.formatDates(
                  props.booking.bookedFrom!,
                  props.booking.bookedTo!
                )}
              </div>
            </Grid>
            <Grid item xs={3}>
              {expanded && (
                <div onClick={() => setExpanded(!expanded)}> <img alt="" style={{ cursor: "pointer" }} width={30} height={30} src={ArrowIcon} /></div>
              )}
              {!expanded && (
                <div onClick={() => setExpanded(!expanded)}>
                  <img alt="" width={30} height={30} style={{ cursor: "pointer", transform: "rotate(180deg)" }} src={ArrowIcon} />
                </div>
              )}
            </Grid>
          </Grid>
        </Grid>

        {expanded && (
          <>
            {props.booking.status !== BookingStatusEnum.SkipperAccepted ? (
              <Grid item container justify="flex-end">
                <button
                  className={styles.editButton}
                  onClick={() => props.editAction(props.booking)}
                >
                  <span>Edit the Booking</span>
                </button>
              </Grid>
            ) : null}
            {props.booking.bookingHistories?.length! > 0 ? (
              props.booking.bookingHistories!.map(booking => (
                <Grid container xs={12} item alignItems="center" className={styles.bookingRowRejected}
                >
                  <Grid item xs={1} container justify="center">
                    <img alt="" src={RejectIcon} />
                  </Grid>
                  <Divider orientation="vertical" />
                  <Grid item xs={5}>
                    <div style={{ marginLeft: 15 }}>
                      {booking.bookingRejected === 0
                        ? "Skipper has rejected the request."
                        : "Skipper did not respond in time."}
                    </div>
                  </Grid>
                  <Grid item xs={4} container>
                    <Grid item xs={3}>
                      <img className={styles.avatar} src={booking.skipper?.userPhotoUrl} alt=""
                      />
                    </Grid>
                    <Grid item xs={9} container alignItems="center">
                      <div>{booking.skipper?.firstName}</div>
                    </Grid>
                  </Grid>
                </Grid>
              ))
            ) : (
                <Grid container xs={12} item alignItems="center" className={styles.bookingRowRejected} >
                  <Grid item xs={12} container justify="center">
                    <span>No booking histories to show</span>
                  </Grid>
                </Grid>
              )}
          </>
        )}
      </Grid>
    </React.Fragment>
  );
};

export default CharterBookingRow;
