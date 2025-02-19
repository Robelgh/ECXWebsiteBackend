using ECX.Website.Application.CQRS.TradeAnalysis_.Request.Command;
using ECX.Website.Application.CQRS.TradeAnalysis_.Request.Queries;
using ECX.Website.Application.CQRS.TrainingDoc_.Request.Command;
using ECX.Website.Application.CQRS.TrainingDoc_.Request.Queries;
using ECX.Website.Application.DTOs.TradeAnalysis;
using ECX.Website.Application.DTOs.TrainingDoc;
using ECX.Website.Application.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECX.Website.API.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class TradeAnalysisController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TradeAnalysisController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<TrainingDocController>
        [HttpGet]
        public async Task<ActionResult<BaseCommonResponse>> Get()
        {
            var query = new GetTradeAnalysisListRequest();
            BaseCommonResponse response = await _mediator.Send(query);
            switch (response.Status)
            {
                case "200": return Ok(response);
                case "400": return BadRequest(response);
                case "404": return NotFound(response);
                default: return response;
            }
        }

        // GET api/<TrainingDocController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BaseCommonResponse>> Get(Guid id)
        {
            var query = new GetTradeAnalysisDetailRequest { Id = id };
            BaseCommonResponse response = await _mediator.Send(query);
            switch (response.Status)
            {
                case "200": return Ok(response);
                case "400": return BadRequest(response);
                case "404": return NotFound(response);
                default: return response;
            }
        }

        // POST api/<TrainingDocController>
        [HttpPost]
        public async Task<ActionResult<BaseCommonResponse>> Post([FromForm] TradeAnalysisFormDto data)
        {
            var command = new CreateTradeAnalysisCommand { TradeAnalysisFormDto = data };
            BaseCommonResponse response = await _mediator.Send(command);
            switch (response.Status)
            {
                case "200": return Ok(response);
                case "400": return BadRequest(response);
                case "404": return NotFound(response);
                default: return response;

            }

        }

        // PUT api/<TrainingDocController>/5
        [HttpPut]
        public async Task<ActionResult<BaseCommonResponse>> Put([FromForm] TradeAnalysisFormDto data)
        {
            var command = new UpdateTradeAnalysisCommand { TradeAnalysisFormDto = data };
            BaseCommonResponse response = await _mediator.Send(command);
            switch (response.Status)
            {
                case "200": return Ok(response);
                case "400": return BadRequest(response);
                case "404": return NotFound(response);
                default: return response;

            }
        }

        // DELETE api/<TrainingDocController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseCommonResponse>> Delete(Guid id)
        {
            var command = new DeleteTradeAnalysisCommand { Id = id };
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
