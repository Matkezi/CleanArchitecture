import React, { useContext } from "react";
import { GuestBookingContext } from "../../providers/booking/guestBooking";
import { Grid } from "@material-ui/core";
import SelectedSkipperCard from './../../components/guest/SelectedSkipperCard';

const GuestStep3 = () => {
  const guestBookingContext = useContext(GuestBookingContext);

  const changeMessage = (text: string) => {
    guestBookingContext.setBooking({
      ...guestBookingContext.booking,
      guestMessege: text
    })
  }


  return (
    <div>
      <Grid container item xs={12}>
        <SelectedSkipperCard skipper={guestBookingContext.booking.skipper!} changeMessage={changeMessage} />
      </Grid>
    </div >
  );
};

export default GuestStep3;
