using ECX.Website.Application.DTOs.Account;
using ECX.Website.Application.DTOs.MarketData;
using ECX.Website.Application.Response;
using ECX.Website.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Application.Contracts.Persistence
{

    public interface IMarketDataRepository
    {
        
        DataTable GetActiveCommodities();
        DataTable GetScrollingData();

        DataTable GetRealTimeData();

        DataTable GetCommodityMarketData(string commodity);

        DataTable GetSmbolMarketData(string symbol);

        DataTable GetcommodityGrade();

        DataTable GetCommodity();
        DataTable GetCommodityTradeData(string commodity);

        DataTable GetPretradeNonCoffeeMarketData();
        DataTable GetPretradeCoffeeMarketData(string Num);




        //Task<BaseCommonResponse> LoginUserAsync(loginDto model);

        //Task<BaseCommonResponse> ConfirmEmailAsync(string userId, string token);

        //Task<BaseCommonResponse> ForgetPasswordAsync(string email);

        //Task<BaseCommonResponse> ResetPasswordAsync(ResetPasswordDto model);
    }
}
