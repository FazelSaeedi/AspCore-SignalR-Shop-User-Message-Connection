using Microsoft.AspNetCore.SignalR;
using MyChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerHost
{
    public class ChatHub : Hub
    {

        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("ReceiveMessage" , "FTS" , DateTimeOffset.UtcNow , "Welcome User");

            await base.OnConnectedAsync();
        }


        public async Task SendMessage(string name, string text)
        {

            var message = new ChatMessage
            {
                SenderName = name,
                Text = text,
                SendAt = DateTimeOffset.Now
            };


            await Clients.All.SendAsync("ReceiveMessage" , message.SenderName , message.SendAt , message.Text);

        }

    }
}
