using ServerHost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerHost.Services.Shop
{
    public class inMemoryShopGroupService : IShopGroupService
    {

        private readonly Dictionary<Guid, ShopGroup> _shopGroupInfo = new Dictionary<Guid, ShopGroup>();

        public Task<Guid> CreateShopGroup(string connectionId , string shopId)
        {

            var id = Guid.NewGuid();
            _shopGroupInfo[id] = new ShopGroup() 
            { 
                shopConnectionId = connectionId  ,
                shopId = shopId 
            };

            return Task.FromResult(id);
        }

        public Task<Guid> GetGroupForConnectionId(string connectionId)
        {
            var foundGroup = _shopGroupInfo.FirstOrDefault(x => x.Value.shopConnectionId == connectionId);

            if (foundGroup.Key == Guid.Empty)
            {
                throw new ArgumentException("Invalid Connection ID");
            }

            return Task.FromResult(foundGroup.Key);
        }

        public Task<Guid> GetShopConnectionIdByShopId(string shopId)
        {
            var foundGroup = _shopGroupInfo.FirstOrDefault(x => x.Value.shopId == shopId);

            if (foundGroup.Key == Guid.Empty)
            {
                throw new ArgumentException("Invalid Connection ID");
            }

            return Task.FromResult(foundGroup.Key);
        }
    }
}
