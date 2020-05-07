import React, { useContext } from "react";
import { GuestBookingContext } from "../../providers/booking/guestBooking";
import BookingCard from "../../components/shared/bookingCard/bookingCard"
import { withStyles, Checkbox } from "@material-ui/core";
import styles from './styles.module.scss';
import CheckBoxOutlinedIcon from '@material-ui/icons/CheckBoxOutlined';
import CheckBoxOutlineBlankOutlinedIcon from '@material-ui/icons/CheckBoxOutlineBlankOutlined';

interface IProps {
    setTos: (value: boolean) => void,
    tos: boolean,
    charterName: string,
    showError: boolean
}

const GuestStep1: React.FC<IProps> = (props: IProps) => {

    const guestBookingContext = useContext(GuestBookingContext);


    const GreenCheckbox = withStyles({
        root: {
            color: "#23395b",
            '&$checked': {
                color: "#23395b",
            },
        },
        checked: {},
    })((props: any) => <Checkbox color="default" {...props} />);

    return (
        <>
            <p className={styles.greeting}>
                Hello, {guestBookingContext.booking.guestName}, let's sail together!<br />
                Request the right skipper for your trip.
            </p>
            <p className={styles.bookingP}>My Booking</p>
            <div className={styles.bookingCard}>
                <BookingCard skipperView={false} textForDisplay={props.charterName} booking={guestBookingContext.booking} showMessage={false}></BookingCard>
            </div>
            <div className={styles.tos}>
                <GreenCheckbox
                    checked={props.tos}
                    onChange={(event: any) => props.setTos(event.target.checked)}
                    value="primary"
                    inputProps={{ 'aria-label': 'primary checkbox' }}
                    size="medium"
                    icon={<CheckBoxOutlineBlankOutlinedIcon />}
                    checkedIcon={<CheckBoxOutlinedIcon />}
                    className={styles.checkbox}
                />
                Accept Terms of service
            </div>
            {props.showError &&
                <div className={"input-feedback " + styles.errorInput}>
                    You must accept Terms of service
            </div>
            }
        </>
    )
}

export default GuestStep1;