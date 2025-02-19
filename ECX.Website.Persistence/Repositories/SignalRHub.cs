using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ECX.Website.Persistence.Repositories
{
 
    public class SignalRHub : Hub
    {
        // Send data to the client
        public async Task SendDataToClient(string message)
        {
            await Clients.All.SendAsync("ReceiveData", message);
        }

    }
}
