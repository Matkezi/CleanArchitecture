import React, { useContext, useEffect } from "react";
import { EditBookingContext } from "../../../providers/booking/editBookingContext";
import styles from "./styles.module.scss";
import { Grid, FormControl } from "@material-ui/core";
import Select from 'react-select';
import { ICountry } from "../../../types/ICountry";

interface IProps {
  countries: ICountry[]
}

const EditGuest: React.FC<IProps> = (props: IProps) => {
  const editBookingContext = useContext(EditBookingContext);

  return (
    <React.Fragment>
      <Grid container spacing={2} className={styles.GuestInputRow}>
        <Grid item xs={3}>
          <input
            className={styles.textInput}
            placeholder="Name"
            value={editBookingContext.booking.guestName}
            onChange={e =>
              editBookingContext.setBooking({
                ...editBookingContext.booking,
                guestName: e.target.value
              })
            }
          ></input>
        </Grid>
        <Grid item xs={3}>
          <input
            placeholder="Email"
            value={editBookingContext.booking.guestEmail}
            onChange={e =>
              editBookingContext.setBooking({
                ...editBookingContext.booking,
                guestEmail: e.target.value
              })
            }
          ></input>
        </Grid>
        <Grid item xs={3}>
          <FormControl className={styles.formSelect}>
            <Select
              className={styles.autoCompleteSelect}
              value={editBookingContext.booking.guestNationality!}
              id="country"
              placeholder="Country"
              options={props.countries}
              onChange={e => editBookingContext.setBooking({
                ...editBookingContext.booking,
                guestNationality: e
              })}
            >
            </Select>
          </FormControl>
        </Grid>
      </Grid>
      <Grid container spacing={2} className={styles.GuestInputRow}>
        <Grid item xs={3}>
          <input
            placeholder="No. of Guests"
            value={editBookingContext.booking.crewSize}
            onChange={e =>
              editBookingContext.setBooking({
                ...editBookingContext.booking,
                crewSize: e.target.value as unknown as number
              })
            }
          ></input>
        </Grid>
        <Grid item xs={3}>
          <input
            placeholder="Onboarding location"
            value="Marina Mandalina - Šibenik"
            disabled
          ></input>
        </Grid>
      </Grid>
    </React.Fragment>
  );
};

export default EditGuest;
