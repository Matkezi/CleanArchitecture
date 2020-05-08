using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SkipperBooking.Base.Enums;
using SkipperBooking.Business.Services.SkillServices;
using SkipperBooking.DAL;
using SkipperBooking.DAL.Entities;
using SkipperBooking.Web.Models;

namespace SkipperBooking.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly ISkillService _skillService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly SkipperBookingDBContext _context;
        public SkillController(ISkillService skillService, IMapper mapper, ILogger<SkipperController> logger, SkipperBookingDBContext context)
        {
            _skillService = skillService;
            _mapper = mapper;
            _logger = logger;
            _context = context;
        }

        // GET: api/Skill
        [HttpGet]
        public IEnumerable<Skill> Get()
        {
            return _context.Skills.ToList();
        }

        // GET: api/Skill/3
        [HttpGet("{id}", Name = "GetSkill")]
        public async Task<IActionResult> GetSkill(SkillsEnum skillId)
        {
            var skill = await _context.Skills.FindAsync(skillId);
            if (skill == null)
            {
                _logger.LogWarning("GetById({Id}) NOT FOUND", skillId);
                return NotFound("Can't find skill");
            }
            Skill skillModel = _mapper.Map<Skill>(skill);
            return Ok(skillModel);
        }
    }
}