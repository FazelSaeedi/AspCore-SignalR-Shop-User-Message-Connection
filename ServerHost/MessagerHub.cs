using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using MyChat.Models;
using ServerHost.Services;
using ServerHost.Services.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerHost
{
    public class MessagerHub : Hub
    {
        private readonly IShopGroupService _shopGroupService;
        private readonly IClientGroupService _clientGroupService;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public MessagerHub(IShopGroupService shopGroupService , IHttpContextAccessor httpContextAccessor , IClientGroupService clientGroupService)
        {
            _shopGroupService = shopGroupService;
            _httpContextAccessor = httpContextAccessor;
            _clientGroupService = clientGroupService;
        }

        public override async Task OnConnectedAsync()
        {
            var Token = _httpContextAccessor.HttpContext.Request.Query["type"].ToString();


            if (Token == "ClientToken")
            {
                var clientGroup = await _clientGroupService.CreateClientGroup(Context.ConnectionId);

                await Groups.AddToGroupAsync(Context.ConnectionId, clientGroup.ToString());
                await Clients.Caller.SendAsync("ReceiveMessage" , "Hi Mr" , DateTimeOffset.UtcNow , "Welcome Client");

                await base.OnConnectedAsync();
            }

            if (Token == "ShopToken")
            {
                Random r = new Random();

                var shopId = r.Next(500) ;
                var shopGroup = await _shopGroupService.CreateShopGroup(Context.ConnectionId , shopId.ToString());

                await Groups.AddToGroupAsync(Context.ConnectionId, shopGroup.ToString());
                await Clients.Caller.SendAsync("ReceiveMessage", "Your Id is = "+ shopId + "", DateTimeOffset.UtcNow, "Welcome Shop");

                await base.OnConnectedAsync();
            }


            await base.OnConnectedAsync();
            return;


        }

        public async Task BuyProductFromShop ( string shopId , string productId )
        {
            var roomId = await _clientGroupService.GetGroupForConnectionId(Context.ConnectionId);
            var shopGroupId = _shopGroupService.GetShopConnectionIdByShopId(shopId);


            await Clients.Group(shopGroupId.Result.ToString()).SendAsync("ReceiveMessage", "User buy order = "+ productId + "", DateTime.UtcNow, "User buy order = " + productId + "");

        }
        public async Task SendMessage(string name, string text)
        {

            // var roomId = await _userGroupService.GetGroupForConnectionId(Context.ConnectionId);


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
