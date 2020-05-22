import React, { useState } from "react";
import { BookingsContext, Booking } from "../../types/Booking";
import bookingApi from "../../services/api/shared/booking";
import metaApi from "../../services/api/meta/metaApi";
import { ICountry } from "../../types/ICountry";

export const BookingContext = React.createContext<BookingsContext>({
  bookings: [],
  countries: [],
  getCountries: () => null,
  setSkipperBookingsData: () => null,
  setCharterBookingsData: () => null,
  setBookingsData: () => null
});

export const BookingContextProvider: React.ComponentType<React.ReactNode> = props => {
  const [bookings, setBookings] = useState<Booking[]>([]);
  const [countries, setCountries] = useState<ICountry[]>([]);

  const setSkipperBookingsData = async () => {
    const bookingData = await bookingApi.getSkipperBookingsPending();
    setBookings(bookingData);
  };

  const getCountries = async () => {
    const response = await metaApi.getCountries();
    setCountries(response);
  }

  const setCharterBookingsData = async () => {
    const bookingData = await bookingApi.getCharterBookings();
    setBookings(bookingData.sort((b1: Booking, b2: Booking) => {
      if (b1.status! > b2.status!) return 1;
      if (b2.status! > b1.status!) return -1;
      return 0;
    }));
  };

  const setBookingsData = (bookings: Booking[]) => {
    setBookings(bookings);
  }

  return (
    <BookingContext.Provider
      value={{
        bookings,
        setSkipperBookingsData,
        setCharterBookingsData,
        setBookingsData,
        countries,
        getCountries
      }}
    >
      {props.children}
    </BookingContext.Provider>
  );
};

export default BookingContextProvider
