using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.Backend.API.Hubs
{
    public class MonitorHub : Hub
    {
        public Task ConnectToMonitor()
        {
            Groups.AddToGroupAsync(Context.ConnectionId, "Monitor");

            return Task.CompletedTask;
        }
    }
}
