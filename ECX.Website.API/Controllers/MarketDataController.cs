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

            [HttpGet("commodityGrade")]
            public IActionResult GetCommodityGrade()
            {

                return Ok(_marketdataRepository.GetcommodityGrade());
            }

            [HttpGet("commodity")]
            public IActionResult GetCommodity()
            {

                return Ok(_marketdataRepository.GetCommodity());
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

        // GET api/<MarketDataEcx>/5
        [HttpGet("dailytradedata/{comid}")]
        public IActionResult GetCommodityDailyTradeData(string comid)
        {
            return Ok(_marketdataRepository.GetCommodityTradeData(comid));
        }

        [HttpGet("pretradenoncoffee")]
        public IActionResult GetPretradeMarketData()
        {
            return Ok(_marketdataRepository.GetPretradeNonCoffeeMarketData());
        }

        [HttpGet("pretradecoffee/{isLocal}")]
        public IActionResult GetPretradeMarketData(string isLocal)
        {
            return Ok(_marketdataRepository.GetPretradeCoffeeMarketData(isLocal));
        }

        [HttpGet("symbol/{symbol}")]
        public IActionResult GetSymbolMarketData(string symbol)
        {
            return Ok(_marketdataRepository.GetSmbolMarketData(symbol));
        }

        
    }
}

