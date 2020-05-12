﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkipperAgency.Application.Skippers.Commands.Availability;
using SkipperAgency.Application.Skippers.Commands.CreateSkipper;
using SkipperAgency.Application.Skippers.Commands.DeleteSkipper;
using SkipperAgency.Application.Skippers.Commands.PreCreateSkipper;
using SkipperAgency.Application.Skippers.Commands.TrustedSkippers;
using SkipperAgency.Application.Skippers.Commands.UpdateSkipper;
using SkipperAgency.Application.Skippers.Queries.Availability.Common.Models;
using SkipperAgency.Application.Skippers.Queries.Availability.GetSkipperAvailability;
using SkipperAgency.Application.Skippers.Queries.GetAllSkippers;
using SkipperAgency.Application.Skippers.Queries.GetSkipper;
using SkipperAgency.Application.Skippers.Queries.PreGetSkipper;
using SkipperAgency.Application.Skippers.Queries.TrustedSkippers;
using SkipperAgency.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkipperAgency.WebUI.Controllers
{
    public class SkipperController : ApiController
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkipperModel>>> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllSkippersQuery()));
        }

        [HttpGet("{skipperId}", Name = "GetSkipper")]
        public async Task<ActionResult<SkipperModel>> Get(string skipperId)
        {
            return Ok(await Mediator.Send(new GetSkipperQuery { Id = skipperId }));
        }

        [HttpGet("avalibility/{skipperId}")]
        public async Task<ActionResult<AvailabilityModel>> GetSkipperAvalibility(string skipperId)
        {
            return Ok(await Mediator.Send(new GetSkipperAvailabilityQuery { Id = skipperId }));

        }

        // PUT: api/Skipper/avalibility/update
        //[Authorize(Roles = "Admin, Skipper")]
        [HttpPut("avalibility/update")]
        public async Task<IActionResult> UpdateSkipperAvalibility(UpdateSkipperAvailabilityCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        // POST: api/Skipper
        [HttpPost]
        public async Task<IActionResult> Create(CreateSkipperCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        // POST: api/Skipper
        [HttpPost("preregister")]
        public async Task<IActionResult> PreRegisterSkipper(PreCreateSkipperCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpGet("preregister/{url}")]
        public async Task<ActionResult<PreGetSkipperModel>> GetSkipperPreRegistration(string url)
        {
            return Ok(await Mediator.Send(new PreGetSkipperQuery { Url = url }));

        }

        // PUT: api/Skipper/5
        //[Authorize(Roles = "Admin, Skipper")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(UpdateSkipperCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Skipper>> Delete(string id)
        {
            await Mediator.Send(new DeleteSkipperCommand { Id = id });
            return NoContent();
        }

        #region Trusted Skipper Charter Actions


        // GET: api/Skipper/pending
        [Authorize(Roles = "Admin, Charter")]
        [HttpGet]
        [Route("pending")]
        public async Task<ActionResult<TrustedSkipperModel>> GetPendingSkippersAsync()
        {
            return Ok(await Mediator.Send(new GetPendingSkippersQuery()));
        }

        // GET: api/Skipper/trusted
        [Authorize(Roles = "Admin, Charter")]
        [HttpGet]
        [Route("trusted")]
        public async Task<ActionResult<TrustedSkipperModel>> GetTrustedSkippersAsync()
        {
            return Ok(await Mediator.Send(new GetTrustedSkippersQuery()));
        }

        // GET: api/Skipper/untrusted
        [Authorize(Roles = "Admin, Charter")]
        [HttpGet]
        [Route("untrusted")]
        public async Task<ActionResult<TrustedSkipperModel>> GetUnTrustedSkippersAsync()
        {
            return Ok(await Mediator.Send(new GetUnTrustedSkippersQuery()));
        }

        // PUT: api/Skipper/trustSkippers
        [Authorize(Roles = "Admin, Charter")]
        [HttpPut]
        [Route("trusted")]

        public async Task<IActionResult> UpdateTrustedSkippers(UpdateTrustedSkippersCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        // PUT: api/Skipper/unTrustSkippers
        [Authorize(Roles = "Admin, Charter")]
        [HttpPut]
        [Route("untrusted")]
        public async Task<IActionResult> UpdateUnTrustedSkippers(UpdateUnTrustedSkippersCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        #endregion
    }
}