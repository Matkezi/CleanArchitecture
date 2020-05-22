import React, { useContext, useEffect, useState } from "react";
import GuestStep1 from "./guestStep1Container";
import { GuestBookingContext } from "../../providers/booking/guestBooking";
import { RouteComponentProps } from "react-router-dom";
import { Grid } from "@material-ui/core";
import GuestStep2 from "./guestStep2Container";
import GuestStep3 from "./guestStep3Container";
import BookingApi from "../../services/api/shared/booking";
import { BookingStatusEnum } from "../../types/Booking";
import { StepperHelper } from '../../helpers/StepperHelper';
import styles from './styles.module.scss';
import SkipperAcceptedComponent from '../../components/guest/skipperAcceptedComponent';
import WaitRespondIcon from '../../assets/img/icons/waiting-respond-icon-blue-220.png';
import { NotificationContext } from "../../providers/notification";
import { NotificationType } from "../../types/NotificationProps";

const GuestBookingContainer = (props: RouteComponentProps<any>) => {
  const guestBookingContext = useContext(GuestBookingContext);
  const notificationContext = useContext(NotificationContext);
  const [charterName, setCharterName] = useState("");
  const [tos, setTos] = useState(false);
  const [showError, setShowError] = useState(false);

  useEffect(() => {
    const fetchBooking = async () => {
      notificationContext.setLoading({ showLoading: true })
      try {
        var booking = await guestBookingContext.fetchGuestBooking(props.match.params.bookingUrl);
        setCharterName(booking.charter.charterName);
        getTime(new Date(booking.skipperRequestTime).getTime());
      } catch (e) {
        notificationContext.setSnackbar({ showSnackbar: true, message: e.message, type: NotificationType.Error })
      } finally {
        notificationContext.setLoading({ showLoading: false })
      }
    };
    const getItemsForSelect = async () => {
      await guestBookingContext.getSkills();
      await guestBookingContext.getLanguages();
    }
    fetchBooking();
    getItemsForSelect();
  }, []);

  const sendRequestToSkipper = async () => {
    notificationContext.setLoading({ showLoading: true })
    try {
      const booking = await BookingApi.postGuestAction(
        guestBookingContext.booking
      );
      guestBookingContext.setBooking(booking);
      clearIntervals();
      getTime(new Date(booking.skipperRequestTime!).getTime());
    } catch (e) {
      notificationContext.setSnackbar({ showSnackbar: true, message: e.message, type: NotificationType.Error })
    } finally {
      notificationContext.setLoading({ showLoading: false })
    }
  };

  var intervalId = 0;

  // Update the count down every 1 second
  function getTime(skipperRequestTime: number) {
    intervalId = window.setInterval(function () {
      // Get today's date and time
      var now = new Date().getTime();
      var deadline = skipperRequestTime + (1000 * 60 * 60 * 24 * 2);
      // Find the distance between now and the count down date
      var distance = (deadline - now);

      if (distance < 0) return;

      // Time calculations for days, hours, minutes and seconds
      var days = Math.floor(distance / (1000 * 60 * 60 * 24));
      var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
      var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
      var seconds = Math.floor((distance % (1000 * 60)) / 1000);

      // Display the result in the element with id="demo"
      var element = document!.getElementById("time-count")!;
      if (element !== null) {
        element!.innerHTML = days + "d " + hours + "h "
          + minutes + "m " + seconds + "s ";
      }
    }, 1000);
  }

  function clearIntervals() {
    var i = 0;
    for (i = 0; i < 10000; i++) {
      window.clearInterval(i);
    }
  }

  return (
    <div className={styles.wrapper}>
      {guestBookingContext.booking.status! ===
        BookingStatusEnum.SkipperRequestPending ? (
          <>
            {StepperHelper.getStep() === 1 ? (
              <GuestStep1 charterName={charterName} showError={showError && !tos} setTos={setTos} tos={tos}></GuestStep1>
            ) : StepperHelper.getStep() === 2 ? (
              <GuestStep2 skills={guestBookingContext.skills} languages={guestBookingContext.languages} history={props.history}></GuestStep2>
            ) : StepperHelper.getStep() === 3 ? (
              <GuestStep3></GuestStep3>
            ) : null}
            <div className={styles.stepper} >
              <Grid container xs={12} item justify="center">
                <Grid container direction="row" item xs={12} className={styles.stepperContainer} justify="center">
                  <Grid item xs={4}>
                    <div className={styles.step1}></div>
                  </Grid>
                  <Grid item xs={4}>
                    <div className={StepperHelper.getStep() > 1 ? styles.step2 + " " + styles.step2Active : styles.step2}></div>
                  </Grid>
                  <Grid item xs={4}>
                    <div className={StepperHelper.getStep() > 2 ? styles.step3 + " " + styles.step3Active : styles.step3}></div>
                  </Grid>
                </Grid>
                <Grid container direction="row" item xs={12} md={9} >
                  {StepperHelper.getStep() === 1 ? (
                    <>
                      <Grid item md={6} xs={12} />
                      <Grid item md={6} xs={12} container justify="flex-end">
                        <button className={styles.buttonFind} onClick={() => tos ? props.history.push(StepperHelper.increasedStepUrl()) : setShowError(true)}>
                          <span>Find a Skipper</span>
                        </button>
                      </Grid>
                    </>
                  ) : StepperHelper.getStep() === 2 ? (
                    <>
                      <Grid item xs={6}>
                        <button className={styles.previousBtn} onClick={() => props.history.push(StepperHelper.decreasedStepUrl())}>
                          <span>Previous</span>
                        </button>
                      </Grid>
                      <Grid item xs={6} />
                    </>
                  ) : StepperHelper.getStep() === 3 ? (
                    <>
                      <Grid item xs={6}>
                        <button className={styles.previousBtn} onClick={() => props.history.push(StepperHelper.decreasedStepUrl())}>
                          <span>Previous</span>
                        </button>
                      </Grid>
                      <Grid item xs={6}>
                        <button className={styles.sendReq} onClick={() => sendRequestToSkipper()}>
                          <span>Send the request</span>
                        </button>
                      </Grid>
                    </>
                  ) : null}
                </Grid>
              </Grid>
            </div>
          </>
        ) :
        <Grid item container>
          {guestBookingContext.booking.status === BookingStatusEnum.SkipperRequested ?
            (<Grid item xs={12}>
              <p className={styles.acceptInfo}>Congratulations on sending the skipper request!</p>
              <p className={styles.time}>{guestBookingContext.booking.skipper?.firstName!} has <b id="time-count" /> to respond to you.</p>
              <Grid item xs={12} container justify="center">
                <img src={WaitRespondIcon} alt="" height={220} width={220} />
              </Grid>
            </Grid>) :
            guestBookingContext.booking.status === BookingStatusEnum.SkipperAccepted ?
              (<SkipperAcceptedComponent booking={guestBookingContext.booking} />) : null}
        </Grid>}
    </div>
  );

};

export default GuestBookingContainer;
