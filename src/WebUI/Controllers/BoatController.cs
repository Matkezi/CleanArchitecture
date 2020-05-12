using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkipperAgency.Application.Boats.Commands.CreateBoat;
using SkipperAgency.Application.Boats.Commands.DeleteBoat;
using SkipperAgency.Application.Boats.Commands.UpdateBoat;
using System.Collections.Generic;
using System.Threading.Tasks;
using SkipperAgency.Application.Boats.Queries.GetCharterBoats;

namespace SkipperAgency.WebUI.Controllers
{
    [Authorize(Roles = "Admin, Charter")]
    public class BoatController : ApiController
    {

        // GET: api/Boat/Charter
        [HttpGet("Charter")]
        public async Task<ActionResult<IEnumerable<BoatModel>>> GetCharterBoats()
        {
            return Ok(await Mediator.Send(new GetCharterBoatsQuery()));
        }

        // POST: api/Boat
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create(CreateBoatCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        // PUT: api/Boat/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, UpdateBoatCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            await Mediator.Send(command);
            return NoContent();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteBoatCommand { Id = id });
            return NoContent();
        }
    }
}
