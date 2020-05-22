using Microsoft.AspNetCore.Mvc;
using SkipperAgency.Application.Skills.Queries.GetSkill;
using SkipperAgency.Domain.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using SkipperAgency.Application.Skills.Queries.GetAllSkills;

namespace SkipperAgency.WebUI.Controllers
{
    [Authorize]
    public class SkillController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkillModel>>> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllSkillsQuery()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SkillModel>> Get(SkillsEnum skillId)
        {
            return Ok(await Mediator.Send(new GetSkillQuery { SkillId = skillId }));
        }
    }
}