using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace wdpr_h.Models
{
    public class Chat : Hub
    {
        public Task Send(string message)
        {
            return Clients.All.SendAsync("Send", message);
        }
    }
}