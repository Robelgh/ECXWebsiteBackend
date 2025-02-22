﻿using ECX.Website.Application.CQRS.SocialMedia_.Request.Command;
using ECX.Website.Application.CQRS.SocialMedia_.Request.Queries;
using ECX.Website.Application.DTOs.SocialMedia;
using ECX.Website.Application.Response;
using ECX.Website.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ECX.Website.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocialMediaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SocialMediaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<SocialMediaController>
        [HttpGet]
        public async Task<ActionResult<BaseCommonResponse>> Get()
        {
            var query = new GetSocialMediaListRequest();
            BaseCommonResponse response = await _mediator.Send(query);
            switch(response.Status){
                case "200" : return Ok(response);
                case "400" : return BadRequest(response);
                case "404" : return NotFound(response);
                default : return response;  
            }
        }

        // GET api/<SocialMediaController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BaseCommonResponse>> Get(Guid id)
        {
            var query = new GetSocialMediaDetailRequest { Id = id };
            BaseCommonResponse response = await _mediator.Send(query);
            switch(response.Status){
                case "200" : return Ok(response);
                case "400" : return BadRequest(response);
                case "404" : return NotFound(response);
                default : return response;  
            }
        }

        // POST api/<SocialMediaController>
        [HttpPost]
        public async Task<ActionResult<BaseCommonResponse>> Post([FromForm] SocialMediaFormDto data)
        {
            var command = new CreateSocialMediaCommand { SocialMediaFormDto = data };
            BaseCommonResponse response = await _mediator.Send(command);
            switch(response.Status){
                case "200" : return Ok(response);
                case "400" : return BadRequest(response);
                case "404" : return NotFound(response);
                default : return response;
                
            }

        }

        // PUT api/<SocialMediaController>/5
        [HttpPut]
        public async Task<ActionResult<BaseCommonResponse>> Put([FromForm] SocialMediaFormDto data)
        {
            var command = new UpdateSocialMediaCommand { SocialMediaFormDto = data};
            BaseCommonResponse response = await _mediator.Send(command);
            switch(response.Status){
                case "200" : return Ok(response);
                case "400" : return BadRequest(response);
                case "404" : return NotFound(response);
                default : return response;
                
            }
        }

        // DELETE api/<SocialMediaController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseCommonResponse>> Delete(Guid id)
        {
            var command = new DeleteSocialMediaCommand { Id = id };
            BaseCommonResponse response = await _mediator.Send(command);
            switch(response.Status){
                case "200" : return Ok(response);
                case "400" : return BadRequest(response);
                case "404" : return NotFound(response);
                default : return response;
                
            }
        }
    }
}