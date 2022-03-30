using apii.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apii.Controllers
{
    [ApiController]
    [Route("Message")]
    public class MessageController : ControllerBase
    {
        IHubContext<SignalRHub> hub;

        public MessageController(IHubContext<SignalRHub> hub)
        {
            this.hub = hub;
        }

        [HttpPost]
        public void Create([FromBody] Message message)
        {
            DataContext.Messages.Add(message);
            this.hub.Clients.All.SendAsync("MessageCreated", message);
        }

        [HttpGet]
        public IEnumerable<Message> GetAll()
        {
            return DataContext.Messages;
        }
    }
}
