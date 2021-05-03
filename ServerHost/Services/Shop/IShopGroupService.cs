using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerHost.Services.Shop
{
    public interface IShopGroupService
    {
        Task<Guid> CreateShopGroup(string connectionId , string shopId);

        Task<Guid> GetGroupForConnectionId(string connectionId);

        Task<Guid> GetShopConnectionIdByShopId(string shopId);
    }
}
