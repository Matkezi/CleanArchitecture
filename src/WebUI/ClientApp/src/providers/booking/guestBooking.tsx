import React, { useState } from "react";
import { IGuestBookingContext, Booking } from "../../types/Booking";
import bookingApi from "../../services/api/shared/booking";
import { ILanguage } from "../../types/ILanguage";
import { ISkill } from "../../types/ISkill";
import skillsApi from "../../services/api/skipper/skillsApi";
import metaApi from "../../services/api/meta/metaApi";

export const GuestBookingContext = React.createContext<IGuestBookingContext>({
  step: 1,
  booking: {},
  skills: [],
  languages: [],
  fetchGuestBooking: (url: string) => null,
  setBooking: () => null,
  setStep: () => null,
  getSkills: () => null,
  getLanguages: () => null
});

export const GuestBookingContextProvider: React.ComponentType<React.ReactNode> = props => {
  const [booking, setBooking] = useState<Booking>({});
  const [step, setStep] = useState<Number>(1);
  const [skills, setSkills] = useState<ISkill[]>([]);
  const [languages, setLanguages] = useState<ILanguage[]>([]);

  const fetchGuestBooking = async (url: string) => {
    const booking = await bookingApi.getGuestBooking(url);
    setBooking(booking);
    return booking;
  };

  const getSkills = async () => {
    const skills = await skillsApi.getSkills();
    setSkills(skills);
  }

  const getLanguages = async () => {
    const languages = await metaApi.getLanguages();
    setLanguages(languages);
  }

  return (
    <GuestBookingContext.Provider
      value={{
        booking,
        step,
        skills,
        languages,
        fetchGuestBooking,
        setBooking,
        setStep,
        getSkills,
        getLanguages
      }}
    >
      {props.children}
    </GuestBookingContext.Provider>
  );
};

export default GuestBookingContextProvider;
