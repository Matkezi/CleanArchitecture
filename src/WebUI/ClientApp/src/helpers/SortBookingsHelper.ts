import { Booking } from './../types/Booking'

export class SortBookingHelper {

    static sortBookings(bookings: Booking[], sorterNumber: number, asc?: boolean): Booking[] {
        if (sorterNumber === 0) {
            return bookings.sort((b1: Booking, b2: Booking) => {
                if (b1.status! > b2.status!) return asc ? 1 : -1;
                if (b2.status! > b1.status!) return asc ? -1 : 1;
                return 0;
            });
        }
        if (sorterNumber === 1) {
            return bookings.sort((b1: Booking, b2: Booking) => {
                if (b1.guestName! < b2.guestName!) return asc ? -1 : 1;
                if (b2.guestName! < b1.guestName!) return asc ? 1 : -1;
                return 0;
            });
        }
        if (sorterNumber === 2) {
            return bookings.sort((b1: Booking, b2: Booking) => {
                if (b1.boat === null) return asc ? 1 : -1;
                if (b2.boat === null) return asc ? -1 : 1;

                if (b1.boat!.name! > b2.boat!.name!) return asc ? -1 : 1;
                if (b2.boat!.name! > b1.boat!.name!) return asc ? 1 : -1;
                return 0;
            });
        }
        if (sorterNumber === 3) {
            return bookings.sort((b1: Booking, b2: Booking) => {
                if (b1.skipper === null) return asc ? 1 : -1;
                if (b2.skipper === null) return asc ? -1 : 1;
                if (b1.skipper!.firstName! < b2.skipper!.firstName!) return asc ? -1 : 1;
                if (b2.skipper!.firstName! < b1.skipper!.firstName!) return asc ? 1 : -1;
                return 0;
            });
        }
        if (sorterNumber === 4) {
            return bookings.sort((b1: Booking, b2: Booking) => {
                if (b1.bookedFrom! < b2.bookedFrom!) return asc ? -1 : 1;
                if (b2.bookedFrom! < b1.bookedFrom!) return asc ? 1 : -1;
                return 0;
            });
        }
        return bookings;
    }

}