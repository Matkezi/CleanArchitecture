import React, { useState } from "react";
import { IEditBookingContext, Booking } from "../../types/Booking";

export const EditBookingContext = React.createContext<IEditBookingContext>({
  showEditBooking: false,
  showNewBooking: false,
  booking: {
    id: undefined,
    boatId: undefined,
    boat: undefined,
    skipperId: undefined,
    skipper: undefined,
    charterId: undefined,
    charter: undefined,
    bookedFrom: undefined,
    bookedTo: undefined,
    status: undefined,
    bookingURL: undefined,
    bookingHistories: undefined,
    skipperAction: undefined,
    onboardingLocation: "Marina Mandalina - Šibenik",
    guestName: undefined,
    guestEmail: undefined,
    guestTOS: false,
    guestNationality: undefined,
    crewSize: undefined,
    guestMessege: undefined
  },
  setBooking: () => null,
  setShowEditBooking: () => null,
  setShowNewBooking: () => null
});

export const EditBookingContextProvider: React.ComponentType<React.ReactNode> = props => {
  const [booking, setBooking] = useState<Booking>({});
  const [showEditBooking, setShowEditBooking] = useState<boolean>(false);
  const [showNewBooking, setShowNewBooking] = useState<boolean>(false);

  return (
    <EditBookingContext.Provider
      value={{
        showEditBooking,
        showNewBooking,
        booking,
        setBooking,
        setShowEditBooking,
        setShowNewBooking
      }}
    >
      {props.children}
    </EditBookingContext.Provider>
  );
}