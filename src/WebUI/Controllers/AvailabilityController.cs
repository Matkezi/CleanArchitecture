using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkipperAgency.Application.Skippers.Commands.Availability;
using SkipperAgency.Application.Skippers.Queries.Availability.Common.Models;
using SkipperAgency.Application.Skippers.Queries.Availability.GetSkipperAvailability;
using System.Threading.Tasks;

namespace SkipperAgency.WebUI.Controllers
{
    [Authorize]
    public class AvailabilityController : ApiController
    {
        [HttpGet("{skipperId}")]
        public async Task<ActionResult<AvailabilityModel>> GetSkipperAvailability(string skipperId)
        {
            return Ok(await Mediator.Send(new GetSkipperAvailabilityQuery { SkipperId = skipperId }));
        }

        [HttpPut]
        [Authorize(Roles = "Admin, Skipper")]
        public async Task<IActionResult> UpdateSkipperAvailability(UpdateSkipperAvailabilityCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }
    }
}