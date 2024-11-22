using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Application.DTOs.MarketData
{
    public class ScrollingData
    {

        public string Symbol { get; set; }

        public double OpeningPrice { get; set; }

        public double ClosingPrice { get; set; }

        public double DayHigh { get; set; }

        public double DayLow { get; set; }

        public double Change { get; set; }

        public double VolumeInLot { get; set; }

        public double VolumeInQuintal { get; set; }

        public double TradeDate { get; set; }

      
    }
}
