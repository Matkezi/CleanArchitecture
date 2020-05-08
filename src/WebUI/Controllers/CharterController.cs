using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SkipperBooking.Business.Services.CharterServices;
using SkipperBooking.DAL;
using SkipperBooking.DAL.Entities;
using SkipperBooking.Web.Models;

namespace SkipperBooking.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharterController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly ICharterService _charterService;
        private readonly SkipperBookingDBContext _context;

        public CharterController(IMapper mapper, ILogger<CharterController> logger, SkipperBookingDBContext context, ICharterService charterService)
        { 
             _charterService = charterService;
            _mapper = mapper;
            _logger = logger;
            _context = context;
           
        }

        // GET: api/Charter
        [HttpGet]
        public IEnumerable<Charter> Get()
        {
            return _context.Charter.ToList();
        }

        // GET: api/Charter/5
        [HttpGet("{id}", Name = "GetCharter")]
        public async Task<IActionResult> GetAsync(string id)
        {
            var charter = await _context.Charter.FindAsync(id);
            if (charter == null)
            {
                _logger.LogWarning("GetById({Id}) NOT FOUND", id);
                return NotFound("Can't find charter");
            }
            CharterModel charterModel = _mapper.Map<CharterModel>(charter);
            return Ok(charterModel);
        }

        // POST: api/Charter
        [HttpPost]
        public async Task<IActionResult> CreateCharter([FromBody] CharterRegistrationModel charterModel)
        {
            Charter charter = new Charter();
            _mapper.Map(charterModel, charter);
            var result = await _charterService.RegisterCharter(charter, charterModel.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result);
            }
            return CreatedAtAction("Create charter", new { id = charter.Id }, charter);
        }

        // PUT: api/Charter/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCharter([FromBody] CharterModel charterModel)
        {
            Charter charter = await _context.Charter.FindAsync(charterModel.Id);
            if (charter == null)
            {
                _logger.LogWarning("Update charter ({Id}) NOT FOUND", charterModel.Id);
                return NotFound("Can't update skipper");
            }
            var result = await _charterService.UpdateCharter(charter, charterModel.NewEmail);
            if (!result.Succeeded)
            {
                return BadRequest(result);
            }
            return CreatedAtAction("Update skipper", new { id = charter.Id }, charter);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCharter(string id)
        {
            Charter charter = await _context.Charter.FindAsync(id);
            if (charter == null)
            {
                _logger.LogWarning("Delete charter ({Id}) NOT FOUND", id);
                return NotFound("Can't delete charter");
            }
            try {
                _context.Charter.Remove(charter);
            }
            catch (Exception e)
            {
                _logger.LogError(e.StackTrace);
                return BadRequest("Can't delete charter");
            }
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}