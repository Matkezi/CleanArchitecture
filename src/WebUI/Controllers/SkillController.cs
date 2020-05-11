using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Skippers.Queries.GetSkipper;
using CleanArchitecture.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SkipperBooking.Base.Enums;
using SkipperBooking.Web.Models;

namespace SkipperBooking.Web.Controllers
{
    public class SkillController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkillModel>>> GetAll()
        {
            return Ok(await Mediator.Send(new GetSkillQuery()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SkillModel>> Get(SkillsEnum skillId)
        {
            return Ok(await Mediator.Send(new GetSkillQuery { SkillId = skillId }));
        }
    }
}