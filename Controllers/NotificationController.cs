using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp1.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;


namespace BlazorApp1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        // GET: api/Notification
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Notification/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Notification
        [HttpPost]
        public void Post(NotificationItem item)
        {
            HubConnection _hubConnection = _hubConnection = new HubConnectionBuilder().WithUrl("https://localhost:5001/NotificationHub").Build();
            _hubConnection.StartAsync().GetAwaiter().GetResult();
            _hubConnection.SendAsync("SendMessage", item.User, item.Message);

        }

        // PUT: api/Notification/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
