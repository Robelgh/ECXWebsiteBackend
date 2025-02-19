using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Persistence.Repositories
{
    public class CountService
    {
        private readonly IHubContext<SignalRHub> _hubContext;
        private Timer _timer;
        private int _currentCount;

        public CountService(IHubContext<SignalRHub> hubContext)
        {
            _hubContext = hubContext;
            _currentCount = 0;
        }

        public void StartCounting()
        {
            _timer = new Timer(UpdateCount, null, 0, 3000); 
        }

        private async void UpdateCount(object state)
        {
            if (_currentCount <= 20)
            {
                await _hubContext.Clients.All.SendAsync("ReceiveCount", _currentCount);
                _currentCount++;
            }
            else
            {
                _timer?.Dispose(); 
            }
        }
    }
}
