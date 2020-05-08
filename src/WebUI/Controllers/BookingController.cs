using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SkipperBooking.Business.Services.BookingServices;
using SkipperBooking.DAL;
using SkipperBooking.DAL.Entities;
using SkipperBooking.Web.Models;
using SkipperBooking.Business.Services.SkipperServices;
using SkipperBooking.Base.Enums;
using Microsoft.EntityFrameworkCore;
using SkipperBooking.Base.Models;

namespace SkipperBooking.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IBookingService _bookingService;
        private readonly ISkipperService _skipperService;
        private readonly SkipperBookingDBContext _context;

        public BookingController(IMapper mapper, ILogger<BookingController> logger, SkipperBookingDBContext context, IBookingService bookingService, ISkipperService skipperService)
        {
            _mapper = mapper;
            _logger = logger;
            _context = context;
            _bookingService = bookingService;
            _skipperService = skipperService;
        }

        [HttpGet]
        [Route("skipper/pending")]
        [Authorize(Roles = "Admin, Skipper")]
        public IActionResult GetSkipperBookingPending()
        {           
            return Ok(_bookingService.FindSkipperBookingsByStatus(HttpContext.User.Identity.Name, BookingStatusEnum.SkipperRequested)
                .ToList().ConvertAll(booking => _mapper.Map<BookingModel>(booking)));
        }

        [HttpGet]
        [Route("skipper/accepted")]
        [Authorize(Roles = "Admin, Skipper")]
        public IActionResult GetSkipperBookingAccepted()
        {
            return Ok(_bookingService.FindSkipperBookingsByStatus(HttpContext.User.Identity.Name, BookingStatusEnum.SkipperAccepted).ToList().ConvertAll(booking => _mapper.Map<BookingModel>(booking)));
        }

        [HttpPost]
        [Route("fetch-skippers")]
        public IActionResult GetAvaliableSkippersForBooking([FromBody] AvaliableSkipperSearch search)
        {
            try
            {
                return Ok(_skipperService.FindSkippersForBooking(search).ToList().ConvertAll(skipper => _mapper.Map<SkipperModel>(skipper)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet]
        [Route("charter/all")]
        [Authorize(Roles = "Admin, Charter")]
        public IActionResult GetCharterBookings()
        {
            return Ok(_context.Bookings.Include(b => b.Charter).Include(b => b.Boat).Include(b => b.Skipper).Include(b => b.BookingHistories)
                .Where(b => b.Charter.Email == HttpContext.User.Identity.Name).ToList().ConvertAll(booking => _mapper.Map<BookingModel>(booking)));
        }

        // GET: api/Booking/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                _logger.LogWarning("GetById({Id}) NOT FOUND", id);
                return NotFound("Can't find booking");
            }
            BookingModel bookingModel = _mapper.Map<BookingModel>(booking);
            return Ok(bookingModel);
        }

        // GET: api/Booking/url/hiw3ufqu32r4njfsd
       [HttpGet("url/{url}", Name = "GetByUrl")]
        public IActionResult GetAsyncByUrl(string url)
        {
            var booking = _context.Bookings.Include(b => b.Boat).Include(b => b.Charter).Include(b => b.Skipper)
                .Where(b => b.BookingURL == url).FirstOrDefault();
            if (booking == null)
            {
                _logger.LogWarning("GetByUrl({Id}) NOT FOUND", url);
                return NotFound("Can't find booking");
            }
            BookingModel bookingModel = _mapper.Map<BookingModel>(booking);
            return Ok(bookingModel);
        }

        // POST: api/Booking
        [HttpPost]
        [Authorize(Roles = "Admin, Charter")]
        public async Task<IActionResult> Post([FromBody] BookingModel bookingModel)
        {
            Booking booking = new Booking();
            _mapper.Map(bookingModel, booking);
            var result = await _bookingService.CreateBooking(booking, HttpContext.User.Identity.Name);
            if (!result.Succeeded)
            {
                return BadRequest(result);
            }
            _context.Entry<Booking>(booking).Reload();
            return CreatedAtAction("Create charter", new { id = booking.Id }, _mapper.Map<BookingModel>(booking));
        }

        // PUT: api/skipper-action/5
        [HttpPut("skipper-action/{id}/{skipperAction}")]
        [Authorize(Roles = "Admin, Skipper")]
        public async Task<IActionResult> SkipperAction(int id, SkipperActionEnum skipperAction)
        {
            Booking booking =  _context.Bookings.Include(b => b.Skipper).Where(b => b.Id == id).FirstOrDefault();
            if (booking == null)
            {
                _logger.LogWarning("Update bookingModel ({Id}) NOT FOUND", id);
                return NotFound("Can't update bookingModel");
            }
            var result = await _bookingService.SkipperActionUpdate(booking, skipperAction);
            if (!result.Succeeded)
            {
                return BadRequest(result);
            }
            _context.Entry(booking).Reload();
            return CreatedAtAction("Update booking", new { id = booking.Id }, booking);
        }

        // PUT: api/guest-action
        [HttpPut("guest-action")]
        public async Task<IActionResult> GuestAction(BookingModel bookingModel)
        {
            Booking booking =  _context.Bookings.Include(x => x.Skipper).FirstOrDefault(x => x.Id == bookingModel.Id);
            if (booking == null)
            {
                _logger.LogWarning("Update bookingModel ({Id}) NOT FOUND", bookingModel.Id);
                return NotFound("Can't update bookingModel");
            }
            _mapper.Map(bookingModel, booking);
            await _bookingService.GuestActionRequestSkipper(booking);
            _context.Entry(booking).Reload();
            _mapper.Map(booking, bookingModel);
            return CreatedAtAction("Update booking", new { id = booking.Id }, bookingModel);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Booking booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                _logger.LogWarning("Delete booking ({Id}) NOT FOUND", id);
                return NotFound("Can't delete booking");
            }

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
