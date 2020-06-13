import { api } from '../../api';
import { Booking, SkipperActionEnum } from '../../../types/Booking';
import { Skipper } from '../../../types/Skipper';
import { AvaliableSkipperSearch } from '../../../types/Searches';
import { API } from '../../../constants/apiRoutes';
import { IRequestedBookingModel } from '../../../types/IRequestedBookingModel';

export default
  {
    getSkipperBookingsPending(): Promise<Booking[]> {
      return api.get(API.SKIPPER.BOOKING.PENDING)
    },
    getSkipperResponseBookingsPending(): Promise<Booking[]> {
      return api.get(API.SKIPPER.BOOKING.REQUESTED)
    },
    getSkipperBookingsAccepted(): Promise<Booking[]> {
      return api.get(API.SKIPPER.BOOKING.ACCEPTED)
    },
    skipperAcceptBooking(id: number): Promise<Booking> {
      return api.put(API.SKIPPER.BOOKING.ACCEPT+"/"+id)
    },
    skipperDeclineBooking(id: number): Promise<Booking> {
      return api.put(API.SKIPPER.BOOKING.DECLINE+"/"+id)
    },
    getCharterBookings(): Promise<Booking[]> {
      return api.get(API.CHARTER.BOOKINGS.GET_ALL)
    },
    getGuestBooking(url: string): Promise<Booking> {
      return api.get(API.GUEST.BOOKING.GET_BY_URL(url))
    },
    editBooking(booking: Booking): Promise<Booking> {
      return api.post(API.CHARTER.BOOKINGS.CREATE, { ...booking, crewSize: parseInt(booking.crewSize!, 10) })
    },
    getAvaliableSkippers(skipperSearch: AvaliableSkipperSearch): Promise<Skipper[]> {
      return api.post(API.GUEST.BOOKING.GET_AVAILABLE_SKIPPERS, skipperSearch)
    },
    postGuestAction(requestedBooking: IRequestedBookingModel): Promise<Booking> {
      return api.put(API.GUEST.BOOKING.REQUEST_BOOKING, requestedBooking)
    },
  };