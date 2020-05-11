using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkipperAgency.Application.Bookings.Commands.CharterCreateBooking;
using SkipperAgency.Application.Bookings.Commands.CharterDeleteBooking;
using SkipperAgency.Application.Bookings.Commands.GuestRequestBooking;
using SkipperAgency.Application.Bookings.Commands.SkipperAcceptBooking;
using SkipperAgency.Application.Bookings.Commands.SkipperDeclineBooking;
using SkipperAgency.Application.Bookings.CommonModels;
using SkipperAgency.Application.Bookings.Queries.CharterGetBookings;
using SkipperAgency.Application.Bookings.Queries.GetBooking;
using SkipperAgency.Application.Bookings.Queries.GetBookingByUrl;
using SkipperAgency.Application.Bookings.Queries.SkipperGetBookings;
using SkipperAgency.Application.Skippers.Queries.Availability.GetAvailableSkippers;
using SkipperAgency.Application.Skippers.Queries.GetSkipper;
using SkipperAgency.Domain.Enums;

namespace SkipperAgency.WebUI.Controllers
{
    public class BookingController : ApiController
    {

        [HttpGet]
        [Route("skipper/pending")]
        [Authorize(Roles = "Admin, Skipper")]
        public async Task<ActionResult<IEnumerable<BookingModel>>> GetSkipperBookingPending()
        {
            return Ok(await Mediator.Send(new SkipperGetBookingsQuery { BookingStatus = BookingStatusEnum.SkipperRequestPending }));
        }

        [HttpGet]
        [Route("skipper/accepted")]
        [Authorize(Roles = "Admin, Skipper")]
        public async Task<ActionResult<IEnumerable<BookingModel>>> GetSkipperBookingAccepted()
        {
            return Ok(await Mediator.Send(new SkipperGetBookingsQuery { BookingStatus = BookingStatusEnum.SkipperAccepted }));
        }

        [HttpPost]
        [Route("fetch-skippers")]
        public async Task<ActionResult<IEnumerable<SkipperModel>>> GetAvaliableSkippersForBooking(GetAvailableSkippersQuery command)
        {
            return Ok(await Mediator.Send(command));

        }

        [HttpGet]
        [Route("charter/all")]
        [Authorize(Roles = "Admin, Charter")]
        public async Task<ActionResult<IEnumerable<BookingModel>>> GetCharterBookings(CharterGetBookingsQuery command)
        {
            return Ok(await Mediator.Send(command));
        }

        // GET: api/Booking/5 
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<BookingModel>> GetAsync(GetBookingQuery command)
        {
            return Ok(await Mediator.Send(command));
        }

        // GET: api/Booking/url/hiw3ufqu32r4njfsd
        [HttpGet("url/{url}", Name = "GetByUrl")]
        public async Task<ActionResult<BookingModel>> GetAsyncByUrl(GetBookingByUrlQuery command)
        {
            return Ok(await Mediator.Send(command));
        }

        // POST: api/Booking
        [HttpPost]
        [Authorize(Roles = "Admin, Charter")]
        public async Task<IActionResult> Create(CharterCreateBookingCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPut("skipper-action/accept")]
        [Authorize(Roles = "Admin, Skipper")]
        public async Task<IActionResult> SkipperAcceptBooking(SkipperAcceptBookingCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPut("skipper-action/decline")]
        [Authorize(Roles = "Admin, Skipper")]
        public async Task<IActionResult> SkipperDeclineBooking(SkipperDeclineBookingCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPut("guest-action/request")]
        public async Task<IActionResult> GuestRequestBooking(GuestRequestBookingCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Charter")]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new CharterDeleteBookingCommand { BookingId = id });
            return NoContent();
        }
    }
}
