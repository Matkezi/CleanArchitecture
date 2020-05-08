using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SkipperBooking.Web.Models;
using System.Threading.Tasks;
using AutoMapper;
using SkipperBooking.DAL.Entities;
using SkipperBooking.DAL;
using SkipperBooking.Business.Services.SkipperServices;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using SkipperBooking.Business.Services.CharterServices;
using System.Linq;
using Microsoft.AspNetCore.Cors;
using System;
using SkipperBooking.Business.Services.FIleService;

using SkipperBooking.Business.Services.SkillServices;
using SkipperBooking.Business.Services.User;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using SkipperBooking.Base.Models;

namespace SkipperBooking.Web.Controllers
{
    [Route("api/[controller]")]
    //[Authorize(Roles = "Admin, Skipper")]
    [ApiController]
    public class SkipperController : ControllerBase
    {
        private readonly ISkipperService _skipperService;
        private readonly ICharterService _charterService;
        private readonly IFileService _fileService;
        private readonly ISkillService _skillService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly SkipperBookingDBContext _context;
        public SkipperController(ISkipperService skipperService, ICharterService charterService, IFileService fileService, ISkillService skillService, IMapper mapper, ILogger<SkipperController> logger, SkipperBookingDBContext context)
        {
            _skipperService = skipperService;
            _charterService = charterService;
            _fileService = fileService;
            _skillService = skillService;
            _mapper = mapper;
            _logger = logger;
            _context = context;
        }

        // GET: api/Skipper
        [HttpGet]
        public IEnumerable<Skipper> Get()
        {
            return _context.Skipper.ToList();
        }

        // GET: api/Skipper/5
        [HttpGet("{skipperId}", Name = "GetSkipper")]
        public async Task<IActionResult> GetSkipper(string skipperId)
        {
            var skipper =  _context.Skipper.Include(s=>s.ListOfSkills).ThenInclude(s => s.Skill).Include(s => s.ListOfLanguages).ThenInclude(l => l.Language).Where(s => s.Id == skipperId).FirstOrDefault();
            if (skipper == null)
            {
                _logger.LogWarning("GetById({Id}) NOT FOUND", skipperId);
                return NotFound("Can't find skipper");
            }
            SkipperModel skipperModel = _mapper.Map<SkipperModel>(skipper);
            return Ok(skipperModel);
        }

        [HttpGet("avalibility/{skipperId}", Name = "GetSkipperAvalibility")]
        public async Task<IActionResult> GetSkipperAvalibility(string skipperId)
        {
            try { return Ok(_skipperService.GetAvalibility(skipperId)); }
            catch (Exception e)
            {
                _logger.LogError("Can't find avalibility!" + e.StackTrace);
                return BadRequest(e.Message);
            }
            
        }

        // PUT: api/Skipper/avalibility/update
        //[Authorize(Roles = "Admin, Skipper")]
        [HttpPut("avalibility/update")]
        public async Task<IActionResult> UpdateSkipperAvalibility(AvalibilityModel newAvalibility)
        {
            Skipper skipper = _context.Skipper.Include(x => x.Availability).Where(s => s.UserName == HttpContext.User.Identity.Name)
                .FirstOrDefault();
            if (skipper is null)
            {
                _logger.LogWarning("Update skipper avalibility NOT FOUND", HttpContext.User.Identity.Name);
                return NotFound("Can't update avalibility");
            }

            var result = await _skipperService.UpdateSkipperAvalibility(skipper, newAvalibility);

            if (!result.Succeeded)
            {
                _logger.LogError(string.Join("; ", result.Errors));
                return BadRequest(result);
            }
            return CreatedAtAction("Update avalibility", new { id = skipper.Id }, newAvalibility);
        }

        // POST: api/Skipper
        [HttpPost]
        public async Task<IActionResult> CreateSkipper([FromBody] SkipperRegisterPartialModel skipperModel)
        {
            Skipper skipper = new Skipper();
            _mapper.Map(skipperModel, skipper);
            var result = await _skipperService.RegisterSkipper(skipper, skipperModel.Password);
            if (!result.Succeeded)
            {
                _logger.LogError(string.Join("; ", result.Errors));
                return BadRequest(result.Errors.First().Description);
            }
            return CreatedAtAction("Create skipper", new { id = skipper.Id }, skipper);
        }

        // POST: api/Skipper
        [HttpPost("preregister")]
        public async Task<IActionResult> PreRegisterSkipper([FromBody] SkipperPreregisterModel skipperModel)
        {
            PreRegisterSkipper skipper = new PreRegisterSkipper();
            _mapper.Map(skipperModel, skipper);
            var result = await _skipperService.PreRigesterSkipper(skipper);
            if (!result.Succeeded)
            {
                _logger.LogError(string.Join("; ", result.Errors));
                return BadRequest(result.Errors.First().Description);
            }
            return CreatedAtAction("Preregistered skipper", new { id = skipper.Email }, skipper);
        }

        [HttpGet("preregister/{url}", Name = "GetSkipperPreRegistration")]
        public async Task<IActionResult> GetSkipperPreRegistration(string url)
        {
            var skipper = _context.SkipperPreRegistration.Where(s => s.URL == url).FirstOrDefault();
            if (skipper == null)
            {
                _logger.LogWarning("GetPreRegistrationById({url}) NOT FOUND", url);
                return NotFound("Can't find skipper preregistration");
            }
            SkipperPreregisterModel skipperModel = _mapper.Map<SkipperPreregisterModel>(skipper);
            return Ok(skipperModel);
        }

        // PUT: api/Skipper/5
        //[Authorize(Roles = "Admin, Skipper")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSkipper([FromBody] SkipperModel skipperModel, string id)
        {
            Skipper skipper = _context.Skipper.Include(x => x.ListOfSkills).ThenInclude(s => s.Skill).Include(x => x.ListOfLanguages).ThenInclude(l => l.Language).FirstOrDefault(x => x.Id == id);
            if (skipper is null)
            {
                _logger.LogWarning("Update skipper ({Id}) NOT FOUND", id);
                return NotFound("Can't update skipper");
            }

            _mapper.Map(skipperModel, skipper);

            var result = await _skipperService.UpdateSkipper(skipper, skipperModel.NewEmail, skipperModel.UserPhoto, skipperModel.UserLicence);

            if (!result.Succeeded)
            {
                _logger.LogError(string.Join("; ", result.Errors));
                return BadRequest(result);
            }
            Skipper skipper_reloaded = _context.Skipper.Include(x => x.ListOfSkills).ThenInclude(s => s.Skill).Include(x => x.ListOfLanguages).ThenInclude(l => l.Language).FirstOrDefault(x => x.Id == id);
            _mapper.Map(skipper_reloaded, skipperModel);
            return CreatedAtAction("Update skipper", new { id = skipper_reloaded.Id }, skipperModel);
        }

        // DELETE: api/Skippper/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Skipper>> DeleteSkipper(string id)
        {
            Skipper skipper = await _context.Skipper.FindAsync(id);
            if (skipper == null)
            {
                _logger.LogWarning("Delete skipper ({Id}) NOT FOUND", id);
                return NotFound("Can't delete skipper");
            }

            try {
                _context.Skipper.Remove(skipper);
                await _context.SaveChangesAsync();
            } 
            catch (Exception e)
            {
                _logger.LogError(e.StackTrace);
                return BadRequest("Can't delete skipper");
            }
            return Ok();
        }

        #region Trusted Skipper Charter Actions


        // GET: api/Skipper/pending
        [Authorize(Roles = "Admin, Charter")]
        [HttpGet]
        [Route("pending")]
        public async Task<IActionResult> GetPendingSkippersAsync()
        {
            // Get all that are not in trusted or untrusted
            Charter currentUser = GetCurrentCharter();
            var pendingSkippers = _context.Skipper.ToList().Where(skipper =>
                !currentUser.TrustedSkippers.Any(ts => ts.SkipperID == skipper.Id)
                && !currentUser.UnTrustedSkippers.Any(uts => uts.SkipperID == skipper.Id)
                ).ToList();

            return Ok(_mapper.Map<List<Skipper>, List<TrustedSkipperModel>>(pendingSkippers));
        }

        // GET: api/Skipper/trusted
        [Authorize(Roles = "Admin, Charter")]
        [HttpGet]
        [Route("trusted")]
        public async Task<IActionResult> GetTrustedSkippersAsync()
        {
            Charter currentUser = GetCurrentCharter();
            var  trustedSkippers = _context.Skipper.Include(s => s.ListOfLanguages).ThenInclude(l=> l.Language).Where(skipper => currentUser.TrustedSkippers.Select(x => x.SkipperID).Contains(skipper.Id)).ToList();
            return Ok(_mapper.Map<List<Skipper>, List<TrustedSkipperModel>>(trustedSkippers));
        }

        // GET: api/Skipper/untrusted
        [Authorize(Roles = "Admin, Charter")]
        [HttpGet]
        [Route("untrusted")]
        public async Task<IActionResult> GetUnTrustedSkippersAsync()
        {
            Charter currentUser = GetCurrentCharter();
            var unTrustedSkippers = _context.Skipper.Where(skipper => currentUser.UnTrustedSkippers.Select(x => x.SkipperID).Contains(skipper.Id)).ToList();
            return Ok(_mapper.Map<List<Skipper>, List<TrustedSkipperModel>>(unTrustedSkippers));
        }

        // PUT: api/Skipper/trustSkippers
        [Authorize(Roles = "Admin, Charter")]
        [HttpPut]
        [Route("trusted")]

        public async Task<IActionResult> UpdateTrustedSkippers([FromBody] List<string> trustedSkippers)
        {
            Charter currentUser = GetCurrentCharter();

            var result = await _charterService.UpdateTrustedSkippers(currentUser, trustedSkippers);
            if (!result.Succeeded)
            {
                _logger.LogError("Couldn't update trusted skipper list ({email})!", HttpContext.User.Identity.Name, result.Errors);
                return BadRequest(result);
            }

            return CreatedAtAction("Updated trusted skippers for charter", result);
        }

        // PUT: api/Skipper/unTrustSkippers
        [Authorize(Roles = "Admin, Charter")]
        [HttpPut]
        [Route("untrusted")]
        public async Task<IActionResult> UpdateUnTrustedSkippers([FromBody] List<string> unTrustedSkippers)
        {
            Charter currentUser = GetCurrentCharter();
            var result = await _charterService.UpdateUnTrustedSkippers(currentUser, unTrustedSkippers);

            if (!result.Succeeded)
            {
                _logger.LogError("Couldn't update trusted skipper list ({email})!", HttpContext.User.Identity.Name, result.Errors);
                return BadRequest(result);
            }

            return CreatedAtAction("Updated untrusted skippers for charter", result);
        }

        private Charter GetCurrentCharter()
        {
            var currentCharter = _context.Charter.Include(c => c.UnTrustedSkippers).Include(c => c.TrustedSkippers).FirstOrDefault(x => x.UserName == HttpContext.User.Identity.Name);
            if (currentCharter is null)
            {
                _logger.LogError($"Couldn't find current charter ({HttpContext.User.Identity.Name}) to update trusted skippers.");
                throw new ArgumentException();
            }
            return currentCharter;
        }
        #endregion
    }
}