using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Skippers.Models1;
using CleanArchitecture.Application.Skippers.Queries.GetSkipper;
using CleanArchitecture.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkipperBooking.Web.Controllers
{
    public class MetaController : ApiController
    {
        [HttpGet]
        [Route("languages")]
        public async Task<ActionResult<IEnumerable<LanguageModel>>> GetLanguages()
        {
            return Ok(await Mediator.Send(new GetLanguagesQuery()));

        }

        [HttpGet]
        [Route("countries")]
        public async Task<ActionResult<IEnumerable<CountryModel>>> GetCountries()
        {
            return Ok(await Mediator.Send(new GetCountriesQuery()));
        }
    }
}
