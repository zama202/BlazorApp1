using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task BroadcastMethod(string from, string message)
        {
            Console.WriteLine("IOPPOORRZ");
            _logger.LogInformation($"{from} broadcasts message: {message}");
            await Clients.All.SendAsync("BroadcastMethodReceived", from, message);
        }
    }
}

