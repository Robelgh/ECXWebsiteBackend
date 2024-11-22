using ECX.Website.Application.Contracts.Persistence;
using ECX.Website.Application.CQRS.MarketData_.Request.Queries;
using ECX.Website.Application.CQRS.Page_.Request.Command;
using ECX.Website.Application.CQRS.Page_.Request.Queries;
using ECX.Website.Application.DTOs.Page;
using ECX.Website.Application.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECX.Website.API.Controllers
{


        [Route("api/[controller]")]
        [ApiController]

        public class MarketDataController : ControllerBase
        {
            private readonly IMediator _mediator;
        private readonly IMarketDataRepository _marketdataRepository;

            public MarketDataController(IMediator mediator, IMarketDataRepository marketdataRepository)
            {
                _mediator = mediator;
               _marketdataRepository = marketdataRepository;
            }

            // GET: api/<MarketDataEcxController>
            [HttpGet("scrollingdata")]
            public IActionResult Get()
            {

              return Ok(_marketdataRepository.GetScrollingData());
            }

            // GET: api/<MarketDataEcxController>
            [HttpGet("realtimedata")]
            public IActionResult GetRealtimeData()
            {

                return Ok(_marketdataRepository.GetRealTimeData());
            }

        // GET api/<MarketDataEcx>/5
        [HttpGet("{commodity}")]
            public IActionResult GetCommodityMarketData(string commodity)
            {
            return Ok(_marketdataRepository.GetCommodityMarketData(commodity));
        }
        [HttpGet("symbol/{symbol}")]
        public IActionResult GetSymbolMarketData(string symbol)
        {
            return Ok(_marketdataRepository.GetSmbolMarketData(symbol));
        }

        //// POST api/<MarketDataEcx>
        //[HttpPost]
        //public async Task<ActionResult<BaseCommonResponse>> Post([FromForm] PageFormDto data)
        //{
        //    var command = new CreatePageCommand { PageFormDto = data };
        //    BaseCommonResponse response = await _mediator.Send(command);
        //    switch (response.Status)
        //    {
        //        case "200": return Ok(response);
        //        case "400": return BadRequest(response);
        //        case "404": return NotFound(response);
        //        default: return response;

        //    }

        //}

        //// PUT api/<MarketDataEcx>/5
        //[HttpPut]
        //public async Task<ActionResult<BaseCommonResponse>> Put([FromForm] PageFormDto data)
        //{
        //    var command = new UpdatePageCommand { PageFormDto = data };
        //    BaseCommonResponse response = await _mediator.Send(command);
        //    switch (response.Status)
        //    {
        //        case "200": return Ok(response);
        //        case "400": return BadRequest(response);
        //        case "404": return NotFound(response);
        //        default: return response;

        //    }
        //}

        //// DELETE api/<MarketDataEcx>/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<BaseCommonResponse>> Delete(Guid id)
        //{
        //    var command = new DeletePageCommand { Id = id };
        //    BaseCommonResponse response = await _mediator.Send(command);
        //    switch (response.Status)
        //    {
        //        case "200": return Ok(response);
        //        case "400": return BadRequest(response);
        //        case "404": return NotFound(response);
        //        default: return response;

        //    }
        //}
    }
}

