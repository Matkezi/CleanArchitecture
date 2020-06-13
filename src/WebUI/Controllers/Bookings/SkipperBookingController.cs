using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkipperAgency.Application.Bookings.Commands.SkipperAcceptBooking;
using SkipperAgency.Application.Bookings.Commands.SkipperDeclineBooking;
using SkipperAgency.Application.Bookings.CommonModels;
using SkipperAgency.Application.Bookings.Queries.GetSkipperBookingsByStatus;
using SkipperAgency.Domain.Enums;

namespace SkipperAgency.WebUI.Controllers.Bookings
{
    [Authorize]
    [Authorize(Roles = "Admin, Skipper")]
    public class SkipperBookingController : ApiController
    {
        [HttpGet]
        [Route("requested")]
        public async Task<ActionResult<IEnumerable<BookingForSkipperModel>>> GetSkipperRequuestedBooking()
        {
            return Ok(await Mediator.Send(new GetSkipperBookingsByStatusQuery { BookingStatus = BookingStatusEnum.SkipperRequested }));
        }
        [HttpGet]
        [Route("pending")]
        public async Task<ActionResult<IEnumerable<BookingForSkipperModel>>> GetSkipperBookingPending()
        {
            return Ok(await Mediator.Send(new GetSkipperBookingsByStatusQuery { BookingStatus = BookingStatusEnum.SkipperRequestPending }));
        }

        [HttpGet]
        [Route("accepted")]
        public async Task<ActionResult<IEnumerable<BookingForSkipperModel>>> GetSkipperBookingAccepted()
        {
            return Ok(await Mediator.Send(new GetSkipperBookingsByStatusQuery { BookingStatus = BookingStatusEnum.SkipperAccepted }));
        }

        [HttpPut]
        [Route("accept/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SkipperAcceptBooking(int id)
        {
            await Mediator.Send(new SkipperAcceptBookingCommand { Id = id });
            return NoContent();
        }

        [HttpPut]
        [Route("decline/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SkipperDeclineBooking(int id)
        {
            await Mediator.Send(new SkipperDeclineBookingCommand { Id = id });
            return NoContent();
        }

    }
}
