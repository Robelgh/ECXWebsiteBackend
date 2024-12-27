using ECX.Website.Application.CQRS.FeedBack_.Request.Command;
using ECX.Website.Application.CQRS.FeedBack_.Request.Queries;
using ECX.Website.Application.CQRS.SessionSchedule_.Request.Command;
using ECX.Website.Application.CQRS.SessionSchedule_.Request.Queries;
using ECX.Website.Application.DTOs.FeedBack;
using ECX.Website.Application.DTOs.SessionSchedule;
using ECX.Website.Application.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECX.Website.API.Controllers
{
        [Route("api/[controller]")]
        [ApiController]

        public class SessionScheduleController : ControllerBase
        {
            private readonly IMediator _mediator;

            public SessionScheduleController(IMediator mediator)
            {
                _mediator = mediator;
            }

            // GET: api/<SessionScheduleController>
           [HttpGet]
            public async Task<ActionResult<BaseCommonResponse>> Get()
            {
                var query = new GetSessionScheduleListRequest();
                BaseCommonResponse response = await _mediator.Send(query);
                switch (response.Status)
                {
                    case "200": return Ok(response);
                    case "400": return BadRequest(response);
                    case "404": return NotFound(response);
                    default: return response;
                }
            }

        // GET api/<SessionScheduleController>/5
        [HttpGet("{id}")]
            public async Task<ActionResult<BaseCommonResponse>> Get(Guid id)
            {
                var query = new GetSessionScheduleDetailRequest { Id = id };
                BaseCommonResponse response = await _mediator.Send(query);
                switch (response.Status)
                {
                    case "200": return Ok(response);
                    case "400": return BadRequest(response);
                    case "404": return NotFound(response);
                    default: return response;
                }
            }

        // POST api/<SessionScheduleController>
        [HttpPost]
            public async Task<ActionResult<BaseCommonResponse>> Post([FromForm] SessionScheduleDto data)
            {
                var command = new CreateSessionScheduleCommand { SessionScheduleDto = data };
                BaseCommonResponse response = await _mediator.Send(command);
                switch (response.Status)
                {
                    case "200": return Ok(response);
                    case "400": return BadRequest(response);
                    case "404": return NotFound(response);
                    default: return response;

                }

            }

        // PUT api/<SessionScheduleController>/5
        [HttpPut]
            public async Task<ActionResult<BaseCommonResponse>> Put([FromForm] SessionScheduleDto data)
            {
                var command = new updateSessionScheduleCommand { SessionScheduleDto = data };
                BaseCommonResponse response = await _mediator.Send(command);
                switch (response.Status)
                {
                    case "200": return Ok(response);
                    case "400": return BadRequest(response);
                    case "404": return NotFound(response);
                    default: return response;

                }
            }

        // DELETE api/<SessionScheduleController>/5
        [HttpDelete("{id}")]
            public async Task<ActionResult<BaseCommonResponse>> Delete(Guid id)
            {
                var command = new DeleteSessionScheduleCommand { Id = id };
                BaseCommonResponse response = await _mediator.Send(command);
                switch (response.Status)
                {
                    case "200": return Ok(response);
                    case "400": return BadRequest(response);
                    case "404": return NotFound(response);
                    default: return response;

                }
            }
        }
    
}
