using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkipperAgency.Application.Charters.Commands.CreateCharter;
using SkipperAgency.Application.Charters.Commands.DeleteCharter;
using SkipperAgency.Application.Charters.Commands.UpdateCharter;
using SkipperAgency.Application.Charters.Queries.GetAllCharters;
using SkipperAgency.Application.Charters.Queries.GetCharter;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace SkipperAgency.WebUI.Controllers
{
    [Authorize(Roles = "Admin, Charter")]
    public class CharterController : ApiController
    {

        // GET: api/Charter
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CharterModel>>> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllChartersQuery()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CharterModel>> Get(string id)
        {
            return Ok(await Mediator.Send(new GetCharterQuery { Id = id }));
        }

        // POST: api/Charter
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        [AllowAnonymous]
        public async Task<IActionResult> Create(CreateCharterCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(string id, UpdateCharterCommand command)
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
        public async Task<ActionResult> Delete(string id)
        {
            await Mediator.Send(new DeleteCharterCommand { Id = id });
            return NoContent();
        }
    }
}