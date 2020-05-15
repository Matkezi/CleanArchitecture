import { ICountry } from './ICountry';
import { ILanguage } from './ILanguage';
import { ISkill } from './ISkill';
import { Boat } from "./Boat";
import { Skipper } from "./Skipper";
import { Charter } from "./Charter";

export interface Booking {
  id?: number;
  boatId?: number;
  boat?: Boat;
  skipperId?: string;
  skipper?: Skipper;
  charterId?: string;
  charter?: Charter;
  bookedFrom?: Date;
  bookedTo?: Date;
  status?: BookingStatusEnum;
  bookingURL?: string;
  bookingHistories?: BookingHistory[];
  skipperAction?: string;
  onboardingLocation?: string;
  guestName?: string;
  guestEmail?: string;
  guestTOS?: boolean
  guestNationality?: ICountry;
  crewSize?: number;
  guestMessege?: string;
  skipperRequestTime?: Date
}

export interface BookingHistory {
  id?: number;
  skipperId: number;
  skipper: Skipper;
  dateTime: Date | string;
  bookingRejected: BookingRejectionEnum;
}

export interface BookingsContext {
  bookings: Booking[];
  countries: ICountry[];
  getCountries: Function;
  setSkipperBookingsData: Function;
  setCharterBookingsData: Function;
  setBookingsData: Function;
}

export interface IEditBookingContext {
  showEditBooking: boolean;
  showNewBooking: boolean;
  booking: Booking;
  setBooking: Function;
  setShowEditBooking: Function;
  setShowNewBooking: Function;
}

export interface IGuestBookingContext {
  step: Number;
  booking: Booking;
  skills: ISkill[],
  languages: ILanguage[],
  setBooking: Function;
  fetchGuestBooking: Function;
  setStep: Function;
  getLanguages: Function;
  getSkills: Function;
}

export enum BookingStatusEnum {
  DeliveryFail = 0,
  SkipperRequestPending = 1,
  SkipperRequested = 2,
  SkipperAccepted = 3,
  SkipperDeclined = 4
}

export enum BookingRejectionEnum {
  SkipperRejected = 0,
  SkipperTimeOutExpired = 1
}

export enum SkipperActionEnum {
  Decline = 0,
  Accept = 1
}