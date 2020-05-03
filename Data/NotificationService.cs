using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;


namespace BlazorApp1.Data
{
    public class NotificationService
    {
        public HubConnection Connection { get; set; }

        public async Task ConnectAsync()
        {
            if (null == Connection)
            {
                Connection = new HubConnectionBuilder()
                    .WithUrl("https://localhost:5001/notificationhub")
                    .Build();

                Console.WriteLine("CONNECT FROM STATEFUL SERVICE");

                Connection.On<string, string>("BroadcastChannel", (user, message) =>
                {
                    this.OnMessage?.Invoke(user, message);
                });
            }
            else
            { 
                Console.WriteLine("ALREADY CONNECTED"); 
            }

            

            if (Connection.State != HubConnectionState.Connected)
            {
                await Connection.StartAsync();
            }
        }

        public Action<string, string> OnMessage { get; set; }

        public async Task BroadcastAsync(string sender, string message)
        {
            await Connection.InvokeAsync("Broadcast", sender, message);
        }
    }
}
