import React, { useEffect, useState, useContext } from 'react';
import BookingCard from '../../components/shared/bookingCard/bookingCard'
import { Booking, SkipperActionEnum } from '../../types/Booking';
import BookingApi from '../../services/api/shared/booking'
import SkippersHeader from '../../components/shared/skippersHeader'
import { SkipperStatus } from "../../types/SkipperStatus";
import { Grid, Divider, LinearProgress } from '@material-ui/core';
import styles from './styles.module.scss';
import { NotificationContext } from '../../providers/notification';
import { NotificationType } from '../../types/NotificationProps';
import { CLIENT } from '../../constants/clientRoutes';

interface IProps {
    history: any
}
const SkipperBooking: React.FC<IProps> = (props: IProps) => {

    const [currentlySelectedSkipperStatus, setCurrentlySelectedSkipperStatus] = useState<SkipperStatus>(SkipperStatus.Pending);
    const [initialPendingSkippers, setInitialPendingSkippers] = useState<Booking[]>([]);
    const [initialAcceptedSkippers, setInitialAcceptedSkippers] = useState<Booking[]>([]);
    const [bookingsToRender, setBookingsToRender] = useState<Booking[]>([]);
    const notificationContext = useContext(NotificationContext);
    const [loading, setLoading] = useState<boolean>(false);

    useEffect(() => {
        updateInitialSkippersFromBackend();
    }, []
    );

    const updateInitialSkippersFromBackend = async () => {
        setLoading(true);
        try {
            var pending = await BookingApi.getSkipperResponseBookingsPending();
            setInitialPendingSkippers([...pending]);
            if (currentlySelectedSkipperStatus === SkipperStatus.Pending) {
                setBookingsToRender([...pending]);
            }

            var accepted = await BookingApi.getSkipperBookingsAccepted();
            setInitialAcceptedSkippers([...accepted]);
            if (currentlySelectedSkipperStatus === SkipperStatus.Approved) {
                setBookingsToRender([...accepted]);
            }
            setLoading(false);
        } catch (e) {
            setLoading(true);
            notificationContext.setSnackbar({ showSnackbar: true, message: e.message, type: NotificationType.Error })
        }

    }

    const doSkipperAction = async (id: number, action: SkipperActionEnum) => {
        notificationContext.setLoading({ showLoading: true });
        try {
            await BookingApi.postSkipperAction(id, action);
            notificationContext.setSnackbar({ showSnackbar: true, message: "Action sucessful!", type: NotificationType.Success })
            updateInitialSkippersFromBackend();
        } catch (e) {
            notificationContext.setSnackbar({ showSnackbar: true, message: e.message, type: NotificationType.Error })
        } finally {
            notificationContext.setLoading({ showLoading: false });
        }

    }

    const updateBookingSkipperStatus = async (updatedSkipperStatus: SkipperStatus) => {
        setCurrentlySelectedSkipperStatus(updatedSkipperStatus);
        switch (updatedSkipperStatus) {
            case SkipperStatus.Pending:
                setBookingsToRender([...initialPendingSkippers]);
                break;
            case SkipperStatus.Approved:
                setBookingsToRender([...initialAcceptedSkippers]);
                break;
        }
    }

    return (
        <div className={styles.wrapper}>
            <div className={styles.pitchLine} style={{ marginBottom: 30 }}>Hey, there are Guests interested in Sailing with you! Respond to their requests.</div>
            <Grid container item xs={12} alignItems="stretch">
                <Grid item container xs={12} direction="column">
                    <SkippersHeader showDeclined={false} color="#23395b" updateSkipperStatus={updateBookingSkipperStatus}></SkippersHeader>
                    <Divider style={{ marginTop: 20 }} />
                </Grid>
                {loading ?
                    <Grid item container xs={12}>
                        <LinearProgress className={styles.linearProgress} />
                    </Grid>
                    :
                    <Grid container item alignItems="center" justify="center" className={styles.bookingContainer}>
                        {bookingsToRender.length > 0 ? bookingsToRender.map((booking, i) =>
                            <Grid item key={i} xs={12} className={styles.bookingCard}>
                                <BookingCard skipperView={true} pending={currentlySelectedSkipperStatus === SkipperStatus.Pending} showMessage={true} textForDisplay={booking.guestName + " (" + booking.guestNationality!.label + ")"} booking={booking} bookingAction={doSkipperAction}></BookingCard>
                            </Grid>
                        ) :
                            <> {currentlySelectedSkipperStatus === SkipperStatus.Pending ?
                                <Grid item xs={12} container>
                                    <Grid item container xs={12}>
                                        <Grid item md={9} xs={12}><p>No booking requests? Update your availability in order to get more chance for bookings requests!</p></Grid>
                                        <Grid item md={3} xs={12}><button className={styles.redirectBtn} onClick={() => props.history.push(CLIENT.SKIPPER.AVAILABILITY)}><span>Update availability</span></button></Grid>
                                    </Grid>
                                </Grid>
                                :
                                <Grid><div>There are no requests to show.</div></Grid>
                            }
                            </>
                        }
                    </Grid>}
            </Grid>
        </div>
    )
}
export default SkipperBooking;