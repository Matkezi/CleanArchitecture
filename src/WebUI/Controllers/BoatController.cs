using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using CleanArchitecture.Application.Boats.Queries.CharterGetBoats;
using CleanArchitecture.Application.Skippers.Queries.Availability;
using CleanArchitecture.Application.Boats.Commands;
using CleanArchitecture.Application.Boats.Commands.UpdateBoat;
using CleanArchitecture.Application.TodoLists.Commands.DeleteTodoList;

namespace CleanArchitecture.WebUI.Controllers
{

    public class BoatController : ApiController
    {

        // GET: api/Boat/Charter
        [HttpGet("Charter")]
        [Authorize(Roles = "Admin, Charter")]
        public async Task<ActionResult<IEnumerable<BoatModel>>> GetCharterBoats()
        {
            return Ok(await Mediator.Send(new CharterGetBoatsQuery()));
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
            if (id != command.BoatId)
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
            await Mediator.Send(new DeleteBoatCommand { BoatId = id });
            return NoContent();
        }
    }
}
