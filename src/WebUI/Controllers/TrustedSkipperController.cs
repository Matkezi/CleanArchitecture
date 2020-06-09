using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkipperAgency.Application.Skippers.Commands.TrustedSkippers;
using SkipperAgency.Application.Skippers.Queries.TrustedSkippers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace SkipperAgency.WebUI.Controllers
{
    [Authorize(Roles = "Admin, Charter")]
    public class TrustedSkipperController : ApiController
    {
        [HttpGet]
        [Route("pending")]
        public async Task<ActionResult<TrustedSkipperModel>> GetPendingSkippersAsync()
        {
            return Ok(await Mediator.Send(new GetPendingSkippersQuery()));
        }

        [HttpGet]
        [Route("trusted")]
        public async Task<ActionResult<TrustedSkipperModel>> GetTrustedSkippersAsync()
        {
            return Ok(await Mediator.Send(new GetTrustedSkippersQuery()));
        }

        [HttpGet]
        [Route("untrusted")]
        public async Task<ActionResult<TrustedSkipperModel>> GetUnTrustedSkippersAsync()
        {
            return Ok(await Mediator.Send(new GetUnTrustedSkippersQuery()));
        }

        [HttpPut]
        [Route("trusted")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTrustedSkippers(IEnumerable<string> Ids)
        {
            await Mediator.Send(new UpdateTrustedSkippersCommand { Ids = Ids });
            return NoContent();
        }

        [HttpPut]
        [Route("untrusted")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUnTrustedSkippers(IEnumerable<string> Ids)
        {
            await Mediator.Send(new UpdateUnTrustedSkippersCommand { Ids = Ids });
            return NoContent();
        }
    }
}