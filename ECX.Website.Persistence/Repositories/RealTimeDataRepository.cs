using ECX.Website.Application.Contracts.Persistence;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Persistence.Repositories
{
    internal class RealTimeDataRepository : IRealTimeDataRepository
    {
        private readonly IHubContext<SignalRHub> _hubContext;

        public RealTimeDataRepository(IHubContext<SignalRHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendRealTimeData(string data)
        {
            // Send the real-time data to all connected clients
            await _hubContext.Clients.All.SendAsync("ReceiveRealTimeData", data);
        }
    }
}
