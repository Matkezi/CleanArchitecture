using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkipperAgency.Application.Bookings.Commands.GuestRequestBooking;
using SkipperAgency.Application.Bookings.CommonModels;
using SkipperAgency.Application.Bookings.Queries.GetBookingByUrl;
using SkipperAgency.Application.Skippers.Queries.Availability.GetAvailableSkippers;
using SkipperAgency.Application.Skippers.Queries.GetSkipper;

namespace SkipperAgency.WebUI.Controllers.Bookings
{
    [AllowAnonymous]
    public class GuestBookingController : ApiController
    {
        [HttpGet("url/{url}")]
        [AllowAnonymous]
        public async Task<ActionResult<BookingModel>> GetByUrl(string url)
        {
            return Ok(await Mediator.Send(new GetBookingByUrlQuery { Url = url }));
        }

        [HttpPost]
        [Route("available-skippers")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<SkipperModel>>> GetAvailableSkippersForBooking(GetAvailableSkippersQuery command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("request")]
        [AllowAnonymous]
        public async Task<IActionResult> GuestRequestBooking(GuestRequestBookingCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

    }
}
