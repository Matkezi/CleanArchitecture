using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkipperAgency.Application.Bookings.Commands.GuestRequestBooking;
using SkipperAgency.Application.Bookings.Commands.SkipperAcceptBooking;
using SkipperAgency.Application.Bookings.Commands.SkipperDeclineBooking;
using SkipperAgency.Application.Bookings.CommonModels;
using SkipperAgency.Application.Bookings.Queries.GetBooking;
using SkipperAgency.Application.Bookings.Queries.GetBookingByUrl;
using SkipperAgency.Application.Skippers.Queries.Availability.GetAvailableSkippers;
using SkipperAgency.Application.Skippers.Queries.GetSkipper;
using SkipperAgency.Domain.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using SkipperAgency.Application.Bookings.Commands.CreateBooking;
using SkipperAgency.Application.Bookings.Commands.DeleteBooking;
using SkipperAgency.Application.Bookings.Queries.GetCharterBookings;
using SkipperAgency.Application.Bookings.Queries.GetSkipperBookings;

namespace SkipperAgency.WebUI.Controllers
{
    public class BookingController : ApiController
    {

        [HttpGet]
        [Route("skipper/pending")]
        [Authorize(Roles = "Admin, Skipper")]
        public async Task<ActionResult<IEnumerable<BookingModel>>> GetSkipperBookingPending()
        {
            return Ok(await Mediator.Send(new GetSkipperBookingsQuery { BookingStatus = BookingStatusEnum.SkipperRequestPending }));
        }

        [HttpGet]
        [Route("skipper/accepted")]
        [Authorize(Roles = "Admin, Skipper")]
        public async Task<ActionResult<IEnumerable<BookingModel>>> GetSkipperBookingAccepted()
        {
            return Ok(await Mediator.Send(new GetSkipperBookingsQuery { BookingStatus = BookingStatusEnum.SkipperAccepted }));
        }

        [HttpPost]
        [Route("fetch-skippers")]
        public async Task<ActionResult<IEnumerable<SkipperModel>>> GetAvailableSkippersForBooking(GetAvailableSkippersQuery command)
        {
            return Ok(await Mediator.Send(command));

        }

        [HttpGet]
        [Route("charter/all")]
        [Authorize(Roles = "Admin, Charter")]
        public async Task<ActionResult<IEnumerable<BookingModel>>> GetCharterBookings(GetCharterBookingsQuery command)
        {
            return Ok(await Mediator.Send(command));
        }

        // GET: api/Booking/5 
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingModel>> GetAsync(GetBookingQuery command)
        {
            return Ok(await Mediator.Send(command));
        }

        // GET: api/Booking/url/hiw3ufqu32r4njfsd
        [HttpGet("url/{url}")]
        public async Task<ActionResult<BookingModel>> GetAsyncByUrl(GetBookingByUrlQuery command)
        {
            return Ok(await Mediator.Send(command));
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

        // POST: api/Booking
        [HttpPost]
        [Authorize(Roles = "Admin, Charter")]
        public async Task<IActionResult> Create(CreateBookingCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Charter")]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteBookingCommand { Id = id });
            return NoContent();
        }
    }
}
