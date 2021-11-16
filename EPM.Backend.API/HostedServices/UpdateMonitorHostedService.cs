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
        private Timer Timer;
        public IServiceProvider Services { get; }
        private PerformanceBLL PerformanceBLL;
        private DescriptionBLL DescriptionBLL;

        public UpdateMonitorHostedService(IServiceProvider services)
        {
            Services = services;
            PerformanceBLL = new PerformanceBLL();
            DescriptionBLL = new DescriptionBLL();
        }

        private void UpdatePerformances(object state)
        {
            using (var scope = Services.CreateScope())
            {
                var hubContext = scope.ServiceProvider.GetRequiredService<IHubContext<MonitorHub>>();

                hubContext.Clients.Group("Monitor").SendAsync("UpdatePerformance", GetPerformance());
            }
        }

        private void UpdateDescriptions()
        {
            using (var scope = Services.CreateScope())
            {
                var hubContext = scope.ServiceProvider.GetRequiredService<IHubContext<MonitorHub>>();

                hubContext.Clients.Group("Monitor").SendAsync("UpdateDescription", GetDescription());
            }
        }

        private PerformanceDTO GetPerformance() 
        {
            return PerformanceBLL.GetPerformances();
        }

        private DescriptionDTO GetDescription() 
        {
            return DescriptionBLL.GetDescriptions();
        }

        public void Dispose()
        {
            Timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            //UpdateDescriptions();
            Timer = new Timer(UpdatePerformances, null, 0, 1000);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }
    }
}
