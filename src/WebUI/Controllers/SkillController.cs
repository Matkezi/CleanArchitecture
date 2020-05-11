using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SkipperAgency.Application.Skills.Queries.GetSkill;
using SkipperAgency.Domain.Enums;

namespace SkipperAgency.WebUI.Controllers
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