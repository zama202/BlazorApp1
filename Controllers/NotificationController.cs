using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp1.Data;
using BlazorApp1.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;
 

namespace BlazorApp1.Controllers
{

    [ApiController]
    public class NotificationController : ControllerBase
    {
        protected HubConnection hubConnection { get; set; }

        //protected void Connect() {
        //    hubConnection = new HubConnectionBuilder().WithUrl("https://localhost:5001/NotificationHub").Build();
        //    hubConnection.StartAsync().GetAwaiter().GetResult();
        //    Console.WriteLine("NEW CONNECTION");
        //}

        protected bool IsConnected() {
            return hubConnection != null;
        }

        // POST: api/Notification/
        [Route("api/[controller]")]
        [HttpPost]
        public void Post(NotificationItem item, [FromServices] NotificationService notificationService)
        {
            if (!IsConnected())
                hubConnection = notificationService.Connection;
            hubConnection.SendAsync("SendMessage", item.User, item.Message);
        }

        // POST: api/Notification/Twin/status
        //action can be update, reset, replot, etc)
        [Route("api/[controller]/Twin")]
        [HttpPost]
        public void Twin([FromHeader()]string action, [FromBody()] Event eventArg, [FromServices] NotificationService notificationService)
        {
            Task t = notificationService.ConnectAsync();

            if (!IsConnected())
                hubConnection = notificationService.Connection;
            hubConnection.SendAsync("SendTwinUpdate", action, eventArg);
        }

        // POST: api/Notification/Twin/status
        //action can be update, reset, replot, etc)
        [Route("api/[controller]/Statistics")]
        [HttpPost]
        public void Statistics([FromHeader()]string action, [FromBody()] Event eventArg, [FromServices] NotificationService notificationService)
        {
            Task t = notificationService.ConnectAsync();

            if (!IsConnected())
                hubConnection = notificationService.Connection;
            hubConnection.SendAsync("SendStatisticsUpdate", action, eventArg);
        }

        // POST: api/Notification/Twin/status
        //action can be update, reset, replot, etc)
        [Route("api/[controller]/Telemetry")]
        [HttpPost]
        public void Telemetry([FromHeader()]string action, [FromBody()] Event eventArg, [FromServices] NotificationService notificationService)
        {
            Task t = notificationService.ConnectAsync();

            if (!IsConnected())
                hubConnection = notificationService.Connection;
            hubConnection.SendAsync("SendTelemetryUpdate", action, eventArg);
        }

        // POST: api/Notification/Twin/status
        //action can be update, reset, replot, etc)
        [Route("api/[controller]/Alerts")]
        [HttpPost]
        public void Alerts([FromHeader()]string action, [FromBody()] Event eventArg, [FromServices] NotificationService notificationService)
        {
            Task t = notificationService.ConnectAsync();

            if (!IsConnected())
                hubConnection = notificationService.Connection;
            hubConnection.SendAsync("SendAlertsUpdate", action, eventArg);
        }

    }
}
