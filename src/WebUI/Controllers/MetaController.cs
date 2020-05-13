using Microsoft.AspNetCore.Mvc;
using SkipperAgency.Application.Metadata.Queries.GetCountries;
using SkipperAgency.Application.Metadata.Queries.GetLanguages;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace SkipperAgency.WebUI.Controllers
{
    [Authorize]
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
