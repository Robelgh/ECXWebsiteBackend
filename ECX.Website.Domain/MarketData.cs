using ECX.Website.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECX.Website.Domain
{
    public class MarketData : BaseDomainEntity
    {

        public Guid LangId { get; set; }

        public DateTime TradeDate { get; set;}

        public string Symbol { get; set; }

        public string WarehouseCode { get;set; }

        public double ProductionYear { get; set; }

        public double OpeningPrice { get; set; }

        public double ClosingPrice { get; set; }

        public double DayHigh { get; set; }

        public double DayLow { get; set; }

        public double Difference { get; set; }

        public double VolumeInLot { get; set; }

        public double VolumeInQuintal { get; set; }

        public double PercentageChange { get; set; }

        public double PreviousClosing { get; set; }

        public double DateKey { get; set; }

        public double tday { get; set; }

        public double tmonth { get; set; }

        public double tyear { get; set; }


    }
}
