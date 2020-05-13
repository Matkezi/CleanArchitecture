﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkipperAgency.Application.Bookings.Commands.SkipperAcceptBooking;
using SkipperAgency.Application.Bookings.Commands.SkipperDeclineBooking;
using SkipperAgency.Application.Bookings.CommonModels;
using SkipperAgency.Domain.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SkipperAgency.Application.Bookings.Queries.GetSkipperBookingsByStatus;

namespace SkipperAgency.WebUI.Controllers
{
    [Authorize]
    [Authorize(Roles = "Admin, Skipper")]
    public class SkipperBookingController : ApiController
    {
        [HttpGet]
        [Route("pending")]
        public async Task<ActionResult<IEnumerable<BookingModel>>> GetSkipperBookingPending()
        {
            return Ok(await Mediator.Send(new GetSkipperBookingsByStatusQuery { BookingStatus = BookingStatusEnum.SkipperRequestPending }));
        }

        [HttpGet]
        [Route("accepted")]
        public async Task<ActionResult<IEnumerable<BookingModel>>> GetSkipperBookingAccepted()
        {
            return Ok(await Mediator.Send(new GetSkipperBookingsByStatusQuery { BookingStatus = BookingStatusEnum.SkipperAccepted }));
        }

        [HttpPut("accept")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SkipperAcceptBooking(SkipperAcceptBookingCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPut("decline")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SkipperDeclineBooking(SkipperDeclineBookingCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

    }
}
