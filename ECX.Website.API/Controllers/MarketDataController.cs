using ECX.Website.Application.Contracts.Persistence;
using ECX.Website.Application.CQRS.MarketData_.Request.Queries;
using ECX.Website.Application.CQRS.Page_.Request.Command;
using ECX.Website.Application.CQRS.Page_.Request.Queries;
using ECX.Website.Application.DTOs.Page;
using ECX.Website.Application.Response;
using ECX.Website.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ECX.Website.API.Controllers
{


        [Route("api/[controller]")]
        [ApiController]

        public class MarketDataController : ControllerBase
        {
            private readonly IMediator _mediator;
            private readonly IMarketDataRepository _marketdataRepository;
            private readonly IRealTimeDataRepository _realTimeDataRepository;

        public MarketDataController(IMediator mediator, IMarketDataRepository marketdataRepository, IRealTimeDataRepository realTimeDataRepository)
            {
                _mediator = mediator;
                _marketdataRepository = marketdataRepository;
                _realTimeDataRepository = realTimeDataRepository;
            }

            [HttpGet("send-data")]
            public async Task<IActionResult> SendData()
            {
                // You can get data from the repository here, if needed
                // For example, let's simulate data
                string data = "New data from server!";

                // Call the service to send data to clients via SignalR
                await _realTimeDataRepository.SendRealTimeData(data);

                return Ok("Data sent to clients!");
            }

       // [Authorize]
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

        [HttpGet("pretradenoncoffee/{commodity}")]
        public IActionResult GetPretradenoncoffeeMarketData(string commodity)
        {
            return Ok(_marketdataRepository.GetPretradeNonCoffeeMarketData(commodity));
        }

        [HttpGet("pretradecoffee/{isLocal}")]
        public IActionResult GetPretradeMarketData(string isLocal)
        {
            return Ok(_marketdataRepository.GetPretradeCoffeeMarketData(isLocal));
        }
        [HttpGet("nontraceablepretrade")]
        public IActionResult getNonTraceableCoffeePretrade()
        {
            return Ok(_marketdataRepository.getNonTraceableCoffeePretrade());
        }
        

        [HttpGet("symbol/{symbol}")]
        public IActionResult GetSymbolMarketData(string symbol)
        {
            return Ok(_marketdataRepository.GetSmbolMarketData(symbol));
        }

        
    }
}

