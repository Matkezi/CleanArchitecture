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

namespace CleanArchitecture.WebUI.Controllers
{

    public class BoatController : ApiController
    {

        // GET: api/Boat/Charter
        [HttpGet("Charter")]
        [Authorize(Roles = "Admin, Charter")]
        public async Task<IEnumerable<BoatModel>> GetCharterBoats(CharterGetBoatsQuery command)
        {
            return await Mediator.Send(command);
        }

        // POST: api/Boat
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BoatModel boatModel)
        {
            Boat boat = new Boat();
            _mapper.Map(boatModel, boat);
            _context.Boats.Add(boat);
            await _context.SaveChangesAsync();
            _context.Entry<Boat>(boat).Reload();
            return CreatedAtAction("Create charter", new { id = boat.Id }, boat);
        }

        // PUT: api/Boat/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] BoatModel boatModel)
        {
            Boat boat = await _context.Boats.FindAsync(boatModel.Id);
            if (boat == null)
            {
                _logger.LogWarning("Update boat ({Id}) NOT FOUND", boatModel.Id);
                return NotFound("Can't update boat");
            }
            _mapper.Map(boatModel, boat);
            await _context.SaveChangesAsync();
            _context.Entry<Boat>(boat).Reload();
            return CreatedAtAction("Update skipper", new { id = boat.Id }, boat);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            Boat boat = await _context.Boats.FindAsync(id);
            if (boat == null)
            {
                _logger.LogWarning("Delete boat ({Id}) NOT FOUND", id);
                return NotFound("Can't delete boat");
            }

            _context.Boats.Remove(boat);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
