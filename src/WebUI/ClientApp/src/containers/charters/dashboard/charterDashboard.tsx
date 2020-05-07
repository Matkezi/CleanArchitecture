import React, { useContext, useEffect, useState } from "react";
import { BookingContext } from '../../../providers/booking/booking';
import CharterBookingRow from "../../../components/charter/charterBooking/CharterBookingRow";
import { Divider, Grid } from "@material-ui/core";
import CreateBookingContainer from "./createBookingContainer";
import { EditBookingContext } from "../../../providers/booking/editBookingContext";
import { Booking } from "../../../types/Booking";
import BookingApi from "../../../services/shared/booking"
import styles from './styles.module.scss';
import { SortBookingHelper } from './../../../helpers/SortBookingsHelper';
import PlusIcon from '../../../assets/img/icons/plus-icon-green-37.png';
import DownIcon from '../../../assets/img/icons/down-sort-icon-black-13.png';
import { NotificationContext } from "../../../providers/notification";
import { NotificationType } from "../../../types/NotificationProps";


const CharterDashboard = () => {
    const bookingContext = useContext(BookingContext);
    const editBookingContext = useContext(EditBookingContext);
    const [sorter, setSorter] = useState({ numOfSorter: 0, asc: true });
    const notificationContext = useContext(NotificationContext);

    useEffect(() => {
        notificationContext.setLoading({ showLoading: true })
        try {
            bookingContext.setCharterBookingsData();
            notificationContext.setLoading({ showLoading: false })
        } catch (e) {
            notificationContext.setLoading({ showLoading: false })
            notificationContext.setSnackbar({ showSnackbar: true, message: e.message, type: NotificationType.Error })
        }
    }, []);

    useEffect(() => {
        const getCountries = async () => {
            await bookingContext.getCountries();
        }
        getCountries();
    }, []);

    const openCreateBooking = () => {
        editBookingContext.setBooking({});
        editBookingContext.setShowNewBooking(true);
    }

    const closeCreateBooking = () => {
        editBookingContext.setBooking({});
        editBookingContext.setShowNewBooking(false);
    }
    const closeEditBooking = () => {
        editBookingContext.setBooking({});
        editBookingContext.setShowEditBooking(false);
    }

    const editBooking = (booking: Booking) => {
        editBookingContext.setBooking(booking);
        editBookingContext.setShowEditBooking(true);
        window.scrollTo(0, 0)
    }

    const saveBooking = async () => {
        notificationContext.setLoading({ showLoading: true })
        try {
            await BookingApi.editBooking(editBookingContext.booking);
            editBookingContext.setShowEditBooking(false);
            editBookingContext.setShowNewBooking(false);
            editBookingContext.setBooking({});
            notificationContext.setSnackbar({ showSnackbar: true, message: "Booking saved!", type: NotificationType.Success })
            bookingContext.setCharterBookingsData();
        } catch (e) {
            notificationContext.setSnackbar({ showSnackbar: true, message: e.message, type: NotificationType.Error })
        } finally {
            notificationContext.setLoading({ showLoading: false })
        }
    }

    const sortBookings = (numOfSorter: number) => {
        if (sorter.numOfSorter === numOfSorter) {
            setSorter({ numOfSorter: numOfSorter, asc: !(sorter.asc) });
            bookingContext.setBookingsData(SortBookingHelper.sortBookings(bookingContext.bookings, numOfSorter, !sorter.asc));
        }
        else {
            setSorter({ numOfSorter: numOfSorter, asc: true });
            bookingContext.setBookingsData(SortBookingHelper.sortBookings(bookingContext.bookings, numOfSorter, true));
        }
    }

    return (
        <div className={styles.wrapper}>
            <div style={{ display: "block", position: "relative", marginTop: 60 }}>
                <Grid container direction="row">
                    <Grid item xs={12} container>
                        <Grid item xs={8}>
                            <div className={styles.title}>Bookings</div>
                        </Grid>
                        <Grid item xs={4} container justify="flex-end">
                            {editBookingContext.showEditBooking === false && editBookingContext.showNewBooking === false ?
                                <div className={styles.createDiv} onClick={() => openCreateBooking()}>
                                    <span>Create a New Booking</span>
                                    <img alt="" src={PlusIcon} className={styles.plusIcon} />
                                </div>
                                :
                                <>
                                    {editBookingContext.showEditBooking === false && editBookingContext.showNewBooking === true &&
                                        <div className={styles.createDiv} onClick={() => closeCreateBooking()}>
                                            <span>New Booking</span>
                                            <img alt="" src={PlusIcon} className={styles.plusIcon + " " + styles.rotate} />
                                        </div>
                                    }
                                    {editBookingContext.showEditBooking === true && editBookingContext.showNewBooking === false &&
                                        <div className={styles.createDiv} onClick={() => closeEditBooking()}>
                                            <span>Edit Booking</span>
                                            <img alt="" src={PlusIcon} className={styles.plusIcon + " " + styles.rotate} />
                                        </div>
                                    }
                                </>
                            }
                        </Grid>
                    </Grid>
                    <Grid item xs={12} style={{ marginTop: 15 }}>
                        <Divider />
                    </Grid>
                    {editBookingContext.showEditBooking || editBookingContext.showNewBooking ?
                        <CreateBookingContainer countries={bookingContext.countries} saveBooking={saveBooking}></CreateBookingContainer> : null}
                    <div className={styles.bookingContainer}>
                        <Grid container xs={12} item>
                            <Grid container xs={12} item justify="space-around" alignItems="center" className={styles.sorterDiv} style={{ marginBottom: 15 }}>
                                <Grid item xs={1} container direction="row">
                                    <div className={sorter.numOfSorter === 0 ? styles.filterLabel + " " + styles.clicked : styles.filterLabel} onClick={() => sortBookings(0)}>
                                        <Grid item container direction="row" xs={12}>
                                            <Grid item xs={10}>
                                                Status
                                            </Grid>
                                            {sorter.numOfSorter === 0 && <Grid item xs={2}>
                                                {!sorter.asc ? <img alt="" width={12} height={12} style={{ transform: "rotate(180deg)" }} src={DownIcon} className={styles.sortIcon} /> :
                                                    <img alt="" width={12} height={12} src={DownIcon} className={styles.sortIcon} />}
                                            </Grid>}
                                        </Grid>
                                    </div>
                                </Grid>
                                <Grid item xs={2} container direction="row">
                                    <Grid item container xs={7}>
                                        <div className={sorter.numOfSorter === 1 ? styles.filterLabel + " " + styles.clicked : styles.filterLabel} onClick={() => sortBookings(1)}>
                                            <Grid item container direction="row" xs={12}>
                                                <Grid item xs={9}>
                                                    Guest
                                            </Grid>
                                                {sorter.numOfSorter === 1 && <Grid item xs={2}>
                                                    {!sorter.asc ? <img alt="" width={12} height={12} style={{ transform: "rotate(180deg)" }} src={DownIcon} className={styles.sortIcon} /> :
                                                        <img alt="" width={12} height={12} src={DownIcon} className={styles.sortIcon} />}
                                                </Grid>
                                                }
                                            </Grid>
                                        </div>
                                    </Grid>
                                </Grid>
                                <Grid item xs={2} container direction="row">
                                    <Grid item container xs={6}>
                                        <div className={sorter.numOfSorter === 2 ? styles.filterLabel + " " + styles.clicked : styles.filterLabel} onClick={() => sortBookings(2)}>
                                            <Grid item container direction="row" xs={12}>
                                                <Grid item xs={9}>
                                                    Boat
                                            </Grid>
                                                {sorter.numOfSorter === 2 && <Grid item xs={2}>
                                                    {!sorter.asc ? <img alt="" width={12} height={12} style={{ transform: "rotate(180deg)" }} src={DownIcon} className={styles.sortIcon} /> :
                                                        <img alt="" width={12} height={12} src={DownIcon} className={styles.sortIcon} />}
                                                </Grid>}
                                            </Grid>
                                        </div>
                                    </Grid>
                                </Grid>
                                <Grid item xs={2} container direction="row">
                                    <Grid item container xs={8}>
                                        <div className={sorter.numOfSorter === 3 ? styles.filterLabel + " " + styles.clicked : styles.filterLabel} onClick={() => sortBookings(3)}>
                                            <Grid item container direction="row" xs={12}>
                                                <Grid item xs={9}>
                                                    Skipper
                                            </Grid>
                                                {sorter.numOfSorter === 3 && <Grid item xs={2}>
                                                    {!sorter.asc ? <img alt="" width={12} height={12} style={{ transform: "rotate(180deg)" }} src={DownIcon} className={styles.sortIcon} /> :
                                                        <img alt="" width={12} height={12} src={DownIcon} className={styles.sortIcon} />}
                                                </Grid>}
                                            </Grid>
                                        </div>
                                    </Grid>
                                </Grid>
                                <Grid item xs={3} container direction="row">
                                    <Grid item xs={5} container>
                                        <div className={sorter.numOfSorter === 4 ? styles.filterLabel + " " + styles.clicked : styles.filterLabel} onClick={() => sortBookings(4)}>
                                            <Grid item container direction="row" xs={12}>
                                                <Grid item xs={8}>
                                                    Dates
                                            </Grid>
                                                {sorter.numOfSorter === 4 && <Grid item xs={2}>
                                                    {!sorter.asc ? <img alt="" width={12} height={12} style={{ transform: "rotate(180deg)" }} src={DownIcon} className={styles.sortIcon} /> :
                                                        <img alt="" width={12} height={12} src={DownIcon} className={styles.sortIcon} />}
                                                </Grid>}
                                            </Grid>
                                        </div>
                                    </Grid>
                                </Grid>
                            </Grid>
                            <Grid container item xs={12} direction="row" alignItems="center" >
                                {bookingContext.bookings.map((booking, i) =>
                                    <div key={i} className={styles.bookingRow}>
                                        <CharterBookingRow booking={booking} editAction={editBooking} ></CharterBookingRow>
                                    </div>
                                )}
                            </Grid>
                        </Grid>
                    </div>
                </Grid>
            </div>
        </div>
    )
}

export default CharterDashboard;