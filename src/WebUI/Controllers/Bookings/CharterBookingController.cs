using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkipperAgency.Application.Bookings.Commands.CreateBooking;
using SkipperAgency.Application.Bookings.Commands.DeleteBooking;
using SkipperAgency.Application.Bookings.CommonModels;
using SkipperAgency.Application.Bookings.Queries.GetAllCharterBookings;
using SkipperAgency.Application.Bookings.Queries.GetBooking;

namespace SkipperAgency.WebUI.Controllers.Bookings
{
    [Authorize(Roles = "Admin, Charter")]
    public class CharterBookingController : ApiController
    {

        [HttpGet]
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create(CreateBookingCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }
       
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Charter")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteBookingCommand { Id = id });
            return NoContent();
        }
    }
}
