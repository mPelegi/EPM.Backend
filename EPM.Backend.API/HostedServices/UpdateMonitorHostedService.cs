using EPM.Backend.API.Hubs;
using EPM.Backend.BLL;
using EPM.Backend.Model.DTO;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EPM.Backend.API.HostedServices
{
    public class UpdateMonitorHostedService : IHostedService, IDisposable
    {
        private List<Timer> Timers;
        public IServiceProvider Services { get; }
        private MonitorBLL MonitorBLL;

        public UpdateMonitorHostedService(IServiceProvider services)
        {
            Services = services;
            MonitorBLL = new MonitorBLL();
            Timers = new List<Timer>();
        }

        private void UpdatePerformances(object state)
        {
            using (var scope = Services.CreateScope())
            {
                var hubContext = scope.ServiceProvider.GetRequiredService<IHubContext<MonitorHub>>();

                hubContext.Clients.Group("Monitor").SendAsync("UpdatePerformance", MonitorBLL.GetPerformances());
            }
        }

        private void UpdateDescriptions(object state)
        {
            using (var scope = Services.CreateScope())
            {
                var hubContext = scope.ServiceProvider.GetRequiredService<IHubContext<MonitorHub>>();

                hubContext.Clients.Group("Monitor").SendAsync("UpdateDescription", MonitorBLL.GetDescriptions());
            }
        }

        public void Dispose()
        {
            Timers?.ForEach(x => x.Dispose());
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Timers.Add(new Timer(UpdateDescriptions, null, 0, 1000)); 
            Timers.Add(new Timer(UpdatePerformances, null, 0, 1000));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Timers?.ForEach(x => x.Change(Timeout.Infinite, 0));

            return Task.CompletedTask;
        }
    }
}
