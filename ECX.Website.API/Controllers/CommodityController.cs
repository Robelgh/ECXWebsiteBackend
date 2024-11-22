using ECX.Website.Application.Contracts.Persistence;
using ECX.Website.Application.CQRS.Commodity_.Request.Command;
using ECX.Website.Application.CQRS.Commodity_.Request.Queries;
using ECX.Website.Application.DTOs.Commodity;
using ECX.Website.Application.Response;
using ECX.Website.Domain;
using ECX.Website.Persistence.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ECX.Website.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class CommodityController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICommodityRepository _commoditiesRepository;
        private readonly IMarketDataRepository _marketdataRepository;


        public CommodityController(IMediator mediator , ICommodityRepository commoditiesRepository, IMarketDataRepository marketdataRepository)
        {
            _mediator = mediator;
            _commoditiesRepository = commoditiesRepository;
            _marketdataRepository = marketdataRepository;
        }

        // GET: api/<CommodityController>
        [HttpGet]
        public async Task<ActionResult<BaseCommonResponse>> Get()
        {
            var query = new GetCommodityListRequest();
            BaseCommonResponse response = await _mediator.Send(query);
            switch(response.Status){
                case "200" : return Ok(response);
                case "400" : return BadRequest(response);
                case "404" : return NotFound(response);
                default : return response;  
            }
        }



        // GET api/<CommodityController>/5
        [HttpGet("gecommodity/{id}")]
        public async Task<ActionResult<BaseCommonResponse>> Get(Guid id)
        {
            var query = new GetCommodityDetailRequest { Id = id };
            BaseCommonResponse response = await _mediator.Send(query);
            switch(response.Status){
                case "200" : return Ok(response);
                case "400" : return BadRequest(response);
                case "404" : return NotFound(response);
                default : return response;  
            }
        }


        // GET: api/<MarketDataEcxController>
        [HttpGet("activecommodities")]
        public IActionResult GetRealtimeData()
        {

            return Ok(_marketdataRepository.GetActiveCommodities());
        }

        // GET api/<CommodityController>/5
        [HttpGet("{commodity}")]
        public async Task<ActionResult<BaseCommonResponse>> Get(string commodity)
        {
            var query = new GetCommodityLookupDetailRequest { Commodity = commodity };
            BaseCommonResponse response = await _mediator.Send(query);
            switch (response.Status)
            {
                case "200": return Ok(response);
                case "400": return BadRequest(response);
                case "404": return NotFound(response);
                default: return response;
            }
        }

        // POST api/<CommodityController>
        [HttpPost]
        public async Task<ActionResult<BaseCommonResponse>> Post([FromForm] CommodityFormDto data)
        {
            var command = new CreateCommodityCommand { CommodityFormDto = data };
            BaseCommonResponse response = await _mediator.Send(command);
            switch(response.Status){
                case "200" : return Ok(response);
                case "400" : return BadRequest(response);
                case "404" : return NotFound(response);
                default : return response;
                
            }

        }

        // PUT api/<CommodityController>/5
        [HttpPut]
        public async Task<ActionResult<BaseCommonResponse>> Put([FromForm] CommodityFormDto data)
        {
            var command = new UpdateCommodityCommand { CommodityFormDto = data};
            BaseCommonResponse response = await _mediator.Send(command);
            switch(response.Status){
                case "200" : return Ok(response);
                case "400" : return BadRequest(response);
                case "404" : return NotFound(response);
                default : return response;
                
            }
        }

        // DELETE api/<CommodityController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseCommonResponse>> Delete(Guid id)
        {
            var command = new DeleteCommodityCommand { Id = id };
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