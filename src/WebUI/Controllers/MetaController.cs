using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SkipperBooking.DAL;
using SkipperBooking.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkipperBooking.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class MetaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly SkipperBookingDBContext _context;

        public MetaController(IMapper mapper, ILogger<SkipperController> logger, SkipperBookingDBContext context)
        {
            _mapper = mapper;
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [Route("languages")]
        public IActionResult GetLanguages()
        {
            var languages = new List<LanguageModel>();
            return Ok(_mapper.Map(_context.Languages, languages));
        }

        [HttpGet]
        [Route("countries")]
        public IActionResult GetCountries()
        {
            var countries = new List<CountryModel>();
            return Ok(_mapper.Map(_context.Countries, countries));
        }
    }
}
