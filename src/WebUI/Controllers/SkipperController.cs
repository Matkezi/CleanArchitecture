using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkipperAgency.Application.Skippers.Commands.Availability;
using SkipperAgency.Application.Skippers.Commands.CreateSkipper;
using SkipperAgency.Application.Skippers.Commands.DeleteSkipper;
using SkipperAgency.Application.Skippers.Commands.PreCreateSkipper;
using SkipperAgency.Application.Skippers.Commands.TrustedSkippers;
using SkipperAgency.Application.Skippers.Commands.UpdateSkipper;
using SkipperAgency.Application.Skippers.Queries.Availability.Common.Models;
using SkipperAgency.Application.Skippers.Queries.Availability.GetSkipperAvailability;
using SkipperAgency.Application.Skippers.Queries.GetAllSkippers;
using SkipperAgency.Application.Skippers.Queries.GetSkipper;
using SkipperAgency.Application.Skippers.Queries.PreGetSkipper;
using SkipperAgency.Application.Skippers.Queries.TrustedSkippers;
using SkipperAgency.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SkipperAgency.WebUI.Controllers
{
    [Authorize(Roles = "Admin, Skipper")]
    public class SkipperController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkipperModel>>> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllSkippersQuery()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SkipperModel>> Get(string id)
        {
            return Ok(await Mediator.Send(new GetSkipperQuery { Id = id }));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Create(CreateSkipperCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(string id, UpdateSkipperCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Skipper>> Delete(string id)
        {
            await Mediator.Send(new DeleteSkipperCommand { Id = id });
            return NoContent();
        }

        [HttpPost("preregister")]
        public async Task<IActionResult> PreRegisterSkipper(PreCreateSkipperCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpGet("preregister/{url}")]
        public async Task<ActionResult<PreGetSkipperModel>> GetSkipperPreRegistration(string url)
        {
            return Ok(await Mediator.Send(new PreGetSkipperQuery { Url = url }));
        }
    }
}