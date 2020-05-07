import { api } from '../api';
import { Booking, SkipperActionEnum } from '../../types/Booking';
import { Skipper } from '../../types/Skipper';
import { AvaliableSkipperSearch } from '../../types/Searches';

export default
  {
    getSkipperBookingsPending(): Promise<Booking[]> {
      return api.get('Booking/skipper/pending')
    },
    getSkipperBookingsAccepted(): Promise<Booking[]> {
      return api.get('Booking/skipper/accepted')
    },
    postSkipperAction(id: number, skipperAction: SkipperActionEnum): Promise<Booking> {
      return api.put('Booking/skipper-action/' + id + "/" + skipperAction)
    },
    getCharterBookings(): Promise<Booking[]> {
      return api.get('Booking/charter/all')
    },
    getGuestBooking(url: String): Promise<Booking> {
      return api.get('Booking/url/' + url)
    },
    editBooking(booking: Booking): Promise<Booking> {
      return api.post('Booking', booking)
    },
    getAvaliableSkippers(skipperSearch: AvaliableSkipperSearch): Promise<Skipper[]> {
      return api.post('Booking/fetch-skippers', skipperSearch)
    },
    postGuestAction(booking: Booking): Promise<Booking> {
      return api.put('Booking/guest-action', booking)
    },
  };