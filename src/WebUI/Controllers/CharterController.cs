using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkipperAgency.Application.Charters.Commands.CreateCharter;
using SkipperAgency.Application.Charters.Commands.DeleteCharter;
using SkipperAgency.Application.Charters.Commands.UpdateCharter;
using SkipperAgency.Application.Charters.Queries.GetAllCharters;
using SkipperAgency.Application.Charters.Queries.GetCharter;

namespace SkipperAgency.WebUI.Controllers
{
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
        public async Task<IActionResult> Create(CreateCharterCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        // PUT: api/Charter/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(UpdateCharterCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteCharter(string id)
        {
            await Mediator.Send(new DeleteCharterCommand { CharterId = id });
            return NoContent();
        }
    }
}