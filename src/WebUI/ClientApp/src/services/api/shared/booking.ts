import { api } from '../../api';
import { Booking, SkipperActionEnum } from '../../../types/Booking';
import { Skipper } from '../../../types/Skipper';
import { AvaliableSkipperSearch } from '../../../types/Searches';
import { API } from '../../../constants/apiRoutes';

export default
  {
    getSkipperBookingsPending(): Promise<Booking[]> {
      return api.get(API.SKIPPER.BOOKING.PENDING)
    },
    getSkipperBookingsAccepted(): Promise<Booking[]> {
      return api.get(API.SKIPPER.BOOKING.ACCEPTED)
    },
    postSkipperAction(id: number, skipperAction: SkipperActionEnum): Promise<Booking> {
      return api.put('Booking/skipper-action/' + id + "/" + skipperAction)
    },
    skipperAcceptBooking(id: number): Promise<Booking> {
      return api.put(API.SKIPPER.BOOKING.ACCEPT, id)
    },
    skipperDeclineBooking(id: number): Promise<Booking> {
      return api.put(API.SKIPPER.BOOKING.DECLINE, id)
    },
    getCharterBookings(): Promise<Booking[]> {
      return api.get(API.CHARTER.BOOKINGS.GET_ALL)
    },
    getGuestBooking(url: string): Promise<Booking> {
      return api.get(API.GUEST.BOOKING.GET_BY_URL(url))
    },
    editBooking(booking: Booking): Promise<Booking> {
      return api.post(API.CHARTER.BOOKINGS.CREATE, booking)
    },
    getAvaliableSkippers(skipperSearch: AvaliableSkipperSearch): Promise<Skipper[]> {
      return api.post(API.GUEST.BOOKING.GET_AVAILABLE_SKIPPERS, skipperSearch)
    },
    postGuestAction(booking: Booking): Promise<Booking> {
      return api.put(API.GUEST.BOOKING.REQUEST_BOOKING, booking)
    },
  };