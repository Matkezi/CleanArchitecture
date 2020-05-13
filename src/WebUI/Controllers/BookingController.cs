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
using SkipperAgency.Application.Bookings.Queries.GetAllCharterBookings;
using SkipperAgency.Application.Bookings.Queries.GetSkipperBookingsByStatus;

namespace SkipperAgency.WebUI.Controllers
{
    [Authorize]
    public class BookingController : ApiController
    {
        [HttpGet]
        [Route("skipper/pending")]
        [Authorize(Roles = "Admin, Skipper")]
        public async Task<ActionResult<IEnumerable<BookingModel>>> GetSkipperBookingPending()
        {
            return Ok(await Mediator.Send(new GetSkipperBookingsByStatusQuery { BookingStatus = BookingStatusEnum.SkipperRequestPending }));
        }

        [HttpGet]
        [Route("skipper/accepted")]
        [Authorize(Roles = "Admin, Skipper")]
        public async Task<ActionResult<IEnumerable<BookingModel>>> GetSkipperBookingAccepted()
        {
            return Ok(await Mediator.Send(new GetSkipperBookingsByStatusQuery { BookingStatus = BookingStatusEnum.SkipperAccepted }));
        }

        [HttpPut("skipper/accept")]
        [Authorize(Roles = "Admin, Skipper")]
        public async Task<IActionResult> SkipperAcceptBooking(SkipperAcceptBookingCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPut("skipper/decline")]
        [Authorize(Roles = "Admin, Skipper")]
        public async Task<IActionResult> SkipperDeclineBooking(SkipperDeclineBookingCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }



        // GET: api/Booking/url/hiw3ufqu32r4njfsd
        [HttpGet("guest/url/{url}")]
        [AllowAnonymous]
        public async Task<ActionResult<BookingModel>> GetByUrl(string url)
        {
            return Ok(await Mediator.Send(new GetBookingByUrlQuery{Url = url}));
        }

        [HttpGet]
        [Route("guest/skippers")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<SkipperModel>>> GetAvailableSkippersForBooking(GetAvailableSkippersQuery command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("guest/request")]
        [AllowAnonymous]
        public async Task<IActionResult> GuestRequestBooking(GuestRequestBookingCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Charter")]
        public async Task<ActionResult<IEnumerable<BookingModel>>> GetAll(GetAllCharterBookingsQuery command)
        {
            return Ok(await Mediator.Send(command));
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingModel>> Get(int id)
        {
            return Ok(await Mediator.Send(new GetBookingQuery{Id = id}));
        }

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
