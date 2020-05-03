using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp1.Model;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace BlazorApp1.SignalR
{
    public class NotificationHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        ILogger<NotificationHub> _logger;

        public NotificationHub(ILogger<NotificationHub> logger)
        {
            _logger = logger;
            _logger.LogInformation("NotificationHub Created");
        }

        public override async Task OnConnectedAsync()
        {
            _logger.LogInformation("New Connection from Client");
            await base.OnConnectedAsync();
        }

        public async Task SendTwinUpdate(string action, Event eventArg)
        {
            await Clients.All.SendAsync("UpdateTwin", action, eventArg);
        }
        public async Task SendAlertsUpdate(string action, Event eventArg)
        {
            await Clients.All.SendAsync("UpdateAlerts", action, eventArg);
        }
        public async Task SendTelemetryUpdate(string action, Event eventArg)
        {
            await Clients.All.SendAsync("UpdateTelemetry", action, eventArg);
        }
        public async Task SendStatisticsUpdate(string action, Event eventArg)
        {
            await Clients.All.SendAsync("UpdateStatistics", action, eventArg);
        }


    }
}

