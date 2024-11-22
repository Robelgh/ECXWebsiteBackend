using ECX.Website.Application.CQRS.ParentLookup_.Request.Command;
using ECX.Website.Application.CQRS.ParentLookup_.Request.Queries;
using ECX.Website.Application.DTOs.PageCatagory;
using ECX.Website.Application.DTOs.ParentLookup;
using ECX.Website.Application.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECX.Website.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ParentLookupController : ControllerBase
    {


        private readonly IMediator _mediator;

        public ParentLookupController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<PageCatagoryController>
        [HttpGet]
        public async Task<ActionResult<BaseCommonResponse>> Get()
        {
            var query = new GetParentLookupListRequest();
            BaseCommonResponse response = await _mediator.Send(query);
            switch (response.Status)
            {
                case "200": return Ok(response);
                case "400": return BadRequest(response);
                case "404": return NotFound(response);
                default: return response;
            }
        }

        // GET api/<PageCatagoryController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BaseCommonResponse>> Get(Guid id)
        {
            var query = new GetParentLookupDetailRequest { Id = id };
            BaseCommonResponse response = await _mediator.Send(query);
            switch (response.Status)
            {
                case "200": return Ok(response);
                case "400": return BadRequest(response);
                case "404": return NotFound(response);
                default: return response;
            }
        }

        // GET api/<PageCatagoryController>/5
        [HttpGet("lan/{id}")]
        public async Task<ActionResult<BaseCommonResponse>> GetParentbyLan(Guid id)
        {
            var query = new GetParentLookupByLangRequest { Id = id };
            BaseCommonResponse response = await _mediator.Send(query);
            switch (response.Status)
            {
                case "200": return Ok(response);
                case "400": return BadRequest(response);
                case "404": return NotFound(response);
                default: return response;
            }
        }

        // POST api/<PageCatagoryController>
        [HttpPost]
        public async Task<ActionResult<BaseCommonResponse>> Post([FromForm] ParentLookupFormDto data)
        {
            var command = new CreateParentLookupCommand { ParentLookupFormDto = data };
            BaseCommonResponse response = await _mediator.Send(command);
            switch (response.Status)
            {
                case "200": return Ok(response);
                case "400": return BadRequest(response);
                case "404": return NotFound(response);
                default: return response;

            }

        }

        // PUT api/<PageCatagoryController>/5
        [HttpPut]
        public async Task<ActionResult<BaseCommonResponse>> Put([FromForm] ParentLookupFormDto data)
        {
            var command = new UpdateParentLookupCommand { ParentLookupFormDto = data };
            BaseCommonResponse response = await _mediator.Send(command);
            switch (response.Status)
            {
                case "200": return Ok(response);
                case "400": return BadRequest(response);
                case "404": return NotFound(response);
                default: return response;

            }
        }

        // DELETE api/<PageCatagoryController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseCommonResponse>> Delete(Guid id)
        {
            var command = new DeleteParentLookupCommand { Id = id };
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
